#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RaduMVC.csproj", "."]
RUN dotnet restore "./RaduMVC.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RaduMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RaduMVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Run the app on container startup
# ENTRYPOINT [ "dotnet", "RazorMvc.dll" ]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet RaduMVC.dll