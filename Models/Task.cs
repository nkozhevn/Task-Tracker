using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Tracker.Models;
using Project = Task_Tracker.Models.Project;
using Task_Tracker.Controllers;

namespace Task_Tracker.Models
{
    public class Task
    {
        // Task information
        public int Id { get; set; }
        public string Name { get; set; }
        public string Project { get; set; }
        public TaskStatuses Status { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        
        // Class Task constructor
        public Task(string name, string project, TaskStatuses status, string description, int priority)
        {
            Name = name;
            Project = project;
            Status = status;
            Description = description;
            Priority = priority;
        }
        public Task()
        {

        }
    }
}