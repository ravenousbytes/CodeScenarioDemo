using AdministrationTool.Data.Models;
using AdministrationTool.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdministrationTool.Web.Api
{
    public class UsersController : ApiController
    {
        private readonly IUserData db;

        public UsersController(IUserData db)
        {
            this.db = db;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            var model = db.GetAll();
            return model;
        }
    }
}
