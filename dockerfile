FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY . .
RUN dotnet restore

WORKDIR /app/CheckoutTechTest.WebApi
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/CheckoutTechTest.WebApi/out ./

ENTRYPOINT ["dotnet", "CheckoutTechTest.WebApi.dll"]