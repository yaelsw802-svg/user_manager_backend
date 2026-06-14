# כדי לבנות את האפליקציה משתמש במכנה הבסיסית של .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# מעתיק את קבצי הפרויקט ומבצע restore
COPY *.sln ./
COPY ClothingStoreApi.API/*.csproj ClothingStoreApi.API/
COPY ClothingStoreApi.Core/*.csproj ClothingStoreApi.Core/
COPY ClothingStoreApi.Data/*.csproj ClothingStoreApi.Data/
COPY ClothingStoreApi.Service/*.csproj ClothingStoreApi.Service/
RUN dotnet restore

# מעתיק את שאר הקבצים ובונה את האפליקציה
COPY . ./
RUN dotnet publish Clean.API -c Release -o out

# כדי להריץ את האפליקציה משתמש במכנה של .NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# מגדיר את הפורט שהאפליקציה תאזין עליו
EXPOSE 80

# מפעיל את האפליקציה
ENTRYPOINT ["dotnet", "ClothingStoreApi.API.dll"]