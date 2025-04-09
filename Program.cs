using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Orderly.Models;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        Env.Load();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", policy =>
            {
                policy.AllowAnyOrigin() // Zezwala na dost�p z dowolnego �r�d�a
                      .AllowAnyMethod() // Zezwala na wszystkie metody (GET, POST, PUT, DELETE, itp.)
                      .AllowAnyHeader() // Zezwala na wszystkie nag��wki
                      .AllowCredentials(); // Zezwala na przesy�anie danych uwierzytelniaj�cych (np. cookies)
            });
        });

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(Environment.GetEnvironmentVariable("DefaultConnection")));

        builder.Services.Configure<JwtSettings>(options =>
        {
            options.SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            options.Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            options.Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            options.ExpirationMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRATION_MINUTES"));
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "JwtBearer";
            options.DefaultChallengeScheme = "JwtBearer";
        }).AddJwtBearer("JwtBearer", options =>
        {
            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
            };
        });

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

        var app = builder.Build();

        app.Urls.Add("http://*:8080");

        app.UseExceptionHandler("/error");
        app.UseHttpsRedirection();
        app.UseCors("AllowSpecificOrigin");
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }

        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}
