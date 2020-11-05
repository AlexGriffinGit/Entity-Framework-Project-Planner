using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using ProjectPlannerModel;

namespace ProjectPlannerBusiness
{
    public class CRUDManager
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

        public List<Feature> RetrieveAllFeatures()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                return pc.Features.ToList();
            }
        }

        public List<Issue> RetrieveAllIssues()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                return pc.Issues.ToList();
            }
        }

        public List<Note> RetrieveAllNotes()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                return pc.Notes.ToList();
            }
        }

        public List<Feature> RetrieveProjectFeatures()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                List<Feature> features = new List<Feature>();

                var _featureIDs =
                    from f in pc.Features
                    where f.ProjectId == SelectedProject.ProjectId
                    select f;

                foreach (var item in _featureIDs)
                {
                    features.Add(item);
                }

                return features;
            }
        }

        public List<Feature> RetrieveCompleteFeatures()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                List<Feature> features = new List<Feature>();

                var _featureIDs =
                    from f in pc.Features
                    where f.ProjectId == SelectedProject.ProjectId && f.Status == 3 //temp num
                    select f;

                foreach (var item in _featureIDs)
                {
                    features.Add(item);
                }

                return features;
            }
        }

        public List<Feature> RetrieveToDoFeatures()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                List<Feature> features = new List<Feature>();

                var _featureIDs =
                    from f in pc.Features
                    where f.ProjectId == SelectedProject.ProjectId && f.Status != 3 //temp num
                    select f;

                foreach (var item in _featureIDs)
                {
                    features.Add(item);
                }

                return features;
            }
        }

        public List<Issue> RetrieveProjectIssues()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                List<Issue> issues = new List<Issue>();

                var _issueIDs =
                    from i in pc.Issues
                    where i.ProjectId == SelectedProject.ProjectId && i.Status != 3 //temp num
                    select i;

                foreach (var item in _issueIDs)
                {
                    issues.Add(item);
                }

                return issues;
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
                Project _newProject = new Project()
                {
                    Title = title,
                    Description = description,
                    Status = status,
                    Link = link
                };

                pc.Projects.Add(_newProject);

                pc.SaveChanges();
            }
        }

        public void CreateNewFeature(string title, string description, int status, int priority, string notes)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Feature _newFeature = new Feature()
                {
                    Title = title,
                    Description = description,
                    Status = status,
                    Priority = priority,
                    Notes = notes,

                    ProjectId = SelectedProject.ProjectId
                };

                pc.Features.Add(_newFeature);

                pc.SaveChanges();
            }
        }

        public void CreateNewIssue(string title, string description, int status, int priority, string notes)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Issue _newIssue = new Issue()
                {
                    Title = title,
                    Description = description,
                    Status = status,
                    Priority = priority,
                    Notes = notes,

                    ProjectId = SelectedProject.ProjectId
                };

                pc.Issues.Add(_newIssue);

                pc.SaveChanges();
            }
        }

        public void CreateNewNote(string title, string body)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Note _newNote = new Note()
                {
                    Title = title,
                    Body = body
                };

                pc.Notes.Add(_newNote);

                pc.SaveChanges();
            }
        }
    }
}
