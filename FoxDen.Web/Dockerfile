FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FoxDen.Web/FoxDen.Web.csproj", "FoxDen.Web/"]
RUN dotnet restore "FoxDen.Web/FoxDen.Web.csproj"
COPY . .
WORKDIR "/src/FoxDen.Web"
RUN dotnet build "FoxDen.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoxDen.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoxDen.Web.dll"]
