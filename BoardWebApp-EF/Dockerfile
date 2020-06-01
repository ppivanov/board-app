#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base

#WORKDIR /app

#COPY *.csproj ./
#RUN dotnet restore

#COPY . ./
#RUN dotnet publish -c Release -o out

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
#WORKDIR /app

#COPY --from=build-env /app/out .


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
EXPOSE 08


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["BoardWebApp.csproj", ""]
RUN dotnet restore "BoardWebApp.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "BoardWebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BoardWebApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BoardWebApp.dll"]