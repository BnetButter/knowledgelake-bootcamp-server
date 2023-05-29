# Set the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy csproj and restore
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Set the ASP.NET Core URLs environment variable
ENV ASPNETCORE_URLS=http://+:5000

# Expose the application on port 5000
EXPOSE 5000

# Start the app
ENTRYPOINT ["dotnet", "MenuAPI.dll"]
