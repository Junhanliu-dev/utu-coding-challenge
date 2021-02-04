# utu-coding-challenge

## RDS Postgres database performed by AWS
----
#### The database is established using RDS postgres database service. In order to make the whole service running on local environment, the connection string should be replaced.
#### The connection string is provided in the email for security concerns.

## How to run the project on local environment
---
### 1. clone the project with git or download the project.
### 2. make sure you have dotnet and npm installed on local environment
* if there is no dotnet and npm environment on your machine.
  * Download dotnet core SDK through this [link](https://dotnet.microsoft.com/download).
  * Download Nodejs SDK through this link [link](https://nodejs.org/en/).
### 3. Setup the dotnet and npm environment
* Under root folder of the project, run ``` dotnet restore ``` in the terminal to install the dependencies.
* Under client-app folder, run ``` npm install ``` in the terminal to install the npm dependencies.
### 4. Replace the Connection string.
* Under the /API folder, replace appsettings.json with the one provided in the email.
### 5. Start the server and front-end React application
* Under the /API folder, in the terminal, run ```dotnet run ``` to start the API server.
* Under the /client-app folder, in the terminal, run ```npm start ``` to start the application.

## Others
### Once the project has started on the local machine, the exposed API end points can be seen through the https://localhost:5001/swagger/index.html
### The Caching strategy is explained on the Caching strategy.docx under the root folder.
