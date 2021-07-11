using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Tracker.Models;
using Project = Task_Tracker.Models.Project;
using Task_Tracker.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Task_Tracker.Models
{
    public class Task
    {
        // Task information
        public static int id = 0;
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter the name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter the name of the project")]
        public string Project { get; set; }

        [Required(ErrorMessage = "Enter the status")]
        public TaskStatuses Status { get; set; }

        [Required(ErrorMessage = "Enter the description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter the bumber for priority")]
        public int Priority { get; set; }
        
        // Class Task constructor
        public Task(string name, string project, TaskStatuses status, string description, int priority)
        {
            Id = id++;
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