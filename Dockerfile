FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["EHR.Service.Api/EHR.Service.Api.csproj", "EHR.Service.Api/"]
COPY ["EHR.Service.Core/EHR.Service.Core.csproj", "EHR.Service.Core/"]
COPY ["EHR.Service.Services/EHR.Service.Services.csproj", "EHR.Service.Services/"]

RUN dotnet restore "EHR.Service.Api/EHR.Service.Api.csproj"

COPY . .

WORKDIR "/src/EHR.Service.Api"
RUN dotnet build "EHR.Service.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EHR.Service.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "EHR.Service.Api.dll"]