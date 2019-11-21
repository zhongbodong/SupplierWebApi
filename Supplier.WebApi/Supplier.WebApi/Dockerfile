FROM registry-vpc.cn-shenzhen.aliyuncs.com/mytools/dotnetsdk:3.0-runtime

RUN mkdir /WebApi

COPY Scafflod.WebApi/release /WebApi

WORKDIR /WebApi

EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Production

CMD ["dotnet", "Scaffold.WebApi.dll"]
