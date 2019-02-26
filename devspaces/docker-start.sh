#!/usr/bin/env bash
/data/KestrelHttpServer/build.sh /p:SkipTests=true
nginx -c /etc/docker-nginx.conf
dotnet $1