# nfl-rushing
## Info
This is my solution to  ["the Rush"](https://github.com/tsicareers/nfl-rushing) interview challenge. 

Web app is built using:
* .NET Core 3.1
* React.js
* SQLite

This was my first time building a project using React.js so there were a few hiccups along the way.
## Setup
### Method 1 - Docker

Navigate to the project directory and build the dockerfile:

```
docker build -t nfl-rushing .
```

### Method 2 - Dotnet

Navigate to the project directory and run

```
dotnet run
```

You can also specify a json file to use. I have included two in the project. 
```
dotnet run s=0
dotnet run s=1
```
> 0 is for the orignal json file I was provided. 1 uses a similar json file where I just added about 13k records.

The website should be accessible at http://localhost:5000
