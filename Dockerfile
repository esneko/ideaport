FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-alpine AS build
WORKDIR /src
COPY ideaport.csproj dist/
RUN dotnet restore dist/ideaport.csproj
COPY . ./dist
WORKDIR /src/dist
RUN dotnet build ideaport.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ideaport.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ideaport.dll"]
