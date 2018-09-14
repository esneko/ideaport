using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ideaport.Models;

namespace ideaport.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SystemContext _system;

        public UsersController(SystemContext system)
        {
            _system = system;
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            List<User> users = _system.User.ToList();

            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            User user = _system.User.Find(new Guid(id));

            return user;
        }

        [HttpPost]
        public void Post(User user)
        {
        }
        
        [HttpPut("{id}")]
        public void Put(string id, User user)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            User user = _system.User.Find(new Guid(id));
            _system.User.Remove(user);
            _system.SaveChanges();
        }
    }
}
