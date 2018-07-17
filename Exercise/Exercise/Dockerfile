FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 65333
EXPOSE 44352

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Exercise/Exercise.csproj Exercise/
RUN dotnet restore Exercise/Exercise.csproj
COPY . .
WORKDIR /src/Exercise
RUN dotnet build Exercise.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Exercise.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Exercise.dll"]
