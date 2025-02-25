FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
COPY Public ./Public
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["blog.csproj", "./"]

RUN dotnet restore "blog.csproj"
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "blog.dll"]