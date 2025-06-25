# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore "./ParInpar.csproj"
RUN dotnet publish "./ParInpar.csproj" -c Release -o /out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "ParInpar.dll"]
