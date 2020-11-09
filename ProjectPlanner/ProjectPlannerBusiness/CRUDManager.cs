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
                    orderby f.Priority
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
                    where f.ProjectId == SelectedProject.ProjectId && f.Status == 3
                    select f;

                if (_featureIDs.Count() > 0)
                {
                    foreach (var item in _featureIDs)
                    {
                        features.Add(item);
                    }
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
                    where f.ProjectId == SelectedProject.ProjectId && f.Status != 3
                    orderby f.Priority
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
                    where i.ProjectId == SelectedProject.ProjectId && i.Status != 3
                    orderby i.Priority
                    select i;

                foreach (var item in _issueIDs)
                {
                    issues.Add(item);
                }

                return issues;
            }
        }

        public int RetrieveIndexOfNewProject()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                List<Project> projects = pc.Projects.ToList();
                int index = 0;

                for (int i = 0; i < projects.Count; i++)
                {
                    if (projects[i].ProjectId == SelectedProject.ProjectId)
                    {
                        index = i;
                    }
                }

                return index;
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

                SelectedProject = _newProject;
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

        public void UpdateProject(string title, string description, int status, string link)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _updateProject =
                    from p in pc.Projects
                    where p.ProjectId == SelectedProject.ProjectId
                    select p;

                foreach (var project in _updateProject)
                {
                    project.Title = title;
                    project.Description = description;
                    project.Status = status;
                    project.Link = link;

                    SelectedProject = project;
                }

                pc.SaveChanges();
            }
        }

        public void UpdateFeature(string title, string description, int status, int priority, string notes)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _updateFeature =
                    from f in pc.Features
                    where f.FeatureId == SelectedFeature.FeatureId
                    select f;

                foreach (var feature in _updateFeature)
                {
                    feature.Title = title;
                    feature.Description = description;
                    feature.Status = status;
                    feature.Priority = priority;
                    feature.Notes = notes;

                    SelectedFeature = feature;
                }

                pc.SaveChanges();
            }
        }

        public void UpdateIssue(string title, string description, int status, int priority, string notes)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _updateIssue =
                    from i in pc.Issues
                    where i.IssueId == SelectedIssue.IssueId
                    select i;

                foreach (var issue in _updateIssue)
                {
                    issue.Title = title;
                    issue.Description = description;
                    issue.Status = status;
                    issue.Priority = priority;
                    issue.Notes = notes;

                    SelectedIssue = issue;
                }

                pc.SaveChanges();
            }
        }

        public void UpdateNote(string title, string body)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _updateNote =
                    from n in pc.Notes
                    where n.NoteId == SelectedNote.NoteId
                    select n;

                foreach (var note in _updateNote)
                {
                    note.Title = title;
                    note.Body = body;

                    SelectedNote = note;
                }

                pc.SaveChanges();            
            }
        }

        public void DeleteProject()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _deleteProject =
                    from p in pc.Projects
                    where p.ProjectId == SelectedProject.ProjectId
                    select p;

                foreach (var item in _deleteProject)
                {
                    pc.RemoveRange(item);
                }

                pc.SaveChanges();
            }
        }

        public void DeleteFeature()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _deleteFeature =
                    from f in pc.Features
                    where f.FeatureId == SelectedFeature.FeatureId
                    select f;

                foreach (var item in _deleteFeature)
                {
                    pc.Features.Remove(item);
                }

                pc.SaveChanges();
            }
        }

        public void DeleteIssue()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _deleteIssue =
                    from i in pc.Issues
                    where i.IssueId == SelectedIssue.IssueId
                    select i;

                foreach (var item in _deleteIssue)
                {
                    pc.Issues.Remove(item);
                }

                pc.SaveChanges();
            }
        }

        public void DeleteNote()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _deleteNote =
                    from n in pc.Notes
                    where n.NoteId == SelectedNote.NoteId
                    select n;

                foreach (var item in _deleteNote)
                {
                    pc.Notes.Remove(item);
                }

                pc.SaveChanges();
            }
        }
    }
}
