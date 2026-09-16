# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj first so restore is cached when only code changes
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source and publish
COPY . .
RUN dotnet publish -c Release -o /app

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Make sure the uploads folder exists inside the image (PhotoServices writes here)
RUN mkdir -p /app/wwwroot/uploads

# Render (and most hosts) inject a PORT env var the container must listen on
ENV ASPNETCORE_ENVIRONMENT=Production
CMD ["sh", "-c", "ASPNETCORE_URLS=http://+:${PORT:-8080} dotnet lab5.dll"]
