using NUnit.Framework;
using ProjectPlannerModel;
using ProjectPlannerBusiness;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace ProjectPlannerTESTS
{
    class JSONExporterTests
    {
        private CRUDManager _crudManager = new CRUDManager();
        private JSONExporter _jsonExporter = new JSONExporter();

        [SetUp]
        public void Setup()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _selectedProject =
                   from p in pc.Projects
                   where p.Title == "TestProj"
                   select p;

                var _selectedFeature =
                   from f in pc.Features
                   where f.Title == "TestFeat"
                   select f;

                var _selectedIssue =
                   from i in pc.Issues
                   where i.Title == "TestIssue"
                   select i;

                var _selectedNote =
                   from n in pc.Notes
                   where n.Title == "TestNote"
                   select n;

                Project _tempProj = new Project()
                {
                    Title = "temp",
                    Description = "Temp Temp",
                    Status = 1,
                    Link = "Temporary"
                };

                pc.RemoveRange(_selectedProject);
                pc.RemoveRange(_selectedFeature);
                pc.RemoveRange(_selectedIssue);
                pc.RemoveRange(_selectedNote);

                pc.Projects.Add(_tempProj);

                pc.SaveChanges();

                _crudManager.SelectedProject = _tempProj;

                File.Delete(@"C:\Users\Alex\Documents\Engineering 73\Entity Framework Project\Entity-Framework-Project-Planner\ProjectPlanner\ProjectPlannerTESTS\bin\Debug\netcoreapp3.1\JSON Export\Projects.json");
                File.Delete(@"C:\Users\Alex\Documents\Engineering 73\Entity Framework Project\Entity-Framework-Project-Planner\ProjectPlanner\ProjectPlannerTESTS\bin\Debug\netcoreapp3.1\JSON Export\Features.json");
                File.Delete(@"C:\Users\Alex\Documents\Engineering 73\Entity Framework Project\Entity-Framework-Project-Planner\ProjectPlanner\ProjectPlannerTESTS\bin\Debug\netcoreapp3.1\JSON Export\Issues.json");
                File.Delete(@"C:\Users\Alex\Documents\Engineering 73\Entity Framework Project\Entity-Framework-Project-Planner\ProjectPlanner\ProjectPlannerTESTS\bin\Debug\netcoreapp3.1\JSON Export\Notes.json");
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

                var _selectedProject2 =
                   from p in pc.Projects
                   where p.Title == "TestProj2"
                   select p;

                var _selectedProject3 =
                   from p in pc.Projects
                   where p.Title == "TestProj3"
                   select p;

                var _selectedFeature =
                   from f in pc.Features
                   where f.Title == "TestFeat"
                   select f;

                var _selectedIssue =
                   from i in pc.Issues
                   where i.Title == "TestIssue"
                   select i;

                var _selectedNote =
                   from n in pc.Notes
                   where n.Title == "TestNote"
                   select n;

                var _tempProj =
                    from p in pc.Projects
                    where p.Title == "temp"
                    select p;

                pc.RemoveRange(_selectedProject);
                pc.RemoveRange(_selectedProject2);
                pc.RemoveRange(_selectedProject3);
                pc.RemoveRange(_selectedFeature);
                pc.RemoveRange(_selectedIssue);
                pc.RemoveRange(_selectedNote);
                pc.RemoveRange(_tempProj);

                pc.SaveChanges();

                File.Delete(@"C:\Users\Alex\Documents\Engineering 73\Entity Framework Project\Entity-Framework-Project-Planner\ProjectPlanner\ProjectPlannerTESTS\bin\Debug\netcoreapp3.1\JSON Export\Projects.json");
                File.Delete(@"C:\Users\Alex\Documents\Engineering 73\Entity Framework Project\Entity-Framework-Project-Planner\ProjectPlanner\ProjectPlannerTESTS\bin\Debug\netcoreapp3.1\JSON Export\Features.json");
                File.Delete(@"C:\Users\Alex\Documents\Engineering 73\Entity Framework Project\Entity-Framework-Project-Planner\ProjectPlanner\ProjectPlannerTESTS\bin\Debug\netcoreapp3.1\JSON Export\Issues.json");
                File.Delete(@"C:\Users\Alex\Documents\Engineering 73\Entity Framework Project\Entity-Framework-Project-Planner\ProjectPlanner\ProjectPlannerTESTS\bin\Debug\netcoreapp3.1\JSON Export\Notes.json");
            }
        }

        [Test]
        public void AddAProjectAndCheckThatTheExportMethodHasExportedAJSONFile()
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

                _jsonExporter.InitSerialisation();

                _jsonExporter.SerialiseProjects();

                FileAssert.Exists(@"C:\Users\Alex\Documents\Engineering 73\Entity Framework Project\Entity-Framework-Project-Planner\ProjectPlanner\ProjectPlannerTESTS\bin\Debug\netcoreapp3.1\JSON Export\Projects.json");
            }
        }

        [Test]
        public void AddAFeatureAndCheckThatTheExportMethodHasExportedAJSONFile()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Feature _testFeat = new Feature()
                {
                    Title = "TestFeat",
                    Description = "This is a test feature",
                    Status = 2,
                    Priority = 1,
                    Notes = "No notes needed",
                    ProjectId = _crudManager.SelectedProject.ProjectId
                };

                pc.Features.Add(_testFeat);

                pc.SaveChanges();

                _jsonExporter.InitSerialisation();

                _jsonExporter.SerialiseFeatures();

                FileAssert.Exists(@"C:\Users\Alex\Documents\Engineering 73\Entity Framework Project\Entity-Framework-Project-Planner\ProjectPlanner\ProjectPlannerTESTS\bin\Debug\netcoreapp3.1\JSON Export\Features.json");
            }
        }

        [Test]
        public void AddAnIssueAndCheckThatTheExportMethodHasExportedAJSONFile()
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

                _jsonExporter.InitSerialisation();

                _jsonExporter.SerialiseIssues();

                FileAssert.Exists(@"C:\Users\Alex\Documents\Engineering 73\Entity Framework Project\Entity-Framework-Project-Planner\ProjectPlanner\ProjectPlannerTESTS\bin\Debug\netcoreapp3.1\JSON Export\Issues.json");
            }
        }

        [Test]
        public void AddANoteAndCheckThatTheExportMethodHasExportedAJSONFile()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Note _testNote = new Note()
                {
                    Title = "TestNote",
                    Body = "A test note",
                };

                pc.Notes.Add(_testNote);

                pc.SaveChanges();

                _jsonExporter.InitSerialisation();

                _jsonExporter.SerialiseNotes();

                FileAssert.Exists(@"C:\Users\Alex\Documents\Engineering 73\Entity Framework Project\Entity-Framework-Project-Planner\ProjectPlanner\ProjectPlannerTESTS\bin\Debug\netcoreapp3.1\JSON Export\Notes.json");
            }
        }
    }
}