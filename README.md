# Worktastic
Based on the Asp .NET Core course of [Jannick Leismann](https://github.com/JannickLeismann). The original code can be found [here](https://github.com/JannickLeismann/worktastic). This version is upgraded to NET Core 7.0 using bootstrap 5. Below you'll find the most common cmd arguments if you are not developing with VS on windows.

## Getting Started
* install net core from [here](https://learn.microsoft.com/en-us/dotnet/core/install/)
* to install entity framework (EF) and it's tools see [here](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
* we can create a mvc razor auth app (like this) with `dotnet new mvc --auth Individual -o <APP_NAME_HERE>`
* for other project templates, see [here](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new)

## Common EF commands
* EF base command `dotnet ef`
* add migration `dotnet ef migrations add <NAME>`
* create schema from migration `dotnet ef database update`
* to learn more about migrations, see [here](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)


## DB Sample Users
### Admin
* name: `admin@worktastic.com`
* pw: `Test.1234`
### User1
* name: `max@mail.com`
* pw: `5kX99xM7KxQz$Ha`
### User2
* name: `david@mail.com`
* pw: `1kX99xM7KxQz$Ha`

## Run Debug
* build and run and automatically update changes `dotnet watch`
* build and run `dotnet run`

## Server Deployment
### Ubuntu 20.04 LTS
* [install the .NET runtime](https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu-2004)

## Sources
* https://github.com/JannickLeismann/worktastic
* https://www.udemy.com/course/aspnet-core-intensivkurs/