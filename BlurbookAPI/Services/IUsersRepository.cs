using BlurbookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlurbookAPI.Services
{
    interface IUsersRepository
    {
        List<User> GetUsersAll();
        User GetUserByID(int userID);
        bool UserAuthentication(string email, string password);
        void CreateNewAccount(string firstName, string lastName, string email, string password);
    }
}
