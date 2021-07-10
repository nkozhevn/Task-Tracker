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
        private static List<Project> ProjectList = new List<Project> //Creating a project list
        {
            new Project("Recruitment", new DateTime(2021, 07, 10), ProjectStatuses.Active, "Test", 10)
        };

        private static List<Task> TaskList = new List<Task> //Creating a task list
        {
            new Task("Code", "Recruitment", TaskStatuses.InProgress, "Test", 10),
            new Task("Test", "Recruitment", TaskStatuses.ToDo, "Test1", 7)
        };

        /*DB public static DataContext db; //Creating a database
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
        } */

        // GET: api/<DataController>/tasks

        [HttpGet("tasks")]
        public IEnumerable<Task> GetTasks()
        {
            return TaskList; // Returning all the tasks
        }

        [HttpGet("projects")]
        public IEnumerable<Project> GetProjects()
        {
            return ProjectList; // Returning all the projects
        }

        /*DB [HttpGet("tasks")]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            return await db.Task.ToListAsync(); // Returning all the tasks
        }

        // GET: api/<DataController>/projects
        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await db.Project.ToListAsync(); // Returning all the projects
        } */

        // GET api/<DataController>/tasks/id

        [HttpGet("tasks/{id}")]
        public List<Task> GetTasks(int id)
        {
            Project proj = ProjectList.FirstOrDefault(t => t.Id == id);
            List<Task> list = new List<Task>();
            for (int i = 0; i < TaskList.Count; i++)
            {
                if (proj.Name == TaskList[i].Project)
                {
                    list.Add(TaskList[i]);
                }
            }
            return list; // Returning all tasts from the direct project
        }

        /*DB [HttpGet("tasks/{id}")]
        public List<Task> GetProjects(int id)
        {
            Project proj = db.Project.FirstOrDefault(t => t.Id == id);
            List<Task> list = new List<Task>();
            list.Add((Task)db.Task.Select(p => p.Project == proj.Name));
            return list; // Returning all tasts from the direct project
        } */

        // POST api/<TasksController>

        [HttpPost]
        public ActionResult Post([FromBody]Task Task)
        {
            if (Task == null)
            {
                return StatusCode((int)HttpStatusCode.Conflict);
            }

            if (!ProjectList.Any(p => p.Name == Task.Project))
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            TaskList.Add(Task);

            return Ok(); // Posting a new task
        }

        [HttpPost]
        public ActionResult Post([FromBody] Project Project)
        {
            if (Project == null)
            {
                return StatusCode((int)HttpStatusCode.Conflict);
            }

            ProjectList.Add(Project);

            return Ok(); // Posting a new project
        }

        /*DB [HttpPost]
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
        } */

        // PUT api/<TasksController>/5

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Task Task)
        {
            if (Task == null)
            {
                return BadRequest();
            }

            if (!TaskList.Any(x => x.Id == id))
            {
                return NotFound();
            }

            if (!ProjectList.Any(p => p.Name == Task.Project))
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            int index = TaskList.FindIndex(t => t.Id == id);
            TaskList[index] = Task;

            return Ok(); // Updating an existing task
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Project Project)
        {
            if (Project == null)
            {
                return BadRequest();
            }

            if (!ProjectList.Any(x => x.Id == id))
            {
                return NotFound();
            }

            Project p = ProjectList.Where(t => t.Id == id).FirstOrDefault();
            int index = ProjectList.IndexOf(p);
            ProjectList[index] = Project;

            for (int i = 0; i < TaskList.Count; i++)
            {
                if (p.Name == TaskList[i].Project)
                {
                    TaskList[i].Project = Project.Name;
                }
            }

            return Ok(); // Updating an existing project
        }

        /*DB [HttpPut("{id}")]
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
        } */

        // DELETE api/<TasksController>/5

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            if (!TaskList.Any(x => x.Id == id))
            {
                return NotFound();
            }

            int index = TaskList.FindIndex(t => t.Id == id);
            TaskList.RemoveAt(index);
            
            return Ok(); // Deleting a task
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProject(int id)
        {
            if (!ProjectList.Any(x => x.Id == id))
            {
                return NotFound();
            }

            Project p = ProjectList.Where(t => t.Id == id).FirstOrDefault();

            for (int i = 0; i < TaskList.Count; i++)
            {
                if (p.Name == TaskList[i].Project)
                {
                    TaskList.RemoveAt(i);
                }
            }

            int index = ProjectList.IndexOf(p);
            ProjectList.RemoveAt(index);

            return Ok(); // Deleting a project
        }

        /*DB [HttpDelete("{id}")]
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
        } */
    }
}
