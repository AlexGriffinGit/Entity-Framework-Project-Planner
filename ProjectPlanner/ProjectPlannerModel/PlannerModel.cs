using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ProjectPlannerModel
{
    public class PlannerModel : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Planner");
    }

    public class Project
    {

    }

    public class Feature
    {

    }

    public class Issue
    {

    }

    public class Note
    {

    }
}
