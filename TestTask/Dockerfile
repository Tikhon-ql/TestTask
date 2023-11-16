#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TestTask/TestTask.csproj", "TestTask/"]
COPY ["TestTask.Logic/TestTask.Logic.csproj", "TestTask.Logic/"]
COPY ["TestTask.Common/TestTask.Common.csproj", "TestTask.Common/"]
COPY ["TestTask.DataProvider/TestTask.DataProvider.csproj", "TestTask.DataProvider/"]
RUN dotnet restore "TestTask/TestTask.csproj"
COPY . .
WORKDIR "/src/TestTask"
RUN dotnet build "TestTask.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTask.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTask.dll"]