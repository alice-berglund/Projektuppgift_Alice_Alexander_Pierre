using Logic.DAL;
using Logic.Entities;
using System;
using System.Collections.Generic;

namespace Logic.Services
{
    public class LoginService
    {
        private DataAccess _db;

        public LoginService()
        {

            _db = new DataAccess();
        }

        public List<User> Login(string username, string password)
        {
            List<User> users = (List<User>)_db.GetData<User>(FileName.User);

           if(users.Exists(user => user.Username.ToLower().Equals(username) && user.Password.Equals(password)))
           {
               return users;
           }
           else
           {
                throw new Exception("User do not exist");
           }
        }
    }
}
