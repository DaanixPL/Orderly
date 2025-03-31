using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Orderly.Models;

namespace Orderly
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin", policy =>
                {
                    policy.WithOrigins("https://localhost:7238", "http://localhost:5086")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            // Add DbContext to DI container
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add controllers
            builder.Services.AddControllers();

            var app = builder.Build();

            // Use HTTPS redirection
            app.UseHttpsRedirection();
            app.UseCors("AllowMyOrigin");  // Use the appropriate CORS policy
            app.UseStaticFiles();

            // Automatically create database and tables
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate(); // Apply migrations to create tables if they do not exist
            }

            app.MapControllers(); // Map controllers
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}
