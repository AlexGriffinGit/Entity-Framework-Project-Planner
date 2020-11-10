using System.Collections.Generic;
using System.Linq;
using ProjectPlannerModel;

namespace ProjectPlannerBusiness
{
    public class CRUDIssueManager
    {
        public Issue SelectedIssue { get; set; }

        public List<Issue> RetrieveAllIssues()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                return pc.Issues.ToList();
            }
        }

        public List<Issue> RetrieveProjectIssues(CRUDProjectManager currentProj)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                List<Issue> issues = new List<Issue>();

                var _issueIDs =
                    from i in pc.Issues
                    where i.ProjectId == currentProj.SelectedProject.ProjectId
                    orderby i.Priority
                    select i;

                foreach (var item in _issueIDs)
                {
                    issues.Add(item);
                }

                return issues;
            }
        }

        public void SetSelectedIssue(object selected)
        {
            SelectedIssue = (Issue)selected;
        }

        public void CreateNewIssue(string title, string description, int status, int priority, string notes, CRUDProjectManager currentProj)
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

                    ProjectId = currentProj.SelectedProject.ProjectId
                };

                pc.Issues.Add(_newIssue);

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
    }
}
