FROM ubuntu:18.04

LABEL description="Devspaces implementation for KestrelHttpServer"

RUN apt-get update \
 && apt-get install -y git \
        libcurl3 \
        libicu60 \
        liblttng-ust0 \
        libkrb5-3 \        
        libssl1.0.0 \        
        libunwind-dev \
        nginx \        
        python3 \
        unzip \
        wget \        
        zlib1g \
 && apt-get clean

COPY docker-start.sh docker-nginx.conf /etc/

RUN chmod 777 /etc/docker-start.sh

WORKDIR /data

ENV PATH=$PATH:/root/.dotnet
ENV ASPNETCORE_ENVIRONMENT Development

EXPOSE 50001

CMD tail -f /dev/null