using NUnit.Framework;
using ProjectPlannerModel;
using ProjectPlannerBusiness;
using System.Linq;
using System.Collections.Generic;

namespace ProjectPlannerTESTS
{
    class CRUDIssueTests
    {
        CRUDIssueManager _crudIssueManager = new CRUDIssueManager();
        CRUDProjectManager _crudProjectManager = new CRUDProjectManager();

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
                _crudProjectManager.SelectedProject = _tempProj;
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
                int numOfIssues = pc.Issues.ToList().Count;

                _crudIssueManager.CreateNewIssue("TestIssue", "This is a test issue", 1, 1, "No notes needed", _crudProjectManager);

                var _issueCount =
                    from i in pc.Issues
                    select i;

                Assert.AreEqual(numOfIssues + 1, _issueCount.Count());
            }
        }

        [Test]
        public void WhenANewProjectIsAddedTheDetailsAreCorrect()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudIssueManager.CreateNewIssue("TestIssue", "This is a test issue", 1, 1, "No notes needed", _crudProjectManager);

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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Issues.Add(_testIssue);

                pc.SaveChanges();

                List<Issue> _issueList = new List<Issue>();

                _issueList = _crudIssueManager.RetrieveAllIssues();

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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Issues.Add(_testIssue);

                pc.SaveChanges();

                _crudIssueManager.SetSelectedIssue(_testIssue);

                Assert.AreEqual(_testIssue, _crudIssueManager.SelectedIssue);
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Issues.Add(_testIssue);

                pc.SaveChanges();

                _crudIssueManager.SetSelectedIssue(_testIssue);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestIssue", _crudIssueManager.SelectedIssue.Title);
                    Assert.AreEqual("This is a test issue", _crudIssueManager.SelectedIssue.Description);
                    Assert.AreEqual(1, _crudIssueManager.SelectedIssue.Status);
                    Assert.AreEqual(1, _crudIssueManager.SelectedIssue.Priority);
                    Assert.AreEqual("No notes needed", _crudIssueManager.SelectedIssue.Notes);
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Issues.Add(_testIssue);

                pc.SaveChanges();

                _firstIssueID = _testIssue.IssueId;
                _firstProjectID = _crudProjectManager.SelectedProject.ProjectId;
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

                _crudProjectManager.SelectedProject = _secondProj;
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Issues.Add(_secondIssue);

                pc.SaveChanges();

                string _title = ""; string _desc = ""; int _status = 0; int _priority = 0; string _notes = "";

                foreach (var item in _crudIssueManager.RetrieveProjectIssues(_crudProjectManager))
                {
                    _title = item.Title;
                    _desc = item.Description;
                    _status = item.Status;
                    _priority = item.Priority;
                    _notes = item.Notes;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(1, _crudIssueManager.RetrieveProjectIssues(_crudProjectManager).Count);
                    Assert.AreEqual("TestIssue2", _title);
                    Assert.AreEqual("This is the 2nd test issue", _desc);
                    Assert.AreEqual(2, _status);
                    Assert.AreEqual(2, _priority);
                    Assert.AreEqual("No notes", _notes);
                    Assert.AreEqual(_crudProjectManager.SelectedProject.ProjectId, _secondIssue.ProjectId);
                    Assert.AreNotEqual(_firstIssueID, _secondIssue.IssueId);
                    Assert.AreNotEqual(_firstProjectID, _secondIssue.ProjectId);
                });
            }
        }

        [Test]
        public void WhenUpdatingASinglePropertyOfAnIssueEnsureTheInformationHasBeenUpdated()
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Issues.Add(_testIssue);

                pc.SaveChanges();

                int _key = _testIssue.IssueId;

                _crudIssueManager.SelectedIssue = _testIssue;

                _crudIssueManager.UpdateIssue("TestIssue", "This is an updated test issue", 1, 1, "No notes needed");

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestIssue", _crudIssueManager.SelectedIssue.Title);
                    Assert.AreEqual("This is an updated test issue", _crudIssueManager.SelectedIssue.Description);
                    Assert.AreEqual(1, _crudIssueManager.SelectedIssue.Status);
                    Assert.AreEqual(1, _crudIssueManager.SelectedIssue.Priority);
                    Assert.AreEqual("No notes needed", _crudIssueManager.SelectedIssue.Notes);
                    Assert.AreEqual(_key, _crudIssueManager.SelectedIssue.IssueId);
                });
            }
        }

        [Test]
        public void WhenUpdatingMultiplePropertiesOfAnIssueEnsureTheInformationHasBeenUpdated()
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Issues.Add(_testIssue);

                pc.SaveChanges();

                int _key = _testIssue.IssueId;

                _crudIssueManager.SelectedIssue = _testIssue;

                _crudIssueManager.UpdateIssue("TestIssue", "This is an updated test issue", 2, 2, "No notes here");

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestIssue", _crudIssueManager.SelectedIssue.Title);
                    Assert.AreEqual("This is an updated test issue", _crudIssueManager.SelectedIssue.Description);
                    Assert.AreEqual(2, _crudIssueManager.SelectedIssue.Status);
                    Assert.AreEqual(2, _crudIssueManager.SelectedIssue.Priority);
                    Assert.AreEqual("No notes here", _crudIssueManager.SelectedIssue.Notes);
                    Assert.AreEqual(_key, _crudIssueManager.SelectedIssue.IssueId);
                });
            }
        }

        [Test]
        public void WhenNothingHasChangedWhenUpdateIsCalledEnsureTheInformationHasNotChanged()
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Issues.Add(_testIssue);

                pc.SaveChanges();

                int _key = _testIssue.IssueId;

                _crudIssueManager.SelectedIssue = _testIssue;

                _crudIssueManager.UpdateIssue("TestIssue", "This is a test issue", 1, 1, "No notes needed");

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestIssue", _crudIssueManager.SelectedIssue.Title);
                    Assert.AreEqual("This is a test issue", _crudIssueManager.SelectedIssue.Description);
                    Assert.AreEqual(1, _crudIssueManager.SelectedIssue.Status);
                    Assert.AreEqual(1, _crudIssueManager.SelectedIssue.Priority);
                    Assert.AreEqual("No notes needed", _crudIssueManager.SelectedIssue.Notes);
                    Assert.AreEqual(_key, _crudIssueManager.SelectedIssue.IssueId);
                });
            }
        }

        [Test]
        public void WhenAnIssueIsDeletedMakeSureItIsRemovedFromTheDatabase()
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Issues.Add(_testIssue);

                pc.SaveChanges();

                int _key = _testIssue.IssueId;

                _crudIssueManager.SelectedIssue = _testIssue;

                _crudIssueManager.DeleteIssue();

                var _containsDeleted =
                    from i in pc.Issues
                    where i.IssueId == _key
                    select i;

                Assert.IsEmpty(_containsDeleted);
            }
        }

        [Test]
        public void WhenAProjectIsDeletedMakeSureTheIssuesAreRemovedFromTheDatabase()
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Issues.Add(_testIssue);

                pc.SaveChanges();

                int _key = _testIssue.IssueId;

                _crudIssueManager.SelectedIssue = _testIssue;

                _crudProjectManager.DeleteProject();

                var _containsDeleted =
                     from i in pc.Issues
                     where i.IssueId == _key
                     select i;

                Assert.IsEmpty(_containsDeleted);
            }
        }
    }
}
