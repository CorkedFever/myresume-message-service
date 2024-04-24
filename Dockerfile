#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./Corkedfever.Message.Service/Corkedfever.Message.Service.csproj", "."]
COPY ["./Corkedfever.Message.Business/Corkedfever.Message.Business.csproj", "."]
COPY ["./Corkedfever.Message.Data/Corkedfever.Message.Data.csproj", "."]
RUN dotnet restore "./Corkedfever.Message.Service.csproj"
RUN dotnet restore "./Corkedfever.Message.Business.csproj"
RUN dotnet restore "./Corkedfever.Message.Data.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Corkedfever.Message.Service.csproj" -c %BUILD_CONFIGURATION% -o /app/build
RUN dotnet build "./Corkedfever.Message.Business.csproj" -c %BUILD_CONFIGURATION% -o /app/build
RUN dotnet build "./Corkedfever.Message.Data.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Corkedfever.Message.Service.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false
RUN dotnet publish "./Corkedfever.Message.Business.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false
RUN dotnet publish "./Corkedfever.Message.Data.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Corkedfever.Message.Service.dll"]