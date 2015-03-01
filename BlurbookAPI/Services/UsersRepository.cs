using BlurbookAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BlurbookAPI.Services
{
    //This class is used to call stored procedured related to Users
    public class UsersRepository : IUsersRepository
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

        public User GetUserByID(int userID)
        {
            var user = new User();

            using (var connection = new SqlConnection(_connStr))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "UserGetByID",
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                connection.Open();
                using (IDataReader dr = command.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        user = FillModel(dr);
                    }
                }
            }

            return user;
        }

        public DataTable GetUserByEmail(string email)
        {
            DataTable user = new DataTable();

            user.Columns.Add(new DataColumn("FName", typeof(String)));
            user.Columns.Add(new DataColumn("LName", typeof(String)));

            using (var connection = new SqlConnection(_connStr))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "UserGetByEmail",
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = email;

                connection.Open();
                using (IDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        user.Rows.Add(dr["FName"], dr["LName"]);
                    }
                }
            }
            return user;
        }

        public bool UserAuthentication(string email, string password)
        {
            bool auth = false;

            using (var connection = new SqlConnection(_connStr))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "UserGetByEmailPass",
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = email;
                command.Parameters.Add("@Password", SqlDbType.VarChar, 255).Value = password;

                connection.Open();
                using (IDataReader dr = command.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        auth = true;
                    }
                }
            }

            return auth;
        }

        public void CreateNewAccount(string firstName, string lastName, string email, string password)
        {
            using (var connection = new SqlConnection(_connStr))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "UserAddNew",
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("FName", firstName);
                command.Parameters.AddWithValue("LName", lastName);
                command.Parameters.AddWithValue("Email", email);
                command.Parameters.AddWithValue("Password", password);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool IsUserExisted(string email)
        {
            bool userExisted = false;

            using (var connection = new SqlConnection(_connStr))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "UserGetByEmail",
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = email;

                connection.Open();
                using (IDataReader dr = command.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        userExisted = true;
                    }
                }
            }

            return userExisted;
        }

        private User FillModel(IDataReader dr)
        {
            var user = new User
            {
                UserID = (int)dr["UserID"],
                FName = (string)dr["FName"],
                LName = (string)dr["LName"],
                Birthday = dr["Birthday"] != DBNull.Value ? (DateTime) dr["Birthday"] : DateTime.MinValue,
                PhoneNumber = dr["PhoneNumber"] != DBNull.Value ? dr["PhoneNumber"].ToString() : null,
                Gender = dr["Gender"] != DBNull.Value ? dr["Gender"].ToString() : null,
                AvatarLink = dr["AvatarLink"] != DBNull.Value ? dr["AvatarLink"].ToString() : null,
                Email = (string)dr["Email"],
                Password = (string)dr["Password"]
            };

            return user;
        }
    }
}