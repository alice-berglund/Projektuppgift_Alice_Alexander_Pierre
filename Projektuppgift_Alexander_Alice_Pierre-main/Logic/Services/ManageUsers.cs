using Logic.DAL;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class ManageUsers
    {
        IDataAccess DataAccess = new DataAccess();
        List<User> Users = new List<User>();

        public List<User> AllUsers()
        {
            Users = (List<User>)DataAccess.GetData<User>(FileName.User);

            return Users;
        }

        public void AddUser(string userName, string passWord, int mechanicIndex, List<User> users, List<Mechanic> mechanics)
        {
            if(passWord.Length < 5 || passWord.Length > 10)
                 throw new Exception("Lösenorden ska vara mellan 5 och 10 tecken");

            if (!StringExtension.ValidEmail(userName))
                throw new Exception("Felaktig emailadress");
            
            User user = new User();
            user.Username = userName;
            user.Password = passWord;
            user.MechanicId = mechanics[mechanicIndex].IdNumber;
            user.IsAdmin = false;

            users.Add(user);

            DataAccess.SaveData<User>(users, FileName.User);
        }

        public List<User> AddAdmin()
        {
            Admin admin = new Admin();
            admin.Username = "Bosse";
            admin.Password = "Meckarn123";
            admin.IsAdmin = true;

            Users.Add(admin);

            return Users;
        }

        public async Task DeleteUser(User chooosenUser, List<User> users)
        {
            users = users
                .Where(user => user.Username != chooosenUser.Username)
                .ToList();

           await DataAccess.SaveData<User>(users, FileName.User);
        }
    }
}
