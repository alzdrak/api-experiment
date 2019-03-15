FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY bookings.api/bookings.api.csproj bookings.api/
RUN dotnet restore bookings.api/bookings.api.csproj
COPY . .
WORKDIR /src/bookings.api
RUN dotnet build bookings.api.csproj -c Release -o /app
RUN dotnet dev-certs https

FROM build AS publish
RUN dotnet publish bookings.api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "bookings.api.dll"]
