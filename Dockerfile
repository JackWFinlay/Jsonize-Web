FROM microsoft/dotnet
RUN mkdir /Jsonize-Web
WORKDIR /Jsonize-Web
ADD /Jsonize-Web /Jsonize-Web
RUN dotnet restore
CMD dotnet run