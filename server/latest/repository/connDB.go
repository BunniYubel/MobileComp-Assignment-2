package repository

import (
	"fmt"
	"gorm.io/driver/mysql"
	"gorm.io/gorm"
	"log"
)

var user = "doadmin"
var password = "AVNS_zP2kyuFUoqgF3b4k0AT"
var server = "db-mysql-syd1-21907-do-user-14450765-0.c.db.ondigitalocean.com"
var port = 25060
var database = "defaultdb"

type MySQLUserRepository struct {
	db *gorm.DB
}

// Create a new repository object. Connect to the database.
func ConnectToDB() (*gorm.DB, error) {
	// Build connection string
	connString := fmt.Sprintf("%s:%s@tcp(%s:%d)/%s?charset=utf8&parseTime=True&loc=Local", user, password, server, port, database)
	// Create connection pool
	db, err := gorm.Open(mysql.Open(connString), &gorm.Config{})
	if err != nil {
		log.Printf("Error opening database connection: %s\n", err.Error())
		return nil, err
	}
	fmt.Printf("Database Connected!")

	return db, err
}
