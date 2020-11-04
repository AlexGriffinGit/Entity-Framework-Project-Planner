using NUnit.Framework;
using ProjectPlannerModel;
using ProjectPlannerBusiness;
using System.Linq;

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
                var selectedIssue =
                   from i in pc.Issues
                   where i.Title == "TestIssue"
                   select i;

                Project TempProj = new Project()
                {
                    Title = "temp",
                    Description = "Temp Temp",
                    Status = 1,
                    Link = "Temporary"
                };

                pc.Issues.RemoveRange(selectedIssue);
                pc.Projects.Add(TempProj);

                pc.SaveChanges();
                _crudManager.SetSelectedProject(TempProj);
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var selectedIssue =
                   from i in pc.Issues
                   where i.Title == "TestIssue"
                   select i;

                var selectedProject =
                    from p in pc.Projects
                    where p.Title == "temp"
                    select p;

                pc.Projects.RemoveRange(selectedProject);
                pc.Issues.RemoveRange(selectedIssue);
                
                pc.SaveChanges();
            }
        }

        [Test]
        public void WhenANewIssueIsAddedTheNumberOfIssuesIncreasesByOne()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewIssue("TestIssue", "This is a test issue", 1, 1, "No notes needed");

                var issueCount =
                    from i in pc.Issues
                    select i;

                Assert.AreEqual(1, issueCount.Count());
            }
        }

        [Test]
        public void WhenANewProjectIsAddedTheDetailsAreCorrect()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewIssue("TestIssue", "This is a test issue", 1, 1, "No notes needed");

                var selectedIssue =
                   from i in pc.Issues
                   where i.Title == "TestIssue"
                   select i;

                string title = "", description = "", notes = "";
                int status = -5, priority = -5;

                foreach (var item in selectedIssue)
                {
                    title = item.Title;
                    description = item.Description;
                    status = item.Status;
                    priority = item.Priority;
                    notes = item.Notes;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestIssue", title);
                    Assert.AreEqual("This is a test issue", description);
                    Assert.AreEqual(1, status);
                    Assert.AreEqual(1, priority);
                    Assert.AreEqual("No notes needed", notes);
                });
            }
        }
    }
}
