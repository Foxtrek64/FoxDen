#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["Backend/FoxDen.Server/FoxDen.Server.csproj", "Backend/FoxDen.Server/"]
RUN dotnet restore "Backend/FoxDen.Server/FoxDen.Server.csproj"
COPY . .
WORKDIR "/src/Backend/FoxDen.Server"
RUN dotnet build "FoxDen.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoxDen.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoxDen.Server.dll"]