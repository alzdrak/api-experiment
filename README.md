
# api-experiment

*This project was created for learning purposes only.*

Flight booking experiment in asp.net core web api with docker support that references external OpenSky restful api. 

### How to run?

*This repo is using linux containers.*

You have two options:

first to build

`docker-compose -f docker-compose.yml -f docker-compose.override.yml -p api-experiment --no-ansi build`

then to run

`docker-compose -f docker-compose.yml -f docker-compose.override.yml -p api-experiment --no-ansi up --no-build --force-recreate --remove-orphans`

Api should now be running: https://localhost:44308/api/bookings

Or you can hit F5 in Visual Studio :)

It will spin up 2 containers, one for the app and another for sql server.



