#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VentanillaUnica.Tramites.Api/VentanillaUnica.Tramites.Api/VentanillaUnica.Tramites.Api.csproj", "VentanillaUnica.Tramites.Api/VentanillaUnica.Tramites.Api/"]
COPY ["VentanillaUnica.Tramites.Dtos/VentanillaUnica.Tramites.Dtos.csproj", "VentanillaUnica.Tramites.Dtos/"]
COPY ["VentanillaUnica.Tramites.Application/VentanillaUnica.Tramites.Application.csproj", "VentanillaUnica.Tramites.Application/"]
COPY ["VentanillaUnica.Tramites.Domain/VentanillaUnica.Tramites.Domain.csproj", "VentanillaUnica.Tramites.Domain/"]
COPY ["VentanillaUnica.Tramites.Domain.Entities/VentanillaUnica.Tramites.Domain.Entities.csproj", "VentanillaUnica.Tramites.Domain.Entities/"]
COPY ["VentanillaUnica.Tramites.Common/VentanillaUnica.Tramites.Common.csproj", "VentanillaUnica.Tramites.Common/"]
COPY ["VentanillaUnica.Tramites.Data/VentanillaUnica.Tramites.Data.csproj", "VentanillaUnica.Tramites.Data/"]
RUN dotnet restore "VentanillaUnica.Tramites.Api/VentanillaUnica.Tramites.Api/VentanillaUnica.Tramites.Api.csproj"
COPY . .
WORKDIR "/src/VentanillaUnica.Tramites.Api/VentanillaUnica.Tramites.Api"
RUN dotnet build "VentanillaUnica.Tramites.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VentanillaUnica.Tramites.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VentanillaUnica.Tramites.Api.dll"]