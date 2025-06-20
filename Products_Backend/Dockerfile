FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
#RUN dotnet ef migrations add Init && dotnet ef database update
EXPOSE 7001
CMD ["dotnet", "run"]
