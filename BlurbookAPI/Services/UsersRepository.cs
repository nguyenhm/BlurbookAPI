using BlurbookAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BlurbookAPI.Services
{
    //This class is used to call stored procedured related to Users
    public class UsersRepository
    {
        string _connStr = ConfigurationManager.ConnectionStrings["BlurbookConnectionString"].ConnectionString;

        public List<User> GetUsersAll()
        {
            List<User> users = new List<User>();

            using (var connection = new SqlConnection(_connStr))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "UserGetAll",
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                using (IDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        users.Add(FillModel(dr));
                    }
                }
            }

            return users;
        }

        private User FillModel(IDataReader dr)
        {
            var user = new User
            {
                UserID = (int)dr["UserID"],
                FName = (string)dr["FName"],
                LName = (string)dr["LName"],
                Birthday = dr["Birthday"] != DBNull.Value ? (DateTime) dr["Birthday"] : DateTime.MinValue,
                PhoneNumber = dr["PhoneNumber"] != DBNull.Value ? dr["PhoneNumber"].ToString() : "",
                Gender = dr["Gender"] != DBNull.Value ? dr["Gender"].ToString() : "",
                AvatarLink = dr["AvatarLink"] != DBNull.Value ? dr["AvatarLink"].ToString() : "",
                Email = (string)dr["Email"],
                Password = (string)dr["Password"]
            };

            return user;
        }

    }
}