# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and restore as distinct layers
COPY SwapfietsBikeTheftTracker.sln ./
COPY src/SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj src/SF.BikeTheft.WebApi/
COPY src/SF.BikeTheft.Application/SF.BikeTheft.Application.csproj src/SF.BikeTheft.Application/
COPY src/SF.BikeTheft.Common/SF.BikeTheft.Common.csproj src/SF.BikeTheft.Common/
COPY src/SF.BikeTheft.Domain/SF.BikeTheft.Domain.csproj src/SF.BikeTheft.Domain/
COPY src/SF.BikeTheft.Infrastructure/SF.BikeTheft.Infrastructure.csproj src/SF.BikeTheft.Infrastructure/

RUN dotnet restore src/SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj

# Copy everything else and build
COPY src/ .
RUN dotnet publish ./SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj -c Release -o /app/build

# Publish the app to a folder in the container
RUN dotnet publish ./SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "SF.BikeTheft.WebApi.dll"]
