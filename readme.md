https://visualstudiomagazine.com/articles/2019/03/01/dealing-with-databases.aspx

docker pull microsoft/mssql-server-linux:latest


todo:

* Serilog works, had to put connection string case sensitive
* health check works
* swagger works
* Seq works
	* add api keys, filter information,add Applied properties and then add column to new signal.


- Move health check ui to apigateway.
- Add health check to all projects
- Add serilog to all projects. (first test what happens on gateway only.)
- clean up docker compose, settings.json
- update doc
- import CSV
- jwt token
- autofac???

**add links page to the health site

Health:
http://localhost:5557/healthchecks-ui
SEQ
http://localhost:5556/
db
http://localhost:5555/

Api gateway:
http://localhost:5540/getCadet/{cadetId}
http://localhost:5540/Cadet/{everything}
http://localhost:5540/Cadet/getCadet/{cadetId}    
http://localhost:5540/Reference/Military/Ranks   //From commonlistsApi    
http://localhost:5540/Reference/Military/Elements/fr  //from CSAR
http://localhost:5540/CadetAndRanks/88/fr //From commonlistsApi ,CadetApi and CSAR api 

http://localhost:5541/swagger