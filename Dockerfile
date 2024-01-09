FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./

RUN apt-get update && \
  apt-get install make -y

RUN make restore && \
  make publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "Satellite.API.dll"]
