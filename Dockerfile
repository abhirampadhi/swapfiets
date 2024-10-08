FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore any dependencies
COPY src/SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj ./SF.BikeTheft.WebApi/
COPY src/SF.BikeTheft.Application/SF.BikeTheft.Application.csproj ./SF.BikeTheft.Application/
COPY src/SF.BikeTheft.Common/SF.BikeTheft.Common.csproj ./SF.BikeTheft.Common/
COPY src/SF.BikeTheft.Domain/SF.BikeTheft.Domain.csproj ./SF.BikeTheft.Domain/
COPY src/SF.BikeTheft.Infrastructure/SF.BikeTheft.Infrastructure.csproj ./SF.BikeTheft.Infrastructure/

RUN dotnet restore ./SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj

# Copy the remaining files and build the app
COPY src/ .
RUN dotnet build ./SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj -c Release -o /app/build

# Publish the app to a folder in the container
RUN dotnet publish ./SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj -c Release -o /app/publish

# Use the official .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Run the app
ENTRYPOINT ["dotnet", "SF.BikeTheft.WebApi.dll"]
