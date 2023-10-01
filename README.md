# Code challenge 

## Description

Most team sports have a depth chart (a ranking of each player) for each position they have. This project allows to add and update a player position in the depth chart through a series of APIs.

This project contains following APIs:<br/><br/>
POST   : `/sport/{sportId}/player` -> Adds a player and updates the depth chart.<br/>
DELETE : `/sport/{sportId}/player/{playerId}` -> Deletes a player and updates the depth chart.<br/>
GET  : `/sport/{sportId}/depthchart` ->  Retrieves all the depthcharts for the sport.<br/>
GET  : `/sport/{sportId}/depthchart?position=WR&playerId=2` ->  Retrieves players behind the given player in the query for the position.<br/>

Note: Only two sports are supported 
 - NFL - sportId value is 1
 - MLB - sportId value is 2
Implementing CRUD operations on Sport can extend this API to support multiple sports in future.

## Getting Started

### Dependencies

* .Net 6 sdk
* Windows, Mac, Linux
* Azure Cosmos DB (Personal subscription on free trail is used) [No action required]

### Installing

* Clone this repo
  ```sh
  git clone https://github.com/soma-potta/CodeTest.git
  ```
* Ensure when running the below command shows .Net 6 installed.
  ```sh
  dotnet --list-sdks
  ```

### Executing program

#### Local

* Copy Cosmos primary key from the email(sent with the github link) in to appsettings.json and replace text `<copy-cosmos-primary-key-here>` 

* Open terminal from the folder containing CodeChallenge.csproj file and run below command:
  ```sh
  dotnet run
  ```
  The above command restores and builds the project. If the build is successful it runs the API on url `https://localhost:7068`. If the port is not available then update it in launchSettings.json file.

  Go to swagger: https://localhost:7068/index.html to test on local machine


#### Live version

## Assumptions 

* Player depth in position starts from 0.<br/>
  eg: Player in the first spot will have position_depth = 0.<br/>
      If a player is be added to second spot then provide position_depth = 1.

* If player_depth is more than the count of players in the position, then that player will be added to the end of the list.

* PlayerId is unique across sport.

* Get players behind the player query takes playerId instead of name. This avoids confusion if two players have the same name.

* This API is not authenticated. Anyone can access this.