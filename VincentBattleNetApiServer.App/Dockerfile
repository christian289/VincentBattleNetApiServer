# DockerFile 위치 -> VincentBattleNetApiServer.App\Dockerfile
# docker build 명령어 실행 위치(build context) -> VincentBattleNetApiServer
# docker build 명령어 -> docker build -t battle-net-api -f VincentBattleNetApiServer.App/Dockerfile .
# docker 컨테이너 실행 명령어 -> docker run -d -p 8080:80 -p 8081:443 --name {컨테이너 이름} {Docker 이미지 이름}
# docker 컨테이너 개발 모드 실행 명령어 -> docker run -e ASPNETCORE_ENVIRONMENT=Development -d -p 8080:80 -p 8081:443 --name {컨테이너 이름} {Docker 이미지 이름}

# 이 Dockerfile은 .NET SDK 9.0 docker image를 사용한 build라고 불리는 첫번째 build stage와
# aspnet 9.0 docker image를 사용한 두번째 build stage로 구분되어 있다.
# 2개의 build stage로 구분히는 이유는 첫번째 build stage에서는 dotnet cli를 이용해서 .NET Application을 build하는데
# 이 과정에서 exe와 obj등 최종 어셈블리에는 필요없는 파일들이 생성된다.
# 따라서 첫번째 build stage에서는 최종 어셈블리를 만드는 역할만 한다.
# 두번째 build stage에서는 --from=build 옵션을 지정한 COPY를 이용하는 것이다.
# --from 옵션을 이전 빌드 스테이지를 참조하는 것인데 첫번째 build stage 이름이 build이므로 --from=build로 지정된 것이다.
# 그리고 첫번째 build stage에서 결과물이 생성되었고, 더이상 dotnet 명령어를 이용할 필요가 없으므로 .NET SDK를 사용한 첫번째 build stage를 버리는 의미로
# 두번째 aspnet 9.0 image를 사용한 두번째 build stage를 정의하는 것이다.
# 만약에 두번째 build stage를 정의하지 않고 첫번째 build stage를 계속 이용해서 docker image를 생성하게되면 .NET SDK 9.0 이미지 위에서 docker image가 생성되기 때문에
# 용량이 매우 크다. 참고로 .NET SDK 9.0 image의 용량은 829MB이며, aspnet9.0 image의 용량은 224MB이다.
# 따라서 두번째 build stage를 정의하면서 aspnet:9.0 image를 지정하여 사용하면 기본 용량이 224MB에서 시작하는 것이다.
# aspnet:9.0 image는 image를 빌드할 수는 없고 실행만 가능한 stage다. aspnet:9.0 image는 빌드를 하는 기능은 빼고 Microsoft가 배포했기 때문이다.

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# 이 프로젝트는 ASP.NET Core NativeAOT 프로젝트다. NativeAOT는 빌드할 때 .NET 어플리케이션 하나로 이곳저곳의 시스템에서 실행할 수 있는 JIT 컴파일과 달리, 처음부터 특정 OS 대상으로 컴파일 해야한다.
# 따라서 Linux 환경에서 실행할 이미지는 Linux 버전으로 컴파일 해야한다.
# 이 링크에 보면 Linux와 Windows때 서로 다른 DockerFile을 확인할 수 있다. https://learn.microsoft.com/ko-kr/dotnet/core/deploying/native-aot/?tabs=net7%2Cwindows#publish-native-aot-using-the-cli
# Linux: https://github.com/dotnet/samples/blob/main/core/nativeaot/HelloWorld/Dockerfile
# Windows: https://github.com/dotnet/samples/blob/main/core/nativeaot/HelloWorld/Dockerfile.windowsservercore-x64
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
       clang zlib1g-dev

WORKDIR /src
COPY ["./VincentBattleNetApiServer.Core/VincentBattleNetApiServer.Core.csproj", "VincentBattleNetApiServer.Core/"]
WORKDIR /src/VincentBattleNetApiServer.Core
RUN dotnet restore
WORKDIR /src
COPY ["./VincentBattleNetApiServer.App/VincentBattleNetApiServer.App.csproj", "VincentBattleNetApiServer.App/"]
WORKDIR /src/VincentBattleNetApiServer.App
RUN dotnet restore
WORKDIR /src
COPY ./VincentBattleNetApiServer.App/ VincentBattleNetApiServer.App/
COPY ./VincentBattleNetApiServer.Core/ VincentBattleNetApiServer.Core/
WORKDIR /src/VincentBattleNetApiServer.App
RUN dotnet publish -r linux-x64 -c Release -o /app/publish

# 일반적인 .NET Application이라면 아래와 같이 빌드한다.
# FROM mcr.microsoft.com/dotnet/aspnet:9.0
# WORKDIR /app
# COPY --from=build /app/publish .
# ENV ASPNETCORE_ENVIRONMENT=Development
# EXPOSE 80
# EXPOSE 443
# ENTRYPOINT ["dotnet", "VincentBattleNetApiServer.App.dll"]

# ASP.NET Core NativeAOT 프로젝트는 아래와 같이 빌드한다.
FROM mcr.microsoft.com/dotnet/runtime-deps:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 80
EXPOSE 443
ENTRYPOINT [ "/app/VincentBattleNetApiServer.App" ]