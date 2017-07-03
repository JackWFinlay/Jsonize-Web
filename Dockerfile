FROM microsoft/dotnet
RUN mkdir /Jsonize-Web
WORKDIR /Jsonize-Web
ADD /Jsonize-Web /Jsonize-Web
RUN dotnet restore
EXPOSE 5000
ENV ASPNETCORE_ENVIRONMENT="Development"
WORKDIR /Jsonize-Web/src/Jsonize-Web
CMD /bin/sh -c 'dotnet run'