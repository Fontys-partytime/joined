#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Partytime.Joined.Service/Partytime.Joined.Service.csproj", "src/Partytime.Joined.Service/"]
COPY ["src/Partytime.Joined.Contracts/Partytime.Joined.Contracts.csproj", "src/Partytime.Joined.Contracts/"]
RUN dotnet restore "src/Partytime.Joined.Service/Partytime.Joined.Service.csproj"
COPY . .
WORKDIR "/src/src/Partytime.Joined.Service"
RUN dotnet build "Partytime.Joined.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Partytime.Joined.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Partytime.Joined.Service.dll"]