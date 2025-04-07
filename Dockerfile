# Stage 1: 
FROM node:20 AS builder

WORKDIR /app
COPY wwwroot/angular-app/ ./angular-app/
WORKDIR /app/angular-app
RUN npm install
RUN npm run build -- --output-path=dist

# Stage 2: 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "Orderly.csproj" -c Release -o /app/publish

# Stage 3:
FROM base AS final
WORKDIR /app
COPY --from=builder /app/angular-app/dist /app/wwwroot
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Orderly.dll"]
