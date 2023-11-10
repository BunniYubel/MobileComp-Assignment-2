package services

import (
	"github.com/ryougi-shiky/COMP90018_Backend/models"
	"github.com/ryougi-shiky/COMP90018_Backend/repository"
)

type UserService interface {
	RegisterUser(user *models.User) error
	GetUserByUsername(username string) (*models.User, error)
	UpdateUserScore(username string, score int) error
	GetTopUsers() ([]models.User, error)
}

type UserServiceImpl struct {
	UserRepository repository.UserRepository
}

func (s *UserServiceImpl) RegisterUser(user *models.User) error {
	return s.UserRepository.RegisterUser(user)
}

func (s *UserServiceImpl) GetUserByUsername(username string) (*models.User, error) {
	return s.UserRepository.GetUserByUsername(username)
}

func (s *UserServiceImpl) UpdateUserScore(username string, score int) error {
	user, err := s.GetUserByUsername(username)
	if err != nil {
		return err
	}
	if score > user.Score {
		user.Score = score
		return s.UserRepository.UpdateUser(user)
	}
	return nil
}

func (s *UserServiceImpl) GetTopUsers() ([]models.User, error) {
	return s.UserRepository.GetTopUsers()
}

func NewUserService(userRepo repository.UserRepository) UserService {
	return &UserServiceImpl{
		UserRepository: userRepo,
	}
}
