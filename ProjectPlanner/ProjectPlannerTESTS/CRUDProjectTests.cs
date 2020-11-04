using NUnit.Framework;
using ProjectPlannerModel;
using ProjectPlannerBusiness;
using System.Linq;

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
                var selectedProject =
                   from p in pc.Projects
                   where p.Title == "TestProj"
                   select p;

                pc.Projects.RemoveRange(selectedProject);

                pc.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var selectedProject =
                   from p in pc.Projects
                   where p.Title == "TestProj"
                   select p;

                pc.Projects.RemoveRange(selectedProject);

                pc.SaveChanges();
            }
        }

        [Test]
        public void WhenANewProjectIsAddedTheNumberOfProjectsIncreasesByOne()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewProject("TestProj", "A blank test project", 0, "No link");

                var projectCount =
                    from p in pc.Projects
                    select p;

                Assert.AreEqual(1, projectCount.Count());
            }
        }

        [Test]
        public void WhenANewProjectIsAddedTheDetailsAreCorrect()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewProject("TestProj", "A blank test project", 0, "No link");

                var projectDetails =
                   from p in pc.Projects
                   where p.Title == "TestProj"
                   select p;

                string title = "", description ="", link = "";
                int status = -5;

                foreach (var item in projectDetails)
                {
                    title = item.Title;
                    description = item.Description;
                    status = item.Status;
                    link = item.Link;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestProj", title);
                    Assert.AreEqual("A blank test project", description);
                    Assert.AreEqual(0, status);
                    Assert.AreEqual("No link", link);
                });
            }
        }
    }
}