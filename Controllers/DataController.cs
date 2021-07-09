using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Task_Tracker.Models;
using System.Threading.Tasks;
using Task = Task_Tracker.Models.Task;
using Microsoft.EntityFrameworkCore;
using Task_Tracker.Controllers;

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
            if (!db.Projects.Any())
            {
                db.Projects.Add(new Project("Recruitment", new DateTime(2021, 07, 10), ProjectStatuses.Active, "Test", 10));
                db.SaveChanges();
            }
            if (!db.Tasks.Any())
            {
                db.Tasks.Add(new Task("Code", "Recruitment", TaskStatuses.InProgress, "Test", 10));
                db.Tasks.Add(new Task("Test", "Recruitment", TaskStatuses.ToDo, "Test1", 7));
                db.SaveChanges();
            }
        }
        // GET: api/<DataController>/tasks
        [HttpGet("tasks")]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            return await db.Tasks.ToListAsync(); // Returning all the tasks
        }

        // GET: api/<DataController>/projects
        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await db.Projects.ToListAsync(); // Returning all the projects
        }

        // GET api/<DataController>/tasks/id
        [HttpGet("tasks/{id}")]
        public List<Task> GetProjects(int id)
        {
            Project proj = db.Projects.FirstOrDefault(t => t.Id == id);
            List<Task> list = new List<Task>();
            list.Add((Task)db.Tasks.Select(p => p.Project == proj.Name));
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
            db.Tasks.Add(task);
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
            db.Projects.Add(project);
            await db.SaveChangesAsync();
            return Ok(project); // Posting a new project
        }

        // PUT api/<TasksController>/5
        [HttpPut]
        public async Task<ActionResult<Task>> PutTask(Task task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            if (!db.Tasks.Any(x => x.Id == task.Id))
            {
                return NotFound();
            }
            db.Update(task);
            await db.SaveChangesAsync();
            return Ok(task); // Updatin an existing task
        }

        [HttpPut]
        public async Task<ActionResult<Project>> PutProject(Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }
            if (!db.Projects.Any(x => x.Id == project.Id))
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
            Task task = db.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            db.Tasks.Remove(task);
            await db.SaveChangesAsync();
            return Ok(task); // Deleting a task
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> DeleteProject(int id)
        {
            Project project = db.Projects.FirstOrDefault(x => x.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            db.Projects.Remove(project);
            await db.SaveChangesAsync();
            return Ok(project); // Deleting a project
        }
    }
}
