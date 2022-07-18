#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/Email/BookingApp.Email/BookingApp.Email.csproj", "services/Email/BookingApp.Email/"]
RUN dotnet restore "services/Email/BookingApp.Email/BookingApp.Email.csproj"
COPY . .
WORKDIR "/src/services/Email/BookingApp.Email"
RUN dotnet build "BookingApp.Email.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookingApp.Email.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingApp.Email.dll"]