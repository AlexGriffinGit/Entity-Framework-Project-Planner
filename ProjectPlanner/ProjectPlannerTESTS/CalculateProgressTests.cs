using NUnit.Framework;
using ProjectPlannerModel;
using ProjectPlannerBusiness;
using System.Linq;

namespace ProjectPlannerTESTS
{
    class CalculateProgressTests
    {
        private CRUDProjectManager _crudManager = new CRUDProjectManager();
        private CalculateProgress _calculateProgress = new CalculateProgress();

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
        public void MakeSureThatTheProgressCalculationReturnsTheExpectedValueAndIsCorrectForFeatures()
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

                //Worth 18%
                Feature _testFeat = new Feature()
                {
                    Title = "TestFeat",
                    Description = "This is a test feature",
                    Status = 2,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Worth 5%
                Feature _testFeat2 = new Feature()
                {
                    Title = "TestFeat2",
                    Description = "This is the second test feature",
                    Status = 0,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Worth 25%
                Feature _testFeat3 = new Feature()
                {
                    Title = "TestFeat3",
                    Description = "This is the third test feature",
                    Status = 3,
                    Priority = 99,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Worth 25%
                Feature _testFeat4 = new Feature()
                {
                    Title = "TestFeat4",
                    Description = "This is the fourth test feature",
                    Status = 3,
                    Priority = 99,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };



                pc.Features.Add(_testFeat);
                pc.Features.Add(_testFeat2);
                pc.Features.Add(_testFeat3);
                pc.Features.Add(_testFeat4);

                pc.SaveChanges();

                int progress = _calculateProgress.CalculateProjectProgress(_testProj);

                Assert.AreEqual(73, progress);
            }
        }

        [Test]
        public void MakeSureThatTheProgressCalculationReturnsTheExpectedValueAndIsCorrectForFeaturesAndIssues()
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

                //Worth 18%
                Feature _testFeat = new Feature()
                {
                    Title = "TestFeat",
                    Description = "This is a test feature",
                    Status = 2,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Worth 5%
                Feature _testFeat2 = new Feature()
                {
                    Title = "TestFeat2",
                    Description = "This is the second test feature",
                    Status = 0,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Worth 25%
                Feature _testFeat3 = new Feature()
                {
                    Title = "TestFeat3",
                    Description = "This is the third test feature",
                    Status = 3,
                    Priority = 99,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Worth 25%
                Feature _testFeat4 = new Feature()
                {
                    Title = "TestFeat4",
                    Description = "This is the fourth test feature",
                    Status = 3,
                    Priority = 99,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Worth -10%
                Issue _testIssue = new Issue()
                {
                    Title = "TestIssue",
                    Description = "This is a test issue",
                    Status = 0,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Worth -3%
                Issue _testIssue2 = new Issue()
                {
                    Title = "TestIssue2",
                    Description = "This is the second test issue",
                    Status = 1,
                    Priority = 20,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Worth -2%
                Issue _testIssue3 = new Issue()
                {
                    Title = "TestIssue3",
                    Description = "This is the third test issue",
                    Status = 2,
                    Priority = 8,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Worth -1%
                Issue _testIssue4 = new Issue()
                {
                    Title = "TestIssue4",
                    Description = "This is the fourth test issue",
                    Status = 2,
                    Priority = 18,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Shouldn't Affect Progress
                Issue _testIssue5 = new Issue()
                {
                    Title = "TestIssue5",
                    Description = "This is the fifth test issue",
                    Status = 3,
                    Priority = 99,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_testFeat);
                pc.Features.Add(_testFeat2);
                pc.Features.Add(_testFeat3);
                pc.Features.Add(_testFeat4);

                pc.Issues.Add(_testIssue);
                pc.Issues.Add(_testIssue2);
                pc.Issues.Add(_testIssue3);
                pc.Issues.Add(_testIssue4);
                pc.Issues.Add(_testIssue5);

                pc.SaveChanges();

                int progress = _calculateProgress.CalculateProjectProgress(_testProj);

                Assert.AreEqual(57, progress);
            }
        }

        [Test]
        public void MakeSureThatTheProgressCalculationReturns100IfAllFeaturesAreCompleteAndAllIssuesAreResolved()
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

                //Worth 25%
                Feature _testFeat = new Feature()
                {
                    Title = "TestFeat",
                    Description = "This is a test feature",
                    Status = 3,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Worth 25%
                Feature _testFeat2 = new Feature()
                {
                    Title = "TestFeat2",
                    Description = "This is the second test feature",
                    Status = 3,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Worth 25%
                Feature _testFeat3 = new Feature()
                {
                    Title = "TestFeat3",
                    Description = "This is the third test feature",
                    Status = 3,
                    Priority = 99,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Worth 25%
                Feature _testFeat4 = new Feature()
                {
                    Title = "TestFeat4",
                    Description = "This is the fourth test feature",
                    Status = 3,
                    Priority = 99,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Shouldn't Affect Progress
                Issue _testIssue = new Issue()
                {
                    Title = "TestIssue",
                    Description = "This is a test issue",
                    Status = 3,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Shouldn't Affect Progress
                Issue _testIssue2 = new Issue()
                {
                    Title = "TestIssue2",
                    Description = "This is the second test issue",
                    Status = 3,
                    Priority = 20,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Shouldn't Affect Progress
                Issue _testIssue3 = new Issue()
                {
                    Title = "TestIssue3",
                    Description = "This is the third test issue",
                    Status = 3,
                    Priority = 8,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                //Shouldn't Affect Progress
                Issue _testIssue4 = new Issue()
                {
                    Title = "TestIssue4",
                    Description = "This is the fourth test issue",
                    Status = 3,
                    Priority = 18,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_testFeat);
                pc.Features.Add(_testFeat2);
                pc.Features.Add(_testFeat3);
                pc.Features.Add(_testFeat4);

                pc.Issues.Add(_testIssue);
                pc.Issues.Add(_testIssue2);
                pc.Issues.Add(_testIssue3);
                pc.Issues.Add(_testIssue4);

                pc.SaveChanges();

                int progress = _calculateProgress.CalculateProjectProgress(_testProj);

                Assert.AreEqual(100, progress);
            }
        }
    }
}
