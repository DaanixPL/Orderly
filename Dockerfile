# Stage 1: Build Angular app
FROM node:20 AS builder

WORKDIR /app
COPY client ./client

WORKDIR /app/client
RUN npm install
RUN npm run build -- --output-path=../angular-dist

# Stage 2: Build .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "Orderly.csproj" -c Release -o /app/publish

# Stage 3: Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Skopiuj backend .NET
COPY --from=build /app/publish .

# Skopiuj zbudowan¹ aplikacjê Angular do wwwroot
COPY --from=builder /app/angular-dist /app/wwwroot

EXPOSE 80
ENTRYPOINT ["dotnet", "Orderly.dll"]
