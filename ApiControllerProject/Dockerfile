# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ApiControllerProject.csproj", "./"]
RUN dotnet restore "./ApiControllerProject.csproj"
COPY . .
RUN dotnet publish "ApiControllerProject.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ApiControllerProject.dll"]