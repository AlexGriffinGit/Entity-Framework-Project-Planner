using NUnit.Framework;
using ProjectPlannerModel;
using ProjectPlannerBusiness;
using System.Linq;
using System.Collections.Generic;

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
                var _selectedFeature =
                   from f in pc.Features
                   where f.Title == "TestFeat"
                   select f;

                Project _tempProj = new Project()
                {
                    Title = "temp",
                    Description = "Temp Temp",
                    Status = 1,
                    Link = "Temporary"
                };

                pc.Features.RemoveRange(_selectedFeature);
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
                var _selectedFeature =
                   from f in pc.Features
                   where f.Title == "TestFeat"
                   select f;

                var _selectedProject =
                    from p in pc.Projects
                    where p.Title == "temp"
                    select p;

                pc.Projects.RemoveRange(_selectedProject);
                pc.Features.RemoveRange(_selectedFeature);

                pc.SaveChanges();
            }
        }

        [Test]
        public void WhenANewFeatureIsAddedTheNumberOfFeaturesIncreasesByOne()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewFeature("TestFeat", "This is a test feature", 1, 1, "No notes needed");

                var _featureCount =
                    from f in pc.Features
                    select f;

                Assert.AreEqual(1, _featureCount.Count());
            }
        }

        [Test]
        public void WhenANewFeatureIsAddedTheDetailsAreCorrect()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewFeature("TestFeat", "This is a test feature", 1, 1, "No notes needed");

                var _featureDetails =
                   from f in pc.Features
                   where f.Title == "TestFeat"
                   select f;

                string _title = "", _description = "", _notes = "";
                int _status = -5, _priority = -5;

                foreach (var item in _featureDetails)
                {
                    _title = item.Title;
                    _description = item.Description;
                    _status = item.Status;
                    _priority = item.Priority;
                    _notes = item.Notes;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestFeat", _title);
                    Assert.AreEqual("This is a test feature", _description);
                    Assert.AreEqual(1, _status);
                    Assert.AreEqual(1, _priority);
                    Assert.AreEqual("No notes needed", _notes);
                });
            }
        }

        [Test]
        public void WhenAListOfFeaturesIsRetrievedMakeSureItIsNotEmptyIfThereAreFeaturesInTheDatabase()
        {
            using (PlannerContext pc = new PlannerContext())
            {
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

                List<Feature> _featureList = new List<Feature>();

                _featureList = _crudManager.RetrieveAllFeatures();

                Assert.IsNotEmpty(_featureList);
            }
        }
    }
}
