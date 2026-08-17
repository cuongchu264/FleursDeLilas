FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["FleursDeLilas.API/FleursDeLilas.API.csproj", "FleursDeLilas.API/"]
RUN dotnet restore "FleursDeLilas.API/FleursDeLilas.API.csproj"

COPY . .
WORKDIR "/src/FleursDeLilas.API"
RUN dotnet publish "FleursDeLilas.API.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "FleursDeLilas.API.dll"]