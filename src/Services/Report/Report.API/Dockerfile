#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Report/Report.API/Report.API.csproj", "src/Services/Report/Report.API/"]
COPY ["src/BuildingBlocks/Common.Helpers/Common.Helpers.csproj", "src/BuildingBlocks/Common.Helpers/"]
COPY ["src/BuildingBlocks/Common.Dto/Common.Dto.csproj", "src/BuildingBlocks/Common.Dto/"]
COPY ["src/BuildingBlocks/Common.Messages/Common.Messages.csproj", "src/BuildingBlocks/Common.Messages/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "src/Services/Report/Report.API/Report.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Report/Report.API"
RUN dotnet build "Report.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Report.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Report.API.dll"]
