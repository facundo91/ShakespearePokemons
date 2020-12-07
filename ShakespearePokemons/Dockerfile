#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet AS base
WORKDIR /app
EXPOSE 80/tcp
EXPOSE 443/tcp

FROM mcr.microsoft.com/dotnet/sdk AS publish
WORKDIR /src
COPY . .
RUN dotnet restore "ShakespearePokemons/ShakespearePokemons.csproj"
WORKDIR "/src/ShakespearePokemons"
RUN dotnet publish "ShakespearePokemons.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ShakespearePokemons.dll"]