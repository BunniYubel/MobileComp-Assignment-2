package routesHandlers

import (
	"github.com/gin-gonic/gin"
	"github.com/ryougi-shiky/COMP90018_Backend/models"
	"github.com/ryougi-shiky/COMP90018_Backend/services"
	"net/http"
)

type RegisterRequest struct {
	Username string `json:"username" binding:"required"`
}

type LoginRequest struct {
	Username string `json:"username" binding:"required"`
}

func RegisterUserHandler(userService services.UserService) gin.HandlerFunc {
	return func(c *gin.Context) {
		var request RegisterRequest
		if err := c.ShouldBindJSON(&request); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		// Check if user already exists
		existingUser, err := userService.GetUserByUsername(request.Username)
		if err == nil {
			// User exists, so log them in instead
			c.JSON(http.StatusOK, gin.H{"message": "User already exists, logged in successfully", "score": existingUser.Score})
			return
		}

		// If we're here, the user does not exist, so we can register them
		user := models.User{
			Username: request.Username,
			Score:    0,
		}

		err = userService.RegisterUser(&user)
		if err != nil {
			c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
			return
		}

		c.JSON(http.StatusOK, gin.H{"message": "User registered successfully"})
	}
}

func LoginUserHandler(userService services.UserService) gin.HandlerFunc {
	return func(c *gin.Context) {
		var request LoginRequest
		if err := c.ShouldBindJSON(&request); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		user, err := userService.GetUserByUsername(request.Username)
		if err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": "User not found"})
			return
		}

		c.JSON(http.StatusOK, gin.H{"message": "User logged in successfully", "score": user.Score})
	}
}

type UpdateScoreRequest struct {
	Username string `json:"username" binding:"required"`
	Score    int    `json:"score" binding:"required"`
}

func UpdateUserScoreHandler(userService services.UserService) gin.HandlerFunc {
	return func(c *gin.Context) {
		var request UpdateScoreRequest
		if err := c.ShouldBindJSON(&request); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		err := userService.UpdateUserScore(request.Username, request.Score)
		if err != nil {
			c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
			return
		}

		c.JSON(http.StatusOK, gin.H{"message": "User score updated successfully"})
	}
}

func GetTopUsersHandler(userService services.UserService) gin.HandlerFunc {
	return func(c *gin.Context) {
		users, err := userService.GetTopUsers()
		if err != nil {
			c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
			return
		}

		c.JSON(http.StatusOK, gin.H{"top_users": users})
	}
}
