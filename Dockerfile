FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /source

COPY *.csproj .

RUN dotnet restore

COPY . .

RUN dotnet publish -c release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /usr/src/app

COPY --from=build /source/out .

ENTRYPOINT ["dotnet", "csharp-api.dll"]