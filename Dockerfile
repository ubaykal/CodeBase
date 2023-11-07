
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Presentation/ECommerce.API/ECommerce.API.csproj", "Presentation/ECommerce.API/"]
COPY ["Infrastructure/ECommerce.Infrastructure/ECommerce.Infrastructure.csproj", "Infrastructure/ECommerce.Infrastructure/"]
COPY ["Core/ECommerce.Application/ECommerce.Application.csproj", "Core/ECommerce.Application/"]
COPY ["Core/ECommerce.Domain/ECommerce.Domain.csproj", "Core/ECommerce.Domain/"]
COPY ["Infrastructure/ECommerce.Persistence/ECommerce.Persistence.csproj", "Infrastructure/ECommerce.Persistence/"]
RUN dotnet restore "Presentation/ECommerce.API/ECommerce.API.csproj"
COPY . .
WORKDIR "ECommerce.API"
RUN dotnet build "Presentation/ECommerce.API/ECommerce.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.API.dll"]