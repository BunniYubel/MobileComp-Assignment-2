# COMP90018 Backend [中文](./CN_README.md)

## How to Use It?
### Register
You only need to enter a username to register.

Send a **POST** request to `https://octopus-app-6yuia.ondigitalocean.app/user/register` with the following JSON payload:
```
{
    "username": "morning" // Change this to your desired username
}
```

If success, return:
```
{
    "message": "User registered successfully"
}
```

If username duplicate, return:
```
{
    "error": "Error 1062 (23000): Duplicate entry 'wow' for key 'users.PRIMARY'"
}
```


### Login
Only need to enter username to login.

Send a **Post** request to `https://octopus-app-6yuia.ondigitalocean.app/user/login` with JSON:
```
{
    "username": "morning" // Change this to your username
}
```
Yes, it's the same as the registration process.

If success, return:
```
{
    "message": "User logged in successfully",
    "score": 0
}
```

If username incorrect, return:
```
{
    "error": "User not found"
}
```

### Update Score
Enter username and new score to update score.
This will only store the maximum score, which means if your new score is lower than original score, it will not be updated.

Send a **Patch** request to `https://octopus-app-6yuia.ondigitalocean.app/user/updateScore` with JSON:
```
{
    "username": "morning", // Change this to your username
    "score": 3 
}
```
If success, return:
```
{
    "message": "User score updated successfully"
}
```

If username incorrect, return:
```
{
    "error": "record not found"
}
```

### Get Top 10 Users
Send a **GET** request to `https://octopus-app-6yuia.ondigitalocean.app/user/getTopUsers` with empty body.

Return a list of users. 

If total number of users is larger than 10, output will be limited in 10 and in the order from high to low.

If total number of users is smaller than 10, output will list all users in the order from high to low.
```
{
    "top_users": [
        {
            "Username": "unimelb",
            "Score": 111
        },
        {
            "Username": "hello",
            "Score": 100
        },
        {
            "Username": "awesOme",
            "Score": 23
        },
        {
            "Username": "morning",
            "Score": 3
        },
        ...
        {
            "Username": "noname",
            "Score": 0
        }
    ]
}
```



### Directory Introduction

##### Directory Structure
```
/COMP90018_Backend
  /api
    /routesHandler
      userRoute.go
  /models
    user.go
  /repository
    user.go
  /services
    user.go
  /server
    main.go
  Dockerfile
  README.md
```

- /server/main.go: The entry point of the project. This main.go file is usually relatively small and contains the startup code and initialization of the service.
- /api: This directory contains public API definitions and protocols, such as data formats, JSON schemas, etc. It also includes route handlers responsible for processing HTTP requests and responses.
- /models: This directory contains the definitions of your data models, such as user.go, defining the structure and data types of user data.
- /repository: This directory contains code related to database interactions. For example, user.go includes all user-related database interaction functions, such as querying, updating, and deleting user data.
- /services: This directory contains the core business logic, or the “service” layer of the application. The user.go file here includes business logic related to users, such as registering new users and validating users.
- Dockerfile:  A configuration file for Docker, defining how to build the Docker image of your application.
- README.md: The README file of the project, providing an overview of the project and instructions on how to build and run the project.

# MySQL

### Users Table
```
CREATE TABLE users (
    username VARCHAR(20) NOT NULL,
    score INT NOT NULL,
    PRIMARY KEY (username)
);
```
