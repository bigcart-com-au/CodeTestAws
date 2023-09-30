# Project Title

Code challenge 

## Description

Most team sports have a depth chart (a ranking of each player) for each position they have. This project allows to add a player and update player position in the depth chart through a series of APIs.

This project contains following APIs:<br/>
POST   : `/sport/{sportId}/player` -> Adds a player and updates the depth chart.<br/>
DELETE : `/sport/{sportId}/player/{playerId}` -> Deletes a player and updates the depth chart.<br/>
GET  : `/sport/{sportId}/depthchart` ->  Retrieves all the depthcharts for the sport.<br/>
GET  : `/sport/{sportId}/depthchart?position=WR&playerId=2` ->  Retrieves players behind the given player in the query for the position.<br/>


## Getting Started

### Dependencies

* .Net 6 SDK
* Windows, Mac, Linux

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

* Open terminal from the folder containing CodeChallenge.csproj file and run below command:
  ```sh
  dotnet run
  ```
  The above command restores and builds the project. If the build is successful it runs the API in localhost

## Help

Any advise for common problems or issues.