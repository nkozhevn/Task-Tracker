using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Tracker.Models
{
    public class Project
    {
        // Project information
        public static int id = 0;
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter the name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter the date")]
        public DateTime OpeningDate { get; set; }

        [Required(ErrorMessage = "Enter the date")]
        public DateTime ClosingDate { get; set; }

        [Required(ErrorMessage = "Enter the status")]
        public ProjectStatuses Status { get; set; }

        [Required(ErrorMessage = "Enter the description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter the priority")]
        public int Priority { get; set; }

        // Class Project constructor
        public Project(string name, DateTime closingdate, ProjectStatuses status, string description, int priority)
        {
            Id = id++;
            Name = name;
            OpeningDate = DateTime.Today;
            ClosingDate = closingdate;
            Status = status;
            Description = description;
            Priority = priority;
        }
        public Project()
        {

        }
    }
}
