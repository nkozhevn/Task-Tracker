using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Task_Tracker.Models;
using System.Threading.Tasks;
using Task = Task_Tracker.Models.Task;
using Microsoft.EntityFrameworkCore;
using Task_Tracker.Controllers;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Task_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public static DataContext db; //Creating a database
        public DataController(DataContext context) // Getting access to database
        {
            db = context;
            // Putting some objects to test the project
            if (!db.Project.Any())
            {
                db.Project.Add(new Project("Recruitment", new DateTime(2021, 07, 10), ProjectStatuses.Active, "Test", 10));
                db.SaveChanges();
            }
            if (!db.Task.Any())
            {
                db.Task.Add(new Task("Code", "Recruitment", TaskStatuses.InProgress, "Test", 10));
                db.Task.Add(new Task("Test", "Recruitment", TaskStatuses.ToDo, "Test1", 7));
                db.SaveChanges();
            }
        }

        // GET: api/<DataController>/tasks
        [HttpGet("tasks")]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            return await db.Task.ToListAsync(); // Returning all the tasks
        }

        // GET: api/<DataController>/projects
        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await db.Project.ToListAsync(); // Returning all the projects
        }

        // GET api/<DataController>/tasks/id
        [HttpGet("tasks/{id}")]
        public List<Task> GetProjects(int id)
        {
            Project proj = db.Project.FirstOrDefault(t => t.Id == id);
            List<Task> list = new List<Task>();
            list.Add((Task)db.Task.Select(p => p.Project == proj.Name));
            return list; // Returning all tasts from the direct project
        }

        // POST api/<TasksController>
        [HttpPost]
        public async Task<ActionResult<Task>> PostTask(Task task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            db.Task.Add(task);
            await db.SaveChangesAsync();
            return Ok(task); // Posting a new task
        }

        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }
            db.Project.Add(project);
            await db.SaveChangesAsync();
            return Ok(project); // Posting a new project
        }

        // PUT api/<TasksController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Task>> PutTask(int id, Task task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            if (!db.Task.Any(x => x.Id == id))
            {
                return NotFound();
            }
            db.Update(task);
            await db.SaveChangesAsync();
            return Ok(task); // Updatin an existing task
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Project>> PutProject(int id, Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }
            if (!db.Project.Any(x => x.Id == id))
            {
                return NotFound();
            }
            db.Update(project);
            await db.SaveChangesAsync();
            return Ok(project); // Updating an existing project
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Task>> DeleteTask(int id)
        {
            Task task = db.Task.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            db.Task.Remove(task);
            await db.SaveChangesAsync();
            return Ok(task); // Deleting a task
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> DeleteProject(int id)
        {
            Project project = db.Project.FirstOrDefault(x => x.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            db.Project.Remove(project);
            await db.SaveChangesAsync();
            return Ok(project); // Deleting a project
        }
    }
}
