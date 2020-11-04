using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using ProjectPlannerModel;

namespace ProjectPlannerBusiness
{
    class CRUDManager
    {
        public Project SelectedProject { get; set; }
        public Feature SelectedFeature { get; set; }
        public Issue SelectedIssue { get; set; }
        public Note SelectedNote { get; set; }

        static void Main(string[] args)
        {
            
        }

        public List<Project> RetrieveAllProjects()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                return pc.Projects.ToList();
            }
        }

        public List<Feature> RetieveAllFeatures()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                return pc.Features.ToList();
            }
        }

        public List<Issue> RetieveAllIssues()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                return pc.Issues.ToList();
            }
        }

        public List<Note> RetieveAllNotes()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                return pc.Notes.ToList();
            }
        }

        public void SetSelectedProject(object selected)
        {
            SelectedProject = (Project)selected;
        }

        public void SetSelectedFeature(object selected)
        {
            SelectedFeature = (Feature)selected;
        }

        public void SetSelectedIssue(object selected)
        {
            SelectedIssue = (Issue)selected;
        }

        public void SetSelectedNote(object selected)
        {
            SelectedNote = (Note)selected;
        }

        public void CreateNewProject(string title, string description, int status, string link)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Project newProject = new Project()
                {
                    Title = title,
                    Description = description,
                    Status = status,
                    Link = link
                };

                pc.Projects.Add(newProject);

                pc.SaveChanges();
            }
        }

        public void CreateNewFeature(string title, string description, int status, int priority, string notes)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Feature newFeature = new Feature()
                {
                    Title = title,
                    Description = description,
                    Status = status,
                    Priority = priority,
                    Notes = notes,

                    ProjectId = SelectedProject.ProjectId
                };

                pc.Features.Add(newFeature);

                pc.SaveChanges();
            }
        }

        public void CreateNewIssue(string title, string description, int status, int priority, string notes)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Issue newIssue = new Issue()
                {
                    Title = title,
                    Description = description,
                    Status = status,
                    Priority = priority,
                    Notes = notes,

                    ProjectId = SelectedProject.ProjectId
                };

                pc.Issues.Add(newIssue);

                pc.SaveChanges();
            }
        }

        public void CreateNewNote(string title, string body)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Note newNote = new Note()
                {
                    Title = title,
                    Body = body
                };

                pc.Notes.Add(newNote);

                pc.SaveChanges();
            }
        }
    }
}
