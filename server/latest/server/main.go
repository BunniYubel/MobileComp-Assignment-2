package main

import (
	"fmt"
	"log"
	"net/http"

	"github.com/gin-gonic/gin"
	"github.com/ryougi-shiky/COMP90018_Backend/api/routesHandlers"
	"github.com/ryougi-shiky/COMP90018_Backend/repository"
	"github.com/ryougi-shiky/COMP90018_Backend/services"
)

func main() {
	fmt.Println("Starting server...")
	db, err := repository.ConnectToDB()
	if err != nil {
		log.Fatalf("Failed to connect to DB: %s", err.Error())
	}

	userRepository := repository.NewUserRepository(db)

	userService := services.NewUserService(userRepository)

	router := gin.Default()

	router.POST("/user/register", routesHandlers.RegisterUserHandler(userService))
	router.POST("/user/login", routesHandlers.LoginUserHandler(userService))
	router.PATCH("/user/updateScore", routesHandlers.UpdateUserScoreHandler(userService))
	router.GET("/user/getTopUsers", routesHandlers.GetTopUsersHandler(userService))

	log.Fatal(http.ListenAndServe(":8080", router))
}
