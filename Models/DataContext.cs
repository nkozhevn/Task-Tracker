using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Tracker.Models
{
    public class DataContext : DbContext
    {
        /*DB public DbSet<Project> Project { get; set; } // Database for Projects
        public DbSet<Task> Task { get; set; } // Database for tasks
        public DataContext(DbContextOptions<DataContext> options) // Database constructor
            : base(options)
        {
            Database.EnsureCreated();
        } */
    }
}