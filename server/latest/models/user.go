package models

type User struct {
	Username string `gorm:"primary_key;type:varchar(20);not null;unique"`
	Score    int    `gorm:"not null"`
}
