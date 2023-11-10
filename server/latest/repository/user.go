package repository

import (
	"github.com/ryougi-shiky/COMP90018_Backend/models"
	"gorm.io/gorm"
)

type UserRepository interface {
	RegisterUser(user *models.User) error
	GetUserByUsername(username string) (*models.User, error)
	UpdateUser(user *models.User) error
	GetTopUsers() ([]models.User, error)
}

// Create a new repository object. Connect to the database.
func NewUserRepository(db *gorm.DB) UserRepository {
	return &MySQLUserRepository{db: db}
}

func (r *MySQLUserRepository) RegisterUser(user *models.User) error {
	return r.db.Create(user).Error
}

func (r *MySQLUserRepository) GetUserByUsername(username string) (*models.User, error) {
	var user models.User
	if err := r.db.Where("username = ?", username).First(&user).Error; err != nil {
		return nil, err
	}
	return &user, nil
}

func (r *MySQLUserRepository) UpdateUser(user *models.User) error {
	return r.db.Model(&models.User{}).Where("username = ?", user.Username).Updates(user).Error
}

func (r *MySQLUserRepository) GetTopUsers() ([]models.User, error) {
	var users []models.User
	err := r.db.Order("score desc").Limit(10).Find(&users).Error
	return users, err
}
