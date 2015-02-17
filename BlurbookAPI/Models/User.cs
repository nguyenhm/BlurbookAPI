using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlurbookAPI.Models
{
    public class User
    {
        public User()
        {

        }

        public User(int userId, string fName, string lName, string phoneNumber, string gender, string avatarLink, string email, string password)
        {
            UserID = userId;
            FName = fName;
            LName = lName;
            PhoneNumber = phoneNumber;
            Gender = gender;
            AvatarLink = avatarLink;
            Email = email;
            Password = password;
        }

        public int UserID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string AvatarLink { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}