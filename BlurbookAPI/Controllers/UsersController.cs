using BlurbookAPI.Models;
using BlurbookAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlurbookAPI.Controllers
{
    public class UsersController : ApiController
    {
        UsersRepository _usersRepository;

        public UsersController()
        {
            _usersRepository = new UsersRepository();
        }

        // GET api/users
        public List<User> GetAllUsers()
        {
            //return new string[] { "value1", "value2" };
            return _usersRepository.GetUsersAll();
        }

        // GET api/users/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/users
        public void Post([FromBody]string value)
        {
        }

        // PUT api/users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/users/5
        public void Delete(int id)
        {
        }
    }
}
