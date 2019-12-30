using System;

namespace Persistence.MODEL
{
    public class User
    {
        public User() { }

        public int UserID{get;set;}
        public string UserName{get;set;}
        public string PassWord{get;set;}
        public string UserEmail{get;set;}
        public string UserPhoneNumber{get;set;}
        public DateTime UserBirthday{get;set;}
        public string UserGender{get;set;}
    }
}