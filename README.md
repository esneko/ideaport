# ideaport

[ASP.NET Core 2.1](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.1) web app that authenticates a user against [Azure AD B2C identity service](https://azure.microsoft.com/en-us/services/active-directory-b2c/) using an [OpenID Connect bearer token](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/) and validates an access to the web API according to the claims included in the JWT token.

## Usage

### [.NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x)

Restore the application dependencies and run the Kestrel web server:
```bash
$ dotnet restore
$ dotnet run
```

### [Docker Container](https://docs.docker.com/)

Build a Docker image that includes the application with all of its dependencies and run the image as a container:
```bash
$ docker-compose up
```

Or pull the pre-built image from [Docker Hub](https://hub.docker.com/r/esneko/ideaport/) and run the image with mapping host machine’s port 5000 to the container’s published port 80:
```bash
docker pull esneko/ideaport
docker run --rm -p 5000:80 esneko/ideaport:latest
```

### [Azure App Service](https://azure.microsoft.com/en-us/services/app-service)

The image deployed from [Docker Hub](https://hub.docker.com/r/esneko/ideaport/) to [Azure Web App for Containers](https://azure.microsoft.com/en-us/services/app-service/containers/) is [available](http://ideaport.azurewebsites.net/).

## Web API

Obtain a token by signing in on [Azure AD B2C login page](https://esneko.b2clogin.com/esneko.onmicrosoft.com/oauth2/v2.0/authorize?p=B2C_1_siup&client_id=9d72ceaa-1b9c-46e4-a4c9-76fcf11134a7&nonce=defaultNonce&redirect_uri=https%3A%2F%2Fjwt.ms%2F&scope=openid&response_type=id_token&prompt=login):
```bash
Email: esneko@gmail.com
Password: qwerty123$
```

Replace the `{token}` with one obtained from the link above and try to access the web API via curl:

```bash
curl http://ideaport.azurewebsites.net/api/users -i --header "Authorization: Bearer {token}"
```
