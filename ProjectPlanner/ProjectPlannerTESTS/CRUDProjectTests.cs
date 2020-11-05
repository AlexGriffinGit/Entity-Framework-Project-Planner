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

                pc.Projects.RemoveRange(_selectedProject);

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

                pc.Projects.RemoveRange(_selectedProject);

                pc.SaveChanges();
            }
        }

        [Test]
        public void WhenANewProjectIsAddedTheNumberOfProjectsIncreasesByOne()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewProject("TestProj", "A blank test project", 0, "No link");

                var _projectCount =
                    from p in pc.Projects
                    select p;

                Assert.AreEqual(1, _projectCount.Count());
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
    }
}