using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Tracker.Models
{
    public class Project
    {
        // Project information
        public static int id = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public ProjectStatuses Status { get; set; }
        public string Description { get; set; }
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
