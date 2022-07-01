FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Custom-Action-Console/Custom-Action-Console.csproj", "Custom-Action-Console/"]
RUN dotnet restore "Custom-Action-Console/Custom-Action-Console.csproj"
COPY . .
WORKDIR "/src/Custom-Action-Console"
RUN dotnet build "Custom-Action-Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Custom-Action-Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Custom-Action-Console.dll"]
