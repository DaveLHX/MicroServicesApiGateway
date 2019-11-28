FROM mcr.microsoft.com/mssql/server
COPY ./*.sql ./
ENV ACCEPT_EULA Y
ENV sa_password P4$wo0RD!

RUN /opt/mssql/bin/sqlservr --accept-eula \ && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'P4$wo0RD!' -d master -i build-database.sql

#RUN /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "P4$wo0RD!" -i sample-model.sql 
#RUN /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "P4$wo0RD!" -i sample-data.sql


#docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=P4$wo0RD!" -p 1433:1433 --name sql1 -d mcr.microsoft.com/mssql/server:latest
#docker exec -it amazing_sammet "bash"
#/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "P4$wo0RD!"

docker pull mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Duuu988@P4w0RD!" `
-p 1488:1433 --name sql1 `
-d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Duuu988@P4w0RD!"


docker-compose -f docker-compose-direct.yml -f docker-compose.override.yml up