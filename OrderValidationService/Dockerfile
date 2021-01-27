FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish OrderValidationService.csproj -c Develop -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
COPY ./https/devcert.pfx ./https/devcert.pfx

ENV ASPNETCORE_Kestrel__Certificates__Default__Password=Password
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=./https/devcert.pfx
ENTRYPOINT ["dotnet", "OrderValidationService.dll"]

