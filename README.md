# nfl-rushing
## Background
This is my solution to  ["the Rush"](https://github.com/tsicareers/nfl-rushing) interview challenge. 

Web app is built using:
* .NET Core 3.1
* React.js
* SQLite

This was my first time building a project using React.js so there were a few hiccups along the way. I figured trying out a new technology for this challenge would demonstrate my ability to quickly pick up something new. Considering the tech stack that theScore uses I believe this is a valuable skill.

Things I would do differently:
* Use a NoSQL database like mongo. The json file provided has some tricky data inconsistancies like mixing strings and numbers which makes using a relational db a chore. As soon as I started writing custom converters to sanitize the data I figured I was going to run into challenges. Luckily they weren't too bad for this project but it's something that could have been avoided.
* Actually leverage popular react components. But that would have likely defeated the purpose to just simply use [react-table](https://react-table.tanstack.com/). I tried to avoid any prebuilt components and do everything myself. With this being my first time building a react app it's probably pretty obvious. I believe I avoided most common anti-patterns but it is a bit jarring moving from .NET MVC which is what I've been working with. I really enjoyed working with react though and if anything this was a good learning experience.
* Use typescript. No idea why I didn't do this. I basically just realised a bit too late.

## Setup
### Method 1 - Docker

Navigate to the project directory and build the dockerfile:

```
docker build -t nfl-rushing .
docker run -d -p 5000:80 nfl-rushing
```
> add --s=0 for default json file or --s=1 for large file.

Or alternatively grab the image directly from my [docker repo](https://hub.docker.com/r/trigs01/work/tags)

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
