




# COMP90018 服务器

## 如何使用？
### 注册
你只需要输入一个用户名来注册。

发送一个 **POST** 请求到 `https://octopus-app-6yuia.ondigitalocean.app/user/register`，带有以下JSON负载：
```
{
"username": "morning" // 将这个改为你想要的用户名
}
```

如果成功，返回：
```
{
    "message": "User registered successfully"
}
```

如果用户名重复，返回：
```
{
    "error": "Error 1062 (23000): Duplicate entry 'wow' for key 'users.PRIMARY'"
}
```

### 登录
你只需要输入用户名来登录。

发送一个 **POST** 请求到 `https://octopus-app-6yuia.ondigitalocean.app/user/login`，带有JSON：

```
{
    "username": "morning" // 将这个改为你的用户名
}
```
是的，这里它和注册过程一样。

如果成功，返回：
```
{
    "message": "User logged in successfully",
    "score": 0
}
```

如果用户名不正确，返回：
```
{
    "error": "User not found"
}
```

### 更新分数
输入用户名和新分数来更新分数。
这将只存储最高分，这意味着如果你的新分数低于原始分数，它将不会被更新。

发送一个 **PATCH** 请求到 `https://octopus-app-6yuia.ondigitalocean.app/user/updateScore`，带有JSON：

```
{
    "username": "morning", // 将这个改为你的用户名
    "score": 3 
}
```

如果成功，返回：

```
{
    "message": "User score updated successfully"
}
```

如果用户名不正确，返回：
```
{
    "error": "record not found"
}
```

### 获取排名榜，显示前10名用户

发送一个 **GET** 请求到 `https://octopus-app-6yuia.ondigitalocean.app/user/getTopUsers`，带有空的请求体。

返回用户列表。

如果用户总数大于10，输出将被限制在10个，并按从高到低的顺序排列。

如果用户总数小于10，输出将列出所有用户，并按从高到低的顺序排列。

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



### 目录介绍

##### 目录结构
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


- /server/main.go: 项目的入口点。这个 main.go 文件通常相对较小，包含了启动代码和服务的初始化。
- /api: 这个目录包含公共的 API 定义和协议，例如数据格式、JSON schemas 等。它还包括负责处理 HTTP 请求和响应的路由处理器。
- /models: 这个目录包含了你的数据模型的定义，例如 user.go，定义了用户数据的结构和数据类型。
- /repository: 这个目录包含了与数据库交互相关的代码。例如，user.go 包含了所有与用户相关的数据库交互函数，如查询、更新和删除用户数据。
- /services: 这个目录包含了应用的核心业务逻辑，或者说是应用的“服务”层。这里的 user.go 文件包含了与用户相关的业务逻辑，例如注册新用户、验证用户等。
- Dockerfile: 用于 Docker 的配置文件，定义了如何构建应用的 Docker 镜像。
- README.md: 项目的 README 文件，提供了项目的概览和如何构建及运行项目的指南。

# MySQL

### 用户表

```
CREATE TABLE users (
    username VARCHAR(20) NOT NULL,
    score INT NOT NULL,
    PRIMARY KEY (username)
);
```


