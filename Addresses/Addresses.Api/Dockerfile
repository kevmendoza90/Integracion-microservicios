#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MicroServices/Addresses/Addresses.Api/Addresses.Api.csproj", "MicroServices/Addresses/Addresses.Api/"]
COPY ["MicroServices/Addresses/Addresses.Core/Addresses.Core.csproj", "MicroServices/Addresses/Addresses.Core/"]
COPY ["Utils/Common/Common.csproj", "Utils/Common/"]
COPY ["MicroServices/Addresses/Addresses.Domain/Addresses.Domain.csproj", "MicroServices/Addresses/Addresses.Domain/"]
COPY ["MicroServices/Addresses/Addresses.Persistence/Addresses.Persistence.csproj", "MicroServices/Addresses/Addresses.Persistence/"]
RUN dotnet restore "MicroServices/Addresses/Addresses.Api/Addresses.Api.csproj"
COPY . .
WORKDIR "/src/MicroServices/Addresses/Addresses.Api"
RUN dotnet build "Addresses.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Addresses.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Addresses.Api.dll"]
