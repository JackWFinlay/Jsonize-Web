FROM microsoft/dotnet
RUN mkdir /Jsonize-Web
WORKDIR /Jsonize-Web
ADD /Jsonize-Web /Jsonize-Web
RUN dotnet restore
EXPOSE 5000
CMD dotnet run