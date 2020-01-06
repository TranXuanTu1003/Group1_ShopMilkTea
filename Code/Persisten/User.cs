using System;
using System.Collections.Generic;

namespace Persistence.MODEL
{
    public class User
    {
        public User() { }

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserAccount { get; set; }
        public string PassWord { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserBirthday { get; set; }
        public string UserGender { get; set; }
        public bool UserShoppingCart { get; set; }
    }
}