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
    public class TasksController : ControllerBase
    {
        private readonly SystemContext _system;

        public TasksController(SystemContext system)
        {
            _system = system;
        }

        [HttpGet]
        public ActionResult<List<Task>> Get()
        {
            List<Task> tasks = _system.Task.ToList();

            return tasks;
        }

        [HttpGet("{id}")]
        public ActionResult<Task> Get(string id)
        {
            Task task = _system.Task.FirstOrDefault(u => u.Id == new Guid(id));

            return task;
        }

        [HttpPost]
        public void Post(Task task)
        {
            _system.Task.Add(task);
            _system.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(string id, Task task)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            Task task = _system.Task.Find(new Guid(id));
            _system.Task.Remove(task);
            _system.SaveChanges();
        }
    }
}
