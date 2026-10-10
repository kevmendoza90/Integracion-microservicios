#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MicroServices/Customers/Business.Api/Business.Api.csproj", "MicroServices/Customers/Business.Api/"]
COPY ["MicroServices/Customers/Business.Infraestructure/Business.Infraestructure.csproj", "MicroServices/Customers/Business.Infraestructure/"]
COPY ["Utils/T-Rest/T-Rest.csproj", "Utils/T-Rest/"]
COPY ["MicroServices/Customers/Business.Core/Business.Core.csproj", "MicroServices/Customers/Business.Core/"]
COPY ["Utils/Common/Common.csproj", "Utils/Common/"]
COPY ["MicroServices/Customers/Business.Domain/Business.Domain.csproj", "MicroServices/Customers/Business.Domain/"]
COPY ["MicroServices/Customers/Business.Persistence/Business.Persistence.csproj", "MicroServices/Customers/Business.Persistence/"]
RUN dotnet restore "MicroServices/Customers/Business.Api/Business.Api.csproj"
COPY . .
WORKDIR "/src/MicroServices/Customers/Business.Api"
RUN dotnet build "Business.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Business.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Business.Api.dll"]
