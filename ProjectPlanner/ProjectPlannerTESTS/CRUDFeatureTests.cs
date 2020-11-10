using NUnit.Framework;
using ProjectPlannerModel;
using ProjectPlannerBusiness;
using System.Linq;
using System.Collections.Generic;

namespace ProjectPlannerTESTS
{
    class CRUDFeatureTests
    {
        CRUDFeatureManager _crudFeatureManager = new CRUDFeatureManager();
        CRUDProjectManager _crudProjectManager = new CRUDProjectManager();

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

                _crudProjectManager.SelectedProject = _tempProj;
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

                var _secondProject =
                    from p in pc.Projects
                    where p.Title == "temp2"
                    select p;

                pc.Projects.RemoveRange(_selectedProject);
                pc.Projects.RemoveRange(_secondProject);
                pc.Features.RemoveRange(_selectedFeature);
                
                pc.SaveChanges();
            }
        }

        [Test]
        public void WhenANewFeatureIsAddedTheNumberOfFeaturesIncreasesByOne()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                int numOfFeatures = pc.Features.ToList().Count;

                _crudFeatureManager.CreateNewFeature("TestFeat", "This is a test feature", 1, 1, "No notes needed", _crudProjectManager);

                var _featureCount =
                    from f in pc.Features
                    select f;

                Assert.AreEqual(numOfFeatures + 1, _featureCount.Count());
            }
        }

        [Test]
        public void WhenANewFeatureIsAddedTheDetailsAreCorrect()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudFeatureManager.CreateNewFeature("TestFeat", "This is a test feature", 1, 1, "No notes needed", _crudProjectManager);

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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_testFeat);

                pc.SaveChanges();

                List<Feature> _featureList = new List<Feature>();

                _featureList = _crudFeatureManager.RetrieveAllFeatures();

                Assert.IsNotEmpty(_featureList);
            }
        }

        [Test]
        public void WhenAFeatureIsSelectedMakeSureItIsTheSelectedFeatureInTheApplication()
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_testFeat);

                pc.SaveChanges();

                _crudFeatureManager.SetSelectedFeature(_testFeat);

                Assert.AreEqual(_testFeat, _crudFeatureManager.SelectedFeature);
            }
        }

        [Test]
        public void WhenAFeatureIsSelectedMakeSureTheInformationIsCorrect()
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_testFeat);

                pc.SaveChanges();

                _crudFeatureManager.SetSelectedFeature(_testFeat);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestFeat", _crudFeatureManager.SelectedFeature.Title);
                    Assert.AreEqual("This is a test feature", _crudFeatureManager.SelectedFeature.Description);
                    Assert.AreEqual(1, _crudFeatureManager.SelectedFeature.Status);
                    Assert.AreEqual(1, _crudFeatureManager.SelectedFeature.Priority);
                    Assert.AreEqual("No notes needed", _crudFeatureManager.SelectedFeature.Notes);
                });
            }
        }

        [Test]
        public void WhenAFeatureIsAddedMakeSureTheProjectFeatureMethodRetrievesTheFeaturesFromThatProjectOnly()
        {
            int _firstProjectID;
            int _firstFeatureID;

            using (PlannerContext pc = new PlannerContext())
            {
                Feature _testFeat = new Feature()
                {
                    Title = "TestFeat",
                    Description = "This is a test feature",
                    Status = 1,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_testFeat);

                pc.SaveChanges();

                _firstFeatureID = _testFeat.FeatureId;
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
                Feature _secondFeat = new Feature()
                {
                    Title = "TestFeat2",
                    Description = "This is the 2nd test feature",
                    Status = 2,
                    Priority = 2,
                    Notes = "No notes",
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_secondFeat);

                pc.SaveChanges();

                string _title = ""; string _desc = ""; int _status = 0; int _priority = 0; string _notes = "";

                foreach (var item in _crudFeatureManager.RetrieveProjectFeatures(_crudProjectManager))
                {
                    _title = item.Title;
                    _desc = item.Description;
                    _status = item.Status;
                    _priority = item.Priority;
                    _notes = item.Notes;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(1, _crudFeatureManager.RetrieveProjectFeatures(_crudProjectManager).Count);
                    Assert.AreEqual("TestFeat2", _title);
                    Assert.AreEqual("This is the 2nd test feature", _desc);
                    Assert.AreEqual(2, _status);
                    Assert.AreEqual(2, _priority);
                    Assert.AreEqual("No notes", _notes);
                    Assert.AreEqual(_crudProjectManager.SelectedProject.ProjectId, _secondFeat.ProjectId);
                    Assert.AreNotEqual(_firstFeatureID, _secondFeat.FeatureId);
                    Assert.AreNotEqual(_firstProjectID, _secondFeat.ProjectId);
                });
            }
        }

        [Test]
        public void WhenAnIncompleteFeatureIsAddedMakeSureTheNotCompletedListRetrievesTheIncompleteFeaturesOnly()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Feature _completeFeat = new Feature()
                {
                    Title = "TestFeat",
                    Description = "This is a test complete feature",
                    Status = 3,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                Feature _incompleteFeat = new Feature()
                {
                    Title = "TestFeat2",
                    Description = "This is the incomplete test feature",
                    Status = 1,
                    Priority = 2,
                    Notes = "No notes",
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_completeFeat);
                pc.Features.Add(_incompleteFeat);

                pc.SaveChanges();

                string _title = ""; string _desc = ""; int _status = 0; int _priority = 0; string _notes = ""; int _featureID = 0;

                foreach (var item in _crudFeatureManager.RetrieveToDoFeatures(_crudProjectManager))
                {
                    _title = item.Title;
                    _desc = item.Description;
                    _status = item.Status;
                    _priority = item.Priority;
                    _notes = item.Notes;
                    _featureID = item.FeatureId;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(1, _crudFeatureManager.RetrieveToDoFeatures(_crudProjectManager).Count);
                    Assert.AreEqual("This is the incomplete test feature", _desc);
                    Assert.AreEqual(1, _status);
                    Assert.AreEqual(2, _priority);
                    Assert.AreEqual("No notes", _notes);
                    Assert.AreNotEqual(_completeFeat.FeatureId, _featureID);
                });
            }
        }

        [Test]
        public void WhenACompleteFeatureIsAddedMakeSureTheCompletedListRetrievesTheCompleteFeaturesOnly()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Feature _completeFeat = new Feature()
                {
                    Title = "TestFeat",
                    Description = "This is the complete test feature",
                    Status = 3,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                Feature _incompleteFeat = new Feature()
                {
                    Title = "TestFeat2",
                    Description = "This is the incomplete test feature",
                    Status = 1,
                    Priority = 2,
                    Notes = "No notes",
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_completeFeat);
                pc.Features.Add(_incompleteFeat);

                pc.SaveChanges();

                string _title = ""; string _desc = ""; int _status = 0; int _priority = 0; string _notes = ""; int _featureID = 0;

                foreach (var item in _crudFeatureManager.RetrieveCompleteFeatures(_crudProjectManager))
                {
                    _title = item.Title;
                    _desc = item.Description;
                    _status = item.Status;
                    _priority = item.Priority;
                    _notes = item.Notes;
                    _featureID = item.FeatureId;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(1, _crudFeatureManager.RetrieveCompleteFeatures(_crudProjectManager).Count);
                    Assert.AreEqual("This is the complete test feature", _desc);
                    Assert.AreEqual(3, _status);
                    Assert.AreEqual(1, _priority);
                    Assert.AreEqual("No notes needed", _notes);
                    Assert.AreNotEqual(_incompleteFeat.FeatureId, _featureID);
                });
            }
        }

        [Test]
        public void WhenUpdatingASinglePropertyOfAFeatureEnsureTheInformationHasBeenUpdated()
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_testFeat);

                pc.SaveChanges();

                int _key = _testFeat.FeatureId;

                _crudFeatureManager.SelectedFeature = _testFeat;

                _crudFeatureManager.UpdateFeature("TestFeat", "This is an updated test feature", 1, 1, "No notes needed");

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestFeat", _crudFeatureManager.SelectedFeature.Title);
                    Assert.AreEqual("This is an updated test feature", _crudFeatureManager.SelectedFeature.Description);
                    Assert.AreEqual(1, _crudFeatureManager.SelectedFeature.Status);
                    Assert.AreEqual(1, _crudFeatureManager.SelectedFeature.Priority);
                    Assert.AreEqual("No notes needed", _crudFeatureManager.SelectedFeature.Notes);
                    Assert.AreEqual(_key, _crudFeatureManager.SelectedFeature.FeatureId);
                });
            }
        }

        [Test]
        public void WhenUpdatingMultiplePropertiesOfAProjectEnsureTheInformationHasBeenUpdated()
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_testFeat);

                pc.SaveChanges();

                int _key = _testFeat.FeatureId;

                _crudFeatureManager.SelectedFeature = _testFeat;

                _crudFeatureManager.UpdateFeature("TestFeat", "This is an updated test feature", 2, 3, "No notes here");

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestFeat", _crudFeatureManager.SelectedFeature.Title);
                    Assert.AreEqual("This is an updated test feature", _crudFeatureManager.SelectedFeature.Description);
                    Assert.AreEqual(2, _crudFeatureManager.SelectedFeature.Status);
                    Assert.AreEqual(3, _crudFeatureManager.SelectedFeature.Priority);
                    Assert.AreEqual("No notes here", _crudFeatureManager.SelectedFeature.Notes);
                    Assert.AreEqual(_key, _crudFeatureManager.SelectedFeature.FeatureId);
                });
            }
        }

        [Test]
        public void WhenNothingHasChangedWhenUpdateIsCalledEnsureTheInformationHasNotChanged()
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_testFeat);

                pc.SaveChanges();

                int _key = _testFeat.FeatureId;

                _crudFeatureManager.SelectedFeature = _testFeat;

                _crudFeatureManager.UpdateFeature("TestFeat", "This is a test feature", 1, 1, "No notes needed");

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestFeat", _crudFeatureManager.SelectedFeature.Title);
                    Assert.AreEqual("This is a test feature", _crudFeatureManager.SelectedFeature.Description);
                    Assert.AreEqual(1, _crudFeatureManager.SelectedFeature.Status);
                    Assert.AreEqual(1, _crudFeatureManager.SelectedFeature.Priority);
                    Assert.AreEqual("No notes needed", _crudFeatureManager.SelectedFeature.Notes);
                    Assert.AreEqual(_key, _crudFeatureManager.SelectedFeature.FeatureId);
                });
            }
        }

        [Test]
        public void WhenAFeatureIsDeletedMakeSureItIsRemovedFromTheDatabase()
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_testFeat);

                pc.SaveChanges();

                int _key = _testFeat.FeatureId;

                _crudFeatureManager.SelectedFeature = _testFeat;

                _crudFeatureManager.DeleteFeature();

                var _containsDeleted =
                    from f in pc.Features
                    where f.FeatureId == _key
                    select f;

                Assert.IsEmpty(_containsDeleted);
            }
        }

        [Test]
        public void WhenAProjectIsDeletedMakeSureTheFeaturesAreRemovedFromTheDatabase()
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
                    ProjectId = _crudProjectManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_testFeat);

                pc.SaveChanges();

                int _key = _testFeat.FeatureId;

                _crudFeatureManager.SelectedFeature = _testFeat;

                _crudProjectManager.DeleteProject();

                var _containsDeleted =
                    from f in pc.Features
                    where f.FeatureId == _key
                    select f;

                Assert.IsEmpty(_containsDeleted);
            }
        }
    }
}
