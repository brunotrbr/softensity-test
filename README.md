# softsensity-test
Softensity Backend Technical Test

[![Contributors][contributors-shield]][contributors-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<p align="center">
  <h3 align="center">Softensity: handle control access registering doors and cards</h3>

  <p align="center">
    Softensity technical test
    <br />
</p>

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Content</summary>
  <ol>
    <li>
      <a href="#the-problem">The problem</a>
    </li>
    <li>
      <a href="#the-solution">The solution</a>
      <ul>
        <li><a href="#database">Database</a></li>
        <li><a href="#class-modelling">Class modelling</a></li>
        <li><a href="#pre-requisites">Pre-requisites</a></li>
        <li><a href="#preparing-the-environment">Preparing the environment</a></li>
        <li><a href="#running-the-program">Running the program</a></li>
        <li><a href="#tests">Tests</a></li>
      </ul>
    </li>
    <li><a href="#additional-considerations">Additional considerations</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
  </ol>
</details>

&nbsp;

## The problem
How can I control access to secure doors considering a user card? Use the example repo as starting point (net-main.zip) in the root directory.

&nbsp;

## The solution
Refactoring the example repo API to handle access control, considering best practices of .NET programming.

The program were created using .NET 8 with Visual Studio IDE. The endpoins could be tested using postman, insomnia or swagger.

&nbsp;

### Database

The database chosen was Entity Framework In Memory.


&nbsp;

### Class modelling 

The classes Card and Door were not changed. It was just added getters, setters and "Key" annotation to Entity Framework understand the database key.

&nbsp;

### Pre-requisites
    .NET 8.0 (Installed dotnet version was 9.0.300. Not tested with other versions). URL: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

    Docker (Docker version 28.1.1. Not tested with other versions). URL: https://docs.docker.com/get-docker/
    
    Docker Compose (Docker Compose version v2.35.1-desktop.1. Not tested with other versions). URL: https://docs.docker.com/compose/install/

&nbsp;

### Preparing the environment

*The installation of .NET/.NET CLI, docker and docker-compose are not covered in this readme. Please follow the instructions using the documentation above.*

&nbsp;

Clone the project at repository url https://github.com/brunotrbr/softensity-test and go to `softensity-test` dir:

```
[Windows]
PS C:\> git clone git@github.com:brunotrbr/softensity-test.git
PS C:\> cd softensity-test

---------------------------------------------------------------------------

[Linux]
user@machine:~$ git clone git@github.com:brunotrbr/softensity-test.git
user@machine:~$ cd softensity-test
```

&nbsp;

To prepare the execution environment run the following script at terminal/console of the computer:

```
[Windows]
PS C:\softensity-test> .\prepare_env.ps1

---------------------------------------------------------------------------

[Linux]
user@machine:softensity-test$ source prepare_env.sh
```

&nbsp;

### Running the program

Use the command `docker-compose up` to build and run the project automatically:

```
[Windows]
PS C:\softensity-test> docker-compose up

---------------------------------------------------------------------------

[Linux]
user@machine:softensity-test$ docker-compose up
```

&nbsp;

Case the command run successfuly, one container is started, named `softensity-test-AccessControl-api-1`). At this moment the API is available to access in http://localhost:5140/swagger/index.html


&nbsp;

Alternatively, use `dotnet xxx` commands to run project the project:

```
[Windows] - Run API
PS C:\softensity-test> cd .\src\AccessControl
PS C:\softensity-test\src\AccessControl> dotnet restore
PS C:\softensity-test\src\AccessControl> dotnet run

---------------------------------------------------------------------------

[Linux] - Run API
user@machine:softensity-test$ cd src
user@machine:src$ dotnet restore
user@machine:src$ dotnet run

```

**Obs:**

The application can be manually tested using the endpoints in the contract with postman, insomnia or acessing swagger after run the application at URL http://localhost:5140/swagger/index.html


&nbsp;

### Tests

I have no time to write tests.

&nbsp;

