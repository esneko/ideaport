using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ideaport.Models;

namespace ideaport.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly SystemContext _system;

        private readonly IMapper _mapper;

        public TasksController(SystemContext system, IMapper mapper)
        {
            _system = system;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Task>> Get()
        {
            List<Task> tasks = _system.Task
                .Include(e => e.Client)
                .Include(e => e.UserTasks)
                .ThenInclude(e => e.User)
                .ToList();

            return tasks;
        }

        [HttpGet("{id}")]
        public ActionResult<Task> Get(string id)
        {
            Task task = _system.Task.Find(new Guid(id));

            return task;
        }

        [HttpPost]
        public void Post(TaskViewModel task)
        {
            Task newTask = _mapper.Map<TaskViewModel, Task>(task);
            _system.Task.Add(newTask);

            _system.UserTasks.AddRange(task.Employees.Select(user => new UserTask()
            {
                UserId = new Guid(user.Id),
                TaskId = newTask.Id
            }));

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
