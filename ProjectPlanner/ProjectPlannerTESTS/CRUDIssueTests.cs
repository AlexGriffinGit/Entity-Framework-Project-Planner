using NUnit.Framework;
using ProjectPlannerModel;
using ProjectPlannerBusiness;
using System.Linq;
using System.Collections.Generic;

namespace ProjectPlannerTESTS
{
    class CRUDIssueTests
    {
        CRUDManager _crudManager = new CRUDManager();

        [SetUp]
        public void Setup()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _selectedIssue =
                   from i in pc.Issues
                   where i.Title == "TestIssue"
                   select i;

                Project _tempProj = new Project()
                {
                    Title = "temp",
                    Description = "Temp Temp",
                    Status = 1,
                    Link = "Temporary"
                };

                pc.Issues.RemoveRange(_selectedIssue);
                pc.Projects.Add(_tempProj);

                pc.SaveChanges();
                _crudManager.SetSelectedProject(_tempProj);
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _selectedIssue =
                   from i in pc.Issues
                   where i.Title == "TestIssue"
                   select i;

                var _selectedProject =
                    from p in pc.Projects
                    where p.Title == "temp"
                    select p;

                pc.Projects.RemoveRange(_selectedProject);
                pc.Issues.RemoveRange(_selectedIssue);
                
                pc.SaveChanges();
            }
        }

        [Test]
        public void WhenANewIssueIsAddedTheNumberOfIssuesIncreasesByOne()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewIssue("TestIssue", "This is a test issue", 1, 1, "No notes needed");

                var _issueCount =
                    from i in pc.Issues
                    select i;

                Assert.AreEqual(1, _issueCount.Count());
            }
        }

        [Test]
        public void WhenANewProjectIsAddedTheDetailsAreCorrect()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewIssue("TestIssue", "This is a test issue", 1, 1, "No notes needed");

                var _selectedIssue =
                   from i in pc.Issues
                   where i.Title == "TestIssue"
                   select i;

                string _title = "", _description = "", _notes = "";
                int _status = -5, _priority = -5;

                foreach (var item in _selectedIssue)
                {
                    _title = item.Title;
                    _description = item.Description;
                    _status = item.Status;
                    _priority = item.Priority;
                    _notes = item.Notes;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestIssue", _title);
                    Assert.AreEqual("This is a test issue", _description);
                    Assert.AreEqual(1, _status);
                    Assert.AreEqual(1, _priority);
                    Assert.AreEqual("No notes needed", _notes);
                });
            }
        }

        [Test]
        public void WhenAListOfIssuesIsRetrievedMakeSureItIsNotEmptyIfThereAreIssuesInTheDatabase()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Issue _testIssue = new Issue()
                {
                    Title = "TestIssue",
                    Description = "This is a test issue",
                    Status = 1,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                pc.Issues.Add(_testIssue);

                pc.SaveChanges();

                List<Issue> _issueList = new List<Issue>();

                _issueList = _crudManager.RetrieveAllIssues();

                Assert.IsNotEmpty(_issueList);
            }
        }
    }
}
