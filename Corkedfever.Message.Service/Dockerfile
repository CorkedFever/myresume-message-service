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
RUN DIR
COPY . .
RUN DIR
RUN dotnet nuget add source "http://192.168.1.4:8090/v3/index.json" --name "localbaget"
RUN dotnet restore "./Corkedfever.Message.Service/Corkedfever.Message.Service.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Corkedfever.Message.Service/Corkedfever.Message.Service.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Corkedfever.Message.Service/Corkedfever.Message.Service.csproj" -c %BUILD_CONFIGURATION% -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Corkedfever.Message.Service.dll"]