## Additional considerations

&nbsp;

#### Next steps

With more time, the Azure deploy should be fixed. As well as written tests, better tool for observability and IaC.

&nbsp;

#### Authentication & Authorization
I started adding a basic authentication (username and password) to secure access to API. But I choose to remove to be more simple to test. In a production environment, I suggest to use Auth0.

&nbsp;

#### Exceptions
As you can see in `ExceptionHandlers` directory, a `GeneralExceptionHandler` class has been created to handle all errors in application. Any error that occurs are logged and the API send back to user an `Internal Server Error` (status code 500) with a generic message, without show the stack trace or expose any kind of information. The request method is logged in the controller, error message and stack trace are logged in console for further investigations of the cause by developers.

I know it may be wrong turn all errors into 500 (depends on the stratefy of the company), but here is just to show that it's possible catch erros globally.

&nbsp;

#### Commits on github
    step 1
    Initial commit with fixes
	
    - Added project AccessControl [Application, Domain, Infrastructure)
	- Moved Door and Cards to AccessControl.Domain.Models
	- Moved DbContext to AccessControl.Infrastructure.Context
	- Added basic versioning to controller
	- Added swagger
	- Changed lauchSettings.json to start launching swagger

    OBS: I know the folder "v1" is not the right way to version an API, and I know there is a package called "Asp.Versioning" that allow me to handle versioning correctly, but I have no time to learn how to use it until our interview. So I choose to do how I do at my current work, and learn with more time after our meeting.


    step 2
	- Recreate folders and solutions, because after add project to the solution they were added alongside src folder. So I have to move new projects to /src and recreate the references in the project using dotnet CLI (dtonet sln add)
	- OBS: when added swagger, I got an error because all actions in CardController were POST, and swagger uses Controller + actions to create the endpoints in the UI. So I analyzed the endpoints and changed the "CancelPermission" to DELETE and "GrantAccess" to PATCH, because it will change part of the information, not recreate or change all infos about a card.


    step 3
	- Added GeneralExceptionHandler. Intentionally turn the errors in Internal Server Error, to not expose any kind of information to the client. Loggers are added to help troubleshooting. 
	- Added interfaces, use cases and dependency injection to handle scenarios and if necessary validate business infos.
	- Added repositories to replace AccessControlService, separating operations from Cards and Doors in specific repositories. I was not initializing the context DbSet (missing get and set). A quick google solved it.


    step 4
    - Added docker support to run application

    step-5
    - Add docker build to github actions

    step-6
    - Created methods and operations to repository and related.
        - Added enum to DoorType and changed request door type to enum. It helped to remove validation to door types and uncertain values (since doortypes 3-99 are not defined but allowed in if/else.)
        - Added EnumSchemaFilter to swagger handle int as string in enums
        - Controller responses changed to ActionResults, and added schemas.
        - Added validations to not insert duplicated doors and cards, and remove only doors that exists and not in use.
        - Added method and validations to CardRepository/GrantAccess
        - Added method and validations to CardRepository/CancelPermission. Since nowhere defines what permissionId means, I took the liberty to change the CancelPermission signature method to "[FromQuery] int cardNumber, [FromQuery] int doorNumber", so we have the same signature to grant and cancel access.
        - Added method to list cards and doors (just to check how things are going)

    step-7
    - Added deploy to Azure, but it's not working

    step-8
    - Removed docker build to github actions added in step-5
    - Added serilog

    step-9
    - Added readme
    - Removed UseDeveloperExceptionPage to GeneralExceptionHandler works properly in development environment
    - Added net-main.zip to root directory

### Acknowledgements

* [Img Shields](https://shields.io)
* [Table generators for markdown](https://www.tablesgenerator.com/markdown_tables)



[contributors-shield]: https://img.shields.io/github/contributors/brunotrbr/softensity-test?style=for-the-badge
[contributors-url]: https://github.com/brunotrbr/softensity-test/graphs/contributors
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/brunotrbr/
