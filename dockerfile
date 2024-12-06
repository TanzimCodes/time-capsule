# Use the official .NET 9 SDK image to build the app (for building)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project file(s) and restore the dependencies
COPY *.csproj ./ 

RUN dotnet restore

# Copy the rest of the application code
COPY . ./


# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET 9 runtime image to run the app (runtime-only)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base


# Set the working directory for the container's runtime environment
WORKDIR /app

# Copy the build artifacts from the build image
COPY --from=build /app/publish . 

# Expose the port that the app will listen on
EXPOSE 80

# Define the entry point to run the application
ENTRYPOINT ["dotnet", "api.dll"]
