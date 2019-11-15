FROM mcr.microsoft.com/mssql/server:2017-CU14-ubuntu

WORKDIR /sqlserver/

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=0R1o3xsouSHnSUyiRN500q9ksEOA8s

ENV PATH="/opt/mssql-tools/bin:${PATH}"

COPY . .

EXPOSE 1433
