using System;

namespace Logic.Entities
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get;  set; }
        public Guid? MechanicId { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class Admin : User
    {

    }
}
