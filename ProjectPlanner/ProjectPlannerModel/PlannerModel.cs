using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string Link { get; set; }
        public List<Feature> Features { get; } = new List<Feature>();
        public List<Issue> Issues { get; } = new List<Issue>();
    }

    public class Feature
    {
        public int FeatureId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public string Notes { get; set; }
    }

    public class Issue
    {
        public int IssueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public string Notes { get; set; }
    }

    public class Note
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
