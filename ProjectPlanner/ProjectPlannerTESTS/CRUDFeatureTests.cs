using NUnit.Framework;
using ProjectPlannerModel;
using ProjectPlannerBusiness;
using System.Linq;

namespace ProjectPlannerTESTS
{
    class CRUDFeatureTests
    {
        CRUDManager _crudManager = new CRUDManager();

        [SetUp]
        public void Setup()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var selectedFeature =
                   from f in pc.Features
                   where f.Title == "TestFeat"
                   select f;

                Project TempProj = new Project()
                {
                    Title = "temp",
                    Description = "Temp Temp",
                    Status = 1,
                    Link = "Temporary"
                };

                pc.Features.RemoveRange(selectedFeature);
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
                var selectedFeature =
                   from f in pc.Features
                   where f.Title == "TestFeat"
                   select f;

                var selectedProject =
                    from p in pc.Projects
                    where p.Title == "temp"
                    select p;

                pc.Projects.RemoveRange(selectedProject);
                pc.Features.RemoveRange(selectedFeature);

                pc.SaveChanges();
            }
        }

        [Test]
        public void WhenANewFeatureIsAddedTheNumberOfFeaturesIncreasesByOne()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewFeature("TestFeat", "This is a test feature", 1, 1, "No notes needed");

                var featureCount =
                    from f in pc.Features
                    select f;

                Assert.AreEqual(1, featureCount.Count());
            }
        }

        [Test]
        public void WhenANewFeatureIsAddedTheDetailsAreCorrect()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewFeature("TestFeat", "This is a test feature", 1, 1, "No notes needed");

                var featureDetails =
                   from f in pc.Features
                   where f.Title == "TestFeat"
                   select f;

                string title = "", description = "", notes = "";
                int status = -5, priority = -5;

                foreach (var item in featureDetails)
                {
                    title = item.Title;
                    description = item.Description;
                    status = item.Status;
                    priority = item.Priority;
                    notes = item.Notes;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestFeat", title);
                    Assert.AreEqual("This is a test feature", description);
                    Assert.AreEqual(1, status);
                    Assert.AreEqual(1, priority);
                    Assert.AreEqual("No notes needed", notes);
                });
            }
        }
    }
}
