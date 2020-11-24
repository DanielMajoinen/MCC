# Moula Code Challenge

---
Welcome and thanks for checking out my implementation of the Moula Coding Challenge!

## Overview

I have broken this service up into 3 separate layers.

1. API - This is the public entry point and contains no business logic.
2. Business - This contains any business and service layer logic.
3. Data - This is responsible for communicating with the database.

I have chosen to use MSSQL for my data store and I have used NPoco as my ORM. This allows me to define exactly how the data is accessed in a fairly straight-forward manor using stored procedures. The database project includes some demo master data in order to have something to return on a fresh deployment.

I have added comments throughout the code to explain decisions, so this readme will focus on usage.

## Getting Started

1. The first step is pulling the code locally.

    ```git
    git clone https://github.com/DanielMajoinen/MCC.git
    ```

2. Next is getting a MSSQL instance running. If you don't have an existing instance you would like to use, feel free to spin up a docker container:

    ```powershell
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=P@ssword1" -p 1434:1433 -d mcr.microsoft.com/mssql/server:2019-CU8-ubuntu-16.04
    ```

3. Publish the Moula.Database project to the MSSQL instance. NOTE: This database includes master data to help with demonstations. Each publish will wipe any data and return it to the defined master data's state.

4. To get the Web API running, we will need to build and run the service:

    ```powershell
    > cd Moula.Api
    > dotnet restore
    > dotnet build
    > dotnet run --launch-profile dev
    ```

5. The Web API will now be accessible at: `http://localhost:5000/api/account/ledger/1`

## Environments

This service supports environmental configuration. The getting started steps show the process of running the service in developer/debug mode. In order to build and run a production release the following steps must be changed:

1. If you have another MSSQL instance for the Production environment, make sure the connection string in appsettings.json is correct.

2. The following commands can be used to build and run a production release of the Web API:

    ```powershell
    > cd Moula.Api
    > dotnet restore
    > dotnet build --configuration release
    > dotnet run --launch-profile prod
    ```

3. The Web API will now be accessible at: `http://localhost:5002/api/account/ledger/1`

## Running Tests

I have included unit tests that can be run from the solutions directory using: `dotnet test`

I have used Xunit, Moq and AutoFixture to perform these tests.

## Further Improvements

- The process of building, deploying and running could be automated with some PowerShell scripts, with parameters for which environment to use.
- The build process could be completed within containers to remove the requirement of having MS Build tools installed locally.
- Docker-compose could be used to have a simple method of bringing the entire service and it's dependencies online.