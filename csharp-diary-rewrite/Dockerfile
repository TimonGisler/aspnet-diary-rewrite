# mainly from: https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-8.0#the-dockerfile --> pulled example and looked at complexapp
# This uses multi stage builds https://docs.docker.com/build/building/multi-stage/
# https://hub.docker.com/_/microsoft-dotnet

#!!!!!!! This will create a docker image of my project, but it will not automatically copy the newest version of svelte
#!!!!!!! I have to run "npm run build" in the svelte folder if I want to update the svelte part of the project
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers --> restore = download dependencies
COPY diary/*.csproj diary/
RUN dotnet restore diary/csharp-diary-rewrite.csproj
COPY diary/ diary/

FROM build AS publish
WORKDIR /source/diary
# for some weird reason have to restore again, because otherewise "Package Microsoft.EntityFrameworkCore.Analyzers, version 7.0.10 was not found."
RUN dotnet restore
RUN dotnet publish --no-restore -o /diary

# final stage/image
# runtime for aspnetcore
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /diary
COPY --from=publish /diary .
ENTRYPOINT ["dotnet", "csharp-diary-rewrite.dll"]
