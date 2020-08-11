#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["OrdersProcessingService.Api/OrdersProcessingService.Api.csproj", "OrdersProcessingService.Api/"]
COPY ["OrdersProcessingService.Infrastructure/OrdersProcessingService.Infrastructure.csproj", "OrdersProcessingService.Infrastructure/"]
COPY ["OrdersProcessingService.Core/OrdersProcessingService.Core.csproj", "OrdersProcessingService.Core/"]
RUN dotnet restore "OrdersProcessingService.Api/OrdersProcessingService.Api.csproj"
COPY . .
WORKDIR "/src/OrdersProcessingService.Api"
RUN dotnet build "OrdersProcessingService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrdersProcessingService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrdersProcessingService.Api.dll"]
EXPOSE 80