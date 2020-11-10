using System.Collections.Generic;
using System.Linq;
using ProjectPlannerModel;

namespace ProjectPlannerBusiness
{
    public class CRUDProjectManager
    {
        public Project SelectedProject { get; set; }

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
    }
}
