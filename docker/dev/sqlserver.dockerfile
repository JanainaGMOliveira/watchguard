FROM mcr.microsoft.com/mssql/server:2017-CU14-ubuntu

WORKDIR /sqlserver/

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=W@tchgu@rd2019

ENV PATH="/opt/mssql-tools/bin:${PATH}"

COPY . .

EXPOSE 1433
