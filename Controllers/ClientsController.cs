using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ideaport.Models;

namespace ideaport.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly SystemContext _system;

        public ClientsController(SystemContext system)
        {
            _system = system;
        }

        [HttpGet]
        public ActionResult<List<Client>> Get()
        {
            List<Client> clients = _system.Client.Include(e => e.Tasks).ToList();

            return clients;
        }

        [HttpGet("{id}")]
        public ActionResult<Client> Get(string id)
        {
            Client client = _system.Client.Find(new Guid(id));

            return client;
        }

        [HttpPost]
        public void Post(Client client)
        {
            _system.Client.Add(client);
            _system.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(string id, Client client)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            Client client = _system.Client.Find(new Guid(id));
            _system.Client.Remove(client);
            _system.SaveChanges();
        }
    }
}
