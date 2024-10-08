# Use an official .NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK for build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/WebAPI/SF.BikeTheft.WebApi.csproj"
RUN dotnet build "src/WebAPI/SF.BikeTheft.WebApi.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "src/WebAPI/SF.BikeTheft.WebApi.csproj" -c Release -o /app/publish

# Final stage for runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SF.BikeTheft.WebApi.dll"]
