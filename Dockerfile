FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app

COPY . /app

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app
COPY --from=build-env /app/out .


ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Armut.MS.Api.dll", "--environment=Development"]


