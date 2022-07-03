# Set the base image as the .NET 6.0 SDK (this includes the runtime)
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env

# Copy everything and publish the release (publish implicitly restores and builds)
COPY . ./
RUN dotnet publish ./Custom-Action-Console/Custom-Action-Console.csproj -c Release -o out --no-self-contained

# Label the container
LABEL maintainer="Joshua Jesper Kraegpoeth Ryder <josh@topswagcode.com>"
LABEL repository="https://github.com/TopSwagCode-dev/Custom-Action"
LABEL homepage="https://github.com/TopSwagCode-dev/Custom-Action"

# Label as GitHub action
LABEL com.github.actions.name="Github Release Metrics"
LABEL com.github.actions.description="A Github action that publishes metrics regarding releases"
LABEL com.github.actions.icon="sliders"
LABEL com.github.actions.color="purple"

# Relayer the .NET SDK, anew with the build output
FROM mcr.microsoft.com/dotnet/sdk:6.0
COPY --from=build-env /out .
ENTRYPOINT [ "dotnet", "/Custom-Action-Console.dll" ]