#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MVCproject/MVCproject.csproj", "MVCproject/"]
COPY ["CoreLib/CoreLib.csproj", "CoreLib/"]
COPY ["DataBase/DataBase.csproj", "DataBase/"]
RUN dotnet restore "MVCproject/MVCproject.csproj"
COPY . .
WORKDIR "/src/MVCproject"
RUN dotnet build "MVCproject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MVCproject.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MVCproject.dll"]