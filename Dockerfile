## AccessControl
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5140

ENV ASPNETCORE_URLS=http://+:5140

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-api

COPY . ./

RUN dotnet restore "src/AccessControl/AccessControl.csproj"
RUN dotnet build "src/AccessControl/AccessControl.csproj" -c Release -o /app/build

FROM build-api AS publish
RUN dotnet publish "src/AccessControl/AccessControl.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccessControl.dll"]
