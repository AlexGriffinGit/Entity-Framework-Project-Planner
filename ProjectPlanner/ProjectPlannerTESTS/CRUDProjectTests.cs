using NUnit.Framework;
using ProjectPlannerModel;
using ProjectPlannerBusiness;
using System.Linq;
using System.Collections.Generic;

namespace ProjectPlannerTESTS
{
    public class CRUDProjectTests
    {
        CRUDManager _crudManager = new CRUDManager();

        [SetUp]
        public void Setup()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _selectedProject =
                   from p in pc.Projects
                   where p.Title == "TestProj"
                   select p;

                var _secondProject =
                   from p in pc.Projects
                   where p.Title == "TestProj2"
                   select p;

                pc.Projects.RemoveRange(_selectedProject);
                pc.Projects.RemoveRange(_secondProject);

                pc.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _selectedProject =
                   from p in pc.Projects
                   where p.Title == "TestProj"
                   select p;

                var _secondProject =
                   from p in pc.Projects
                   where p.Title == "TestProj2"
                   select p;

                pc.Projects.RemoveRange(_selectedProject);
                pc.Projects.RemoveRange(_secondProject);

                pc.SaveChanges();
            }
        }

        [Test]
        public void WhenANewProjectIsAddedTheNumberOfProjectsIncreasesByOne()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                int numOfProjects = pc.Projects.ToList().Count;

                _crudManager.CreateNewProject("TestProj", "A blank test project", 0, "No link");

                var _projectCount =
                    from p in pc.Projects
                    select p;

                Assert.AreEqual(numOfProjects + 1, _projectCount.Count());
            }
        }

        [Test]
        public void WhenANewProjectIsAddedTheDetailsAreCorrect()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewProject("TestProj", "A blank test project", 0, "No link");

                var _projectDetails =
                   from p in pc.Projects
                   where p.Title == "TestProj"
                   select p;

                string _title = "", _description ="", _link = "";
                int _status = -5;

                foreach (var item in _projectDetails)
                {
                    _title = item.Title;
                    _description = item.Description;
                    _status = item.Status;
                    _link = item.Link;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestProj", _title);
                    Assert.AreEqual("A blank test project", _description);
                    Assert.AreEqual(0, _status);
                    Assert.AreEqual("No link", _link);
                });
            }
        }

        [Test]
        public void WhenAListOfProjectsIsRetrievedMakeSureItIsNotEmptyIfThereAreProjectsInTheDatabase()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Project _testProj = new Project()
                {
                    Title = "TestProj",
                    Description = "A blank test project",
                    Status = 1,
                    Link = "No Link"
                };

                pc.Projects.Add(_testProj);

                pc.SaveChanges();

                List<Project> _projectList = new List<Project>();

                _projectList = _crudManager.RetrieveAllProjects();

                Assert.IsNotEmpty(_projectList);
            }
        }

        [Test]
        public void WhenAProjectIsSelectedMakeSureItIsTheSelectedProjectInTheApplication()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Project _testProj = new Project()
                {
                    Title = "TestProj",
                    Description = "A blank test project",
                    Status = 1,
                    Link = "No Link"
                };

                pc.Projects.Add(_testProj);

                pc.SaveChanges();

                _crudManager.SetSelectedProject(_testProj);

                Assert.AreEqual(_testProj, _crudManager.SelectedProject);
            }
        }

        [Test]
        public void WhenAProjectIsSelectedMakeSureTheInformationIsCorrect()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Project _testProj = new Project()
                {
                    Title = "TestProj",
                    Description = "A blank test project",
                    Status = 1,
                    Link = "No Link"
                };

                pc.Projects.Add(_testProj);

                pc.SaveChanges();

                _crudManager.SetSelectedProject(_testProj);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestProj", _crudManager.SelectedProject.Title);
                    Assert.AreEqual("A blank test project", _crudManager.SelectedProject.Description);
                    Assert.AreEqual(1, _crudManager.SelectedProject.Status);
                    Assert.AreEqual("No Link", _crudManager.SelectedProject.Link);
                });
            }
        }

        [Test]
        public void WhenUpdatingASinglePropertyOfAProjectEnsureTheInformationHasBeenUpdated()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Project _testProj = new Project()
                {
                    Title = "TestProj",
                    Description = "A blank test project",
                    Status = 1,
                    Link = "No Link"
                };

                pc.Projects.Add(_testProj);

                pc.SaveChanges();

                int _key = _testProj.ProjectId;

                _crudManager.SelectedProject = _testProj;

                _crudManager.UpdateProject("TestProj", "I've updated this test project", 1, "No Link");

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestProj", _crudManager.SelectedProject.Title);
                    Assert.AreEqual("I've updated this test project", _crudManager.SelectedProject.Description);
                    Assert.AreEqual(1, _crudManager.SelectedProject.Status);
                    Assert.AreEqual("No Link", _crudManager.SelectedProject.Link);
                    Assert.AreEqual(_key, _crudManager.SelectedProject.ProjectId);
                });
            }
        }

        [Test]
        public void WhenUpdatingMultiplePropertiesOfAProjectEnsureTheInformationHasBeenUpdated()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Project _testProj = new Project()
                {
                    Title = "TestProj",
                    Description = "A blank test project",
                    Status = 1,
                    Link = "No Link"
                };

                pc.Projects.Add(_testProj);

                pc.SaveChanges();

                int _key = _testProj.ProjectId;

                _crudManager.SelectedProject = _testProj;

                _crudManager.UpdateProject("TestProj", "I've updated this test project", 3, "www.google.com");

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestProj", _crudManager.SelectedProject.Title);
                    Assert.AreEqual("I've updated this test project", _crudManager.SelectedProject.Description);
                    Assert.AreEqual(3, _crudManager.SelectedProject.Status);
                    Assert.AreEqual("www.google.com", _crudManager.SelectedProject.Link);
                    Assert.AreEqual(_key, _crudManager.SelectedProject.ProjectId);
                });
            }
        }

        [Test]
        public void WhenNothingHasChangedWhenUpdateIsCalledEnsureTheInformationHasNotChanged()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Project _testProj = new Project()
                {
                    Title = "TestProj",
                    Description = "A blank test project",
                    Status = 1,
                    Link = "No Link"
                };

                pc.Projects.Add(_testProj);

                pc.SaveChanges();

                int _key = _testProj.ProjectId;

                _crudManager.SelectedProject = _testProj;

                _crudManager.UpdateProject("TestProj", "A blank test project", 1, "No Link");

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestProj", _crudManager.SelectedProject.Title);
                    Assert.AreEqual("A blank test project", _crudManager.SelectedProject.Description);
                    Assert.AreEqual(1, _crudManager.SelectedProject.Status);
                    Assert.AreEqual("No Link", _crudManager.SelectedProject.Link);
                    Assert.AreEqual(_key, _crudManager.SelectedProject.ProjectId);
                });
            }
        }

        [Test]
        public void WhenAProjectIsDeletedMakeSureItIsRemovedFromTheDatabase()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Project _testProj = new Project()
                {
                    Title = "TestProj",
                    Description = "A blank test project",
                    Status = 1,
                    Link = "No Link"
                };

                pc.Projects.Add(_testProj);

                pc.SaveChanges();

                int _key = 0;

                _crudManager.SelectedProject = _testProj;

                _crudManager.DeleteProject();

                var _containsDeleted =
                    from p in pc.Projects
                    where p.ProjectId == _key
                    select p;

                Assert.IsEmpty(_containsDeleted);
            }
        }

        [Test]
        public void WhenAProjectIsDeletedMakeSureItsFeaturesAreRemovedFromTheDatabaseAsWell()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Project _testProj = new Project()
                {
                    Title = "TestProj",
                    Description = "A blank test project",
                    Status = 1,
                    Link = "No Link"
                };

                pc.Projects.Add(_testProj);

                pc.SaveChanges();

                int _projectKey = _testProj.ProjectId;

                _crudManager.SelectedProject = _testProj;

                Feature _testFeat = new Feature()
                {
                    Title = "TestFeat",
                    Description = "This is a test feature",
                    Status = 1,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_testFeat);

                pc.SaveChanges();

                int _featureKey = _testFeat.FeatureId;

                _crudManager.DeleteProject();

                var _containsFeatureID =
                    from f in pc.Features
                    where f.FeatureId == _featureKey
                    select f;

                var _containsFeatureProjectID =
                    from f in pc.Features
                    where f.ProjectId == _projectKey
                    select f;

                Assert.Multiple(() =>
                {
                    Assert.IsEmpty(_containsFeatureID);
                    Assert.IsEmpty(_containsFeatureProjectID);
                });
            }
        }

        [Test]
        public void WhenAProjectIsDeletedMakeSureItsIssuesAreRemovedFromTheDatabaseAsWell()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Project _testProj = new Project()
                {
                    Title = "TestProj",
                    Description = "A blank test project",
                    Status = 1,
                    Link = "No Link"
                };

                pc.Projects.Add(_testProj);

                pc.SaveChanges();

                int _projectKey = _testProj.ProjectId;

                _crudManager.SelectedProject = _testProj;

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

                int _issueKey = _testIssue.IssueId;

                _crudManager.DeleteProject();

                var _containsIssueID =
                    from i in pc.Issues
                    where i.IssueId == _issueKey
                    select i;

                var _containsIssueProjectID =
                    from i in pc.Issues
                    where i.ProjectId == _projectKey
                    select i;

                Assert.Multiple(() =>
                {
                    Assert.IsEmpty(_containsIssueID);
                    Assert.IsEmpty(_containsIssueProjectID);
                });
            }
        }

        [Test]
        public void WhenANewProjectIsCreatedMakeSureThatTheIndexOfTheNewProjectIsCorrectlyReturnedByTheRetrieveIndexOfNewProjectMethod()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Project _testProj = new Project()
                {
                    Title = "TestProj",
                    Description = "A blank test project",
                    Status = 1,
                    Link = "No Link"
                };

                pc.Projects.Add(_testProj);

                pc.SaveChanges();

                _crudManager.SelectedProject = _testProj;

                int _firstProjectIndex = _crudManager.RetrieveIndexOfNewProject();

                Project _testProj2 = new Project()
                {
                    Title = "TestProj2",
                    Description = "the second blank test project",
                    Status = 2,
                    Link = "No Link here"
                };

                pc.Projects.Add(_testProj2);

                pc.SaveChanges();

                _crudManager.SelectedProject = _testProj2;

                int _secondProjectIndex = _crudManager.RetrieveIndexOfNewProject();

                List<Project> projects = pc.Projects.ToList();
                int index = 0;

                for (int i = 0; i < projects.Count; i++)
                {
                    if (projects[i].ProjectId == _crudManager.SelectedProject.ProjectId)
                    {
                        index = i;
                    }
                }

                Assert.Multiple(() =>
                {
                    Assert.AreNotEqual(_firstProjectIndex, index);
                    Assert.AreEqual(_secondProjectIndex, index);
                });
            }
        }
    }
}