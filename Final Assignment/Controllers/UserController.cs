using Final_Assignment.Models;
using Final_Assignment.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_Assignment.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        UserRepository userRepo = new UserRepository();

        [Route("")]

        public IHttpActionResult Get()
        {
            return Ok(userRepo.GetAll());
        }

        [Route("login")]
        public IHttpActionResult PostValidate(User user)
        {
            if (userRepo.Validate(user))
            {
                return Ok(userRepo.getIdbyUserName(user));
            }
            return StatusCode(HttpStatusCode.Unauthorized);
        }

        [Route("{id}", Name = "GetUserById")]
        public IHttpActionResult Get(int id)
        {
            User user = userRepo.Get(id);
            if (user == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(user);
        }

        [Route("")]
        public IHttpActionResult Post(User user)
        {
            userRepo.Insert(user);
            string uri = Url.Link("GetUserById", new { id = user.UserID });
            return Created(uri, user);
        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] User user)
        {
            user.UserID = id;
            userRepo.Update(user);
            return Ok(user);
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (userRepo.Get(id) == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            userRepo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
