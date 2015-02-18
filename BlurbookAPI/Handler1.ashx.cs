using BlurbookAPI.Services;
using JsonServices;
using JsonServices.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlurbookAPI
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : JsonHandler
    {
        public Handler1()
        {
            this.service.Name = "BlurbookWebAPI";
            this.service.Description = "JSON API for android appliation";
            InterfaceConfiguration IConfig = new InterfaceConfiguration("RestAPI", typeof(IUsersRepository), typeof(UsersRepository));
            this.service.Interfaces.Add(IConfig);
        }
    }
}