# Worktastic
Based on the Asp .NET Core course of [Jannick Leismann](https://github.com/JannickLeismann). The original code can be found [here](https://github.com/JannickLeismann/worktastic). This version is upgraded to NET Core 7.0 using bootstrap 5. Below you'll find the most common cmd arguments if you are not developing with VS on windows.

## Getting Started
* install net core from [here](https://learn.microsoft.com/en-us/dotnet/core/install/)
* to install entity framework (EF) and it's tools see [here](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
* we can create a mvc razor auth app (like this) with `dotnet new mvc --auth Individual -o <APP_NAME_HERE>`
* for other project templates, see [here](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new)

## Common EF commands
* EF base command `dotnet ef`
* add migration `dotnet ef migrations add <NAME> -o Data/Migrations`
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
#### Publish
* run `dotnet publish --configuration Release` in local project dir 
* copy the files to the server by running `scp -r bin/Release/net8.0/publish/* username@remotecomputer:/var/www/worktastic` locally
* ssh to the server
* [install the .NET runtime](https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu-2004) on the server

#### Install & Configure Postgresql
* `sudo apt install postgresql`

#### Install & Configure Nginx
* (the following steps will all be on the server side)
* [install nginx](https://www.nginx.com/resources/wiki/start/topics/tutorials/install/#official-debian-ubuntu-packages)
* get nginx status `service nginx status`
* run nginx `sudo service nginx start`
* when we open the browser and navigate to the server ip, the nginx start page should be shown
* go to `cd /etc/nginx`
* `mkdir sites-available`
* `mkdir sites-enabled`
* `nano /etc/nginx/sites-available/default`
* add the following and save
```

server {
    listen        80;
    location / {
        proxy_pass         http://127.0.0.1:5000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
}
```
* add symlink `sudo ln -s /etc/nginx/sites-available/default /etc/nginx/sites-enabled/default`
* `nano nano /etc/nginx/nginx.conf`
* add the line `include       /etc/nginx/sites-enabled/*;` to `http {}` and save
* `sudo nginx -s reload`
* run `sudo nginx -t` to verify the configuration
* reload nginx with `sudo nginx -s reload` to apply the changes

#### Test the site
* run `dotnet /var/www/worktastic/worktastic.dll` on the server
* when we open the browser and navigate to the server ip the startpage of the site should be shown

#### Setup service
* ssh to server and run `chown -R www-data:www-data /var/www`
* run `scp -r worktastic-app.service username@remotecomputer:/etc/systemd/system` on the local machine in project directory
* run `sudo systemctl enable worktastic-app.service` on server to enable the service
* run `sudo systemctl start worktastic-app.service` to start the service
  
## Sources
* https://github.com/JannickLeismann/worktastic
* https://www.udemy.com/course/aspnet-core-intensivkurs/