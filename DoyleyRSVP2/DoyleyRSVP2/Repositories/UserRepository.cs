using System;
using System.Collections.Generic;
using DoyleyRSVP2.Models;
using SQLite;

namespace DoyleyRSVP2.Repositories
{
    public class UserRepository
    {
        private SQLiteConnection conn;
        public static List<UserModel> Users { get; set; } = new List<UserModel>();

        public UserRepository(string dbPath)
        {
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<UserModel>();
        }

        // Method to get a user by their email address
        public UserModel GetUserByEmail(string email)
        {
            return conn.Table<UserModel>().FirstOrDefault(u => u.Email == email);
        }

        // Method to add user to the user table
        public void AddUser(UserModel user)
        {
            conn.Insert(user);
        }

        // Method to check if the user is already registered
        public bool IsUserRegistered(string email)
        {
            var user = conn.Table<UserModel>().Where(u => u.Email == email).FirstOrDefault();
            return user != null;
        }

        // Add other database operations here...
    }

}

