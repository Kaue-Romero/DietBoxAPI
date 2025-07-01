# Etapa base com SDK para uso geral e EF CLI
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Instala a ferramenta dotnet-ef globalmente
RUN dotnet tool install --global dotnet-ef \
    && echo 'export PATH="$PATH:/root/.dotnet/tools"' >> /root/.bashrc

ENV PATH="$PATH:/root/.dotnet/tools"

# Etapa de build
FROM base AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DietBoxAPI/DietBoxAPI.csproj", "DietBoxAPI/"]
RUN dotnet restore "DietBoxAPI/DietBoxAPI.csproj"
COPY . .
WORKDIR "/src/DietBoxAPI"
RUN dotnet build "DietBoxAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DietBoxAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DietBoxAPI.dll"]
