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
                _crudManager.SelectedProject = _tempProj;
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

                var _secondProject =
                    from p in pc.Projects
                    where p.Title == "temp2"
                    select p;

                pc.Projects.RemoveRange(_selectedProject);
                pc.Projects.RemoveRange(_secondProject);
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

                Assert.AreEqual(2, _issueCount.Count());
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

        [Test]
        public void WhenAIssueIsSelectedMakeSureItIsTheSelectedIssueInTheApplication()
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

                _crudManager.SetSelectedIssue(_testIssue);

                Assert.AreEqual(_testIssue, _crudManager.SelectedIssue);
            }
        }

        [Test]
        public void WhenAnIssueIsSelectedMakeSureTheInformationIsCorrect()
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

                _crudManager.SetSelectedIssue(_testIssue);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestIssue", _crudManager.SelectedIssue.Title);
                    Assert.AreEqual("This is a test issue", _crudManager.SelectedIssue.Description);
                    Assert.AreEqual(1, _crudManager.SelectedIssue.Status);
                    Assert.AreEqual(1, _crudManager.SelectedIssue.Priority);
                    Assert.AreEqual("No notes needed", _crudManager.SelectedIssue.Notes);
                });
            }
        }

        [Test]
        public void WhenAnIssueIsAddedMakeSureTheProjectIssueMethodRetrievesTheIssuesFromThatProjectOnly()
        {
            int _firstProjectID;
            int _firstIssueID;

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

                _firstIssueID = _testIssue.IssueId;
                _firstProjectID = _crudManager.SelectedProject.ProjectId;
            }

            using (PlannerContext pc = new PlannerContext())
            {
                Project _secondProj = new Project()
                {
                    Title = "temp2",
                    Description = "Second test project",
                    Status = 2,
                    Link = "no link"
                };

                pc.Projects.Add(_secondProj);
                pc.SaveChanges();

                _crudManager.SelectedProject = _secondProj;
            }

            using (PlannerContext pc = new PlannerContext())
            {
                Issue _secondIssue = new Issue()
                {
                    Title = "TestIssue2",
                    Description = "This is the 2nd test issue",
                    Status = 2,
                    Priority = 2,
                    Notes = "No notes",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                pc.Issues.Add(_secondIssue);

                pc.SaveChanges();

                string _title = ""; string _desc = ""; int _status = 0; int _priority = 0; string _notes = "";

                foreach (var item in _crudManager.RetrieveProjectIssues())
                {
                    _title = item.Title;
                    _desc = item.Description;
                    _status = item.Status;
                    _priority = item.Priority;
                    _notes = item.Notes;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(1, _crudManager.RetrieveProjectIssues().Count);
                    Assert.AreEqual("TestIssue2", _title);
                    Assert.AreEqual("This is the 2nd test issue", _desc);
                    Assert.AreEqual(2, _status);
                    Assert.AreEqual(2, _priority);
                    Assert.AreEqual("No notes", _notes);
                    Assert.AreEqual(_crudManager.SelectedProject.ProjectId, _secondIssue.ProjectId);
                    Assert.AreNotEqual(_firstIssueID, _secondIssue.IssueId);
                    Assert.AreNotEqual(_firstProjectID, _secondIssue.ProjectId);
                });
            }
        }
    }
}
