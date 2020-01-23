FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build

WORKDIR /src

COPY uploadFile.sln ./
COPY Util/*.csproj ./Util/
COPY storage.Client/*.csproj ./storage.Client/
COPY uploadFile.Core/*.csproj ./uploadFile.Core/

RUN dotnet restore

COPY . .

WORKDIR /src/Util
RUN dotnet build -c Release -o /app

WORKDIR /src/storage.Client
RUN dotnet build -c Release -o /app

WORKDIR /src/uploadFile.Core
RUN dotnet build -c Release -o /app

FROM build AS publish

RUN dotnet publish -c Release -o /app

FROM base AS final

ENV TZ=America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

WORKDIR /app

RUN mkdir UploadTemp

RUN mkdir DownloadTemp

RUN mkdir Keys

COPY ./storage.Client/Keys ./Keys

COPY --from=publish /app .
ENTRYPOINT ["dotnet", "uploadFile.Core.dll"]