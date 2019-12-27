using AdministrationTool.Data.Models;
using AdministrationTool.Data.Services;
using AdministrationTool.Web.Models;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

// TODO: Add Elmah or ApplicationInsigts or something similar to capture, log, notify errors...
// TODO: Add Polly for Exception Handling policy.
// TODO: Do we need an association controller for the Manager?
namespace AdministrationTool.Web.Api
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserData db;
        private readonly IMapper mapper;

        public UsersController(IUserData db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        // TODO: Add bool includeManager?
        /// <summary>
        /// Get details about all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route()]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var users = await db.GetAllAsync();
                return Ok(mapper.Map<IEnumerable<UserModel>>(users));
            }
            catch (Exception)
            {
                return InternalServerError();
                throw;
            }
        }

        /// <summary>
        /// Get details about one user.
        /// </summary>
        /// <param name="principalName">The unique principalName of the user.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{principalName}", Name = "GetUser")]
        public async Task<IHttpActionResult> Get(string principalName)
        {
            try
            {
                var user = db.Get(principalName);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<UserModel>(user));
            }
            catch (Exception)
            {
                return InternalServerError();
                throw;
            }
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route()]
        public async Task<IHttpActionResult> Post(UserModel model)
        {
            try
            {
                ValidateModel(model);
                if (ModelState.IsValid)
                {
                    var user = mapper.Map<User>(model);
                    db.Add(user);
                    if (!await db.SaveChangesAsync())
                    {
                        return InternalServerError();
                    }
                    var newModel = mapper.Map<UserModel>(user);
                    return CreatedAtRoute("GetUser", new { principalName = newModel.PrincipalName }, newModel);
                }

                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return InternalServerError();
                throw;
            }
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="principalName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{principalName}")]
        public async Task<IHttpActionResult> Put(string principalName, UserModel model)
        {
            try
            {
                ValidateModel(model);
                if (ModelState.IsValid)
                {
                    var user = db.Get(principalName);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    var updatedUser = mapper.Map<User>(model);
                    db.Update(updatedUser);
                    if (!await db.SaveChangesAsync())
                    {
                        return InternalServerError();
                    }
                    var updatedModel = mapper.Map<UserModel>(updatedUser);
                    return CreatedAtRoute("GetUser", new { principalName = updatedModel.PrincipalName }, updatedModel);
                }

                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return InternalServerError();
                throw;
            }
        }

        /// <summary>
        /// Delete an existing user.
        /// </summary>
        /// <param name="principalName"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{principalName}")]
        public async Task<IHttpActionResult> Delete(string principalName)
        {
            try
            {
                var user = db.Get(principalName);
                if (user == null)
                {
                    return NotFound();
                }
                db.Delete(user);
                if (!await db.SaveChangesAsync())
                {
                    return InternalServerError();
                }
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
                throw;
            }
        }

        private void ValidateModel(UserModel model)
        {
            //TODO: Get code in Validator
            if (db.Get(model.PrincipalName) != null)
            {
                ModelState.AddModelError("PrincipalName", "User principalName must be unique.");
            }
            if (db.Get(model.ManagerPrincipalName) == null)
            {
                ModelState.AddModelError("ManagerPrincipalName", "User managerPrincipalName must exist as a user.");
            }
        }
    }
}
