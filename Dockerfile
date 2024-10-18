# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the solution file and restore as distinct layers
COPY SwapfietsBikeTheftTracker.sln ./
COPY src/SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj src/SF.BikeTheft.WebApi/

RUN dotnet restore src/SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish src/SF.BikeTheft.WebApi/SF.BikeTheft.WebApi.csproj -c Release -o out

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose port (if needed)
EXPOSE 5000

# Set the entry point
ENTRYPOINT ["dotnet", "SF.BikeTheft.WebApi.dll"]
