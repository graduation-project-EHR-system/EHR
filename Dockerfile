FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["EHR/EHR.csproj", "EHR/"]
COPY ["EHR.Core/EHR.Core.csproj", "EHR.Core/"]
COPY ["EHR.Service/EHR.Service.csproj", "EHR.Service/"]

RUN dotnet restore "EHR/EHR.csproj"

COPY . .

WORKDIR "/src/EHR"
RUN dotnet build "EHR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EHR.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "EHR.dll"]