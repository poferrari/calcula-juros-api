FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5008

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY . /src
RUN dotnet restore
COPY . .
WORKDIR "/src/CalculaJuros.Api"
RUN dotnet build "CalculaJuros.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalculaJuros.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalculaJuros.Api.dll"]

