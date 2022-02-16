#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Contact/Contact.API/Contact.API.csproj", "src/Services/Contact/Contact.API/"]
COPY ["src/BuildingBlocks/Common.Middleware/Common.Middleware.csproj", "src/BuildingBlocks/Common.Middleware/"]
COPY ["src/BuildingBlocks/Common.Dto/Common.Dto.csproj", "src/BuildingBlocks/Common.Dto/"]
COPY ["src/BuildingBlocks/Common.Helpers/Common.Helpers.csproj", "src/BuildingBlocks/Common.Helpers/"]
COPY ["src/Services/Contact/Contact.Infrastructure/Contact.Infrastructure.csproj", "src/Services/Contact/Contact.Infrastructure/"]
COPY ["src/Services/Contact/Contact.Application/Contact.Application.csproj", "src/Services/Contact/Contact.Application/"]
COPY ["src/Services/Contact/Contact.Domain/Contact.Domain.csproj", "src/Services/Contact/Contact.Domain/"]
COPY ["src/BuildingBlocks/Common.Messages/Common.Messages.csproj", "src/BuildingBlocks/Common.Messages/"]
RUN dotnet restore "src/Services/Contact/Contact.API/Contact.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Contact/Contact.API"
RUN dotnet build "Contact.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contact.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contact.API.dll"]
