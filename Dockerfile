FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
ENV ASPNETCORE_ENVIRONMENT=Development

# Klasörleri Docker imajına kopyalayın

WORKDIR /app

COPY . /app

# Projeleri derleyin
RUN dotnet publish -c Release -o out

# .NET çalıştırma zamanı tabanlı Docker imajını kullanın
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Uygulama dosyalarını Docker imajına kopyalayın
WORKDIR /app
COPY --from=build-env /app/out .


ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Armut.MS.Api.dll", "--environment=Development"]


