# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory
WORKDIR /app

# Copy the .csproj and restore any dependencies
COPY src/SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj ./SF.BikeTheft.WebApi/
RUN dotnet restore ./SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj

# Copy the rest of the application code
COPY . ./

# Build the application
RUN dotnet publish ./SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj -c Release -o out

# Build the final image with the runtime only
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory in the final image
WORKDIR /app

# Copy the published output from the build environment
COPY --from=build-env /app/out .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "SF.BikeTheft.WebApi.dll"]
