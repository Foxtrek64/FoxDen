FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FoxDen.API/FoxDen.API.csproj", "FoxDen.API/"]
RUN dotnet restore "FoxDen.API/FoxDen.API.csproj"
COPY . .
WORKDIR "/src/FoxDen.API"
RUN dotnet build "FoxDen.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoxDen.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoxDen.API.dll"]
