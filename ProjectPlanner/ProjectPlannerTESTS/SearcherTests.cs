using NUnit.Framework;
using ProjectPlannerModel;
using ProjectPlannerBusiness;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ProjectPlannerTESTS
{
    class SearcherTests
    {
        private Searcher _searcher = new Searcher();
        private CRUDProjectManager _crudProjectManager = new CRUDProjectManager();

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

                _crudProjectManager.SelectedProject = _tempProj;
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
            }
        }

        [Test]
        public void MakeSureThatTheProjectIsReturnedWhenAttemptedToSearchForIt()
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

                List<Project> _foundProjects = _searcher.SearchProjects("blank");

                string _title = "";
                string _description = "";
                string _link = "";
                int _status = -5;

                foreach (var item in _foundProjects)
                {
                    _title = item.Title;
                    _description = item.Description;
                    _link = item.Link;
                    _status = item.Status;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestProj", _title);
                    Assert.AreEqual("A blank test project", _description);
                    Assert.AreEqual("No Link", _link);
                    Assert.AreEqual(1, _status);

                    Assert.IsNotEmpty(_foundProjects);
                });
            }
        }

        [Test]
        public void MakeSureThatTheProjectIsNotReturnedWhenAttemptedToSearchForAnUnrelatedString()
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

                List<Project> _foundProjects = _searcher.SearchProjects("Zero");

                string _title = "";
                string _description = "";
                string _link = "";
                int _status = -5;

                foreach (var item in _foundProjects)
                {
                    _title = item.Title;
                    _description = item.Description;
                    _link = item.Link;
                    _status = item.Status;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("", _title);
                    Assert.AreEqual("", _description);
                    Assert.AreEqual("", _link);
                    Assert.AreEqual(-5, _status);

                    Assert.IsEmpty(_foundProjects);
                });
            }
        }

        [Test]
        public void MakeSureThatTheFeatureIsReturnedWhenAttemptedToSearchForIt()
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

                List<Feature> _foundFeatures = _searcher.SearchFeatures("This is a test feature");

                string _title = "";
                string _description = "";
                string _notes = "";
                int _status = -5;
                int _priority = -5;

                foreach (var item in _foundFeatures)
                {
                    _title = item.Title;
                    _description = item.Description;
                    _notes = item.Notes;
                    _status = item.Status;
                    _priority = item.Priority;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestFeat", _title);
                    Assert.AreEqual("This is a test feature", _description);
                    Assert.AreEqual("No notes needed", _notes);
                    Assert.AreEqual(1, _status);
                    Assert.AreEqual(1, _priority);

                    Assert.IsNotEmpty(_foundFeatures);
                });
            }
        }

        [Test]
        public void MakeSureThatTheFeatureIsNotReturnedWhenAttemptedToSearchForAnUnrelatedString()
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

                List<Feature> _foundFeatures = _searcher.SearchFeatures("This is a test failure");

                string _title = "";
                string _description = "";
                string _notes = "";
                int _status = -5;
                int _priority = -5;

                foreach (var item in _foundFeatures)
                {
                    _title = item.Title;
                    _description = item.Description;
                    _notes = item.Notes;
                    _status = item.Status;
                    _priority = item.Priority;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("", _title);
                    Assert.AreEqual("", _description);
                    Assert.AreEqual("", _notes);
                    Assert.AreEqual(-5, _status);
                    Assert.AreEqual(-5, _priority);

                    Assert.IsEmpty(_foundFeatures);
                });
            }
        }

        [Test]
        public void MakeSureThatTheIssueIsReturnedWhenAttemptedToSearchForIt()
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

                List<Issue> _foundIssues = _searcher.SearchIssues("This is a test issue");

                string _title = "";
                string _description = "";
                string _notes = "";
                int _status = -5;
                int _priority = -5;

                foreach (var item in _foundIssues)
                {
                    _title = item.Title;
                    _description = item.Description;
                    _notes = item.Notes;
                    _status = item.Status;
                    _priority = item.Priority;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestIssue", _title);
                    Assert.AreEqual("This is a test issue", _description);
                    Assert.AreEqual("No notes needed", _notes);
                    Assert.AreEqual(1, _status);
                    Assert.AreEqual(1, _priority);

                    Assert.IsNotEmpty(_foundIssues);
                });
            }
        }

        [Test]
        public void MakeSureThatTheIssueIsNotReturnedWhenAttemptedToSearchForAnUnrelatedString()
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

                List<Issue> _foundIssues = _searcher.SearchIssues("This is a test failure");

                string _title = "";
                string _description = "";
                string _notes = "";
                int _status = -5;
                int _priority = -5;

                foreach (var item in _foundIssues)
                {
                    _title = item.Title;
                    _description = item.Description;
                    _notes = item.Notes;
                    _status = item.Status;
                    _priority = item.Priority;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("", _title);
                    Assert.AreEqual("", _description);
                    Assert.AreEqual("", _notes);
                    Assert.AreEqual(-5, _status);
                    Assert.AreEqual(-5, _priority);

                    Assert.IsEmpty(_foundIssues);
                });
            }
        }

        [Test]
        public void MakeSureThatTheNoteIsReturnedWhenAttemptedToSearchForIt()
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

                List<Note> _foundNotes = _searcher.SearchNotes("A test note");

                string _title = "";
                string _body = "";

                foreach (var item in _foundNotes)
                {
                    _title = item.Title;
                    _body = item.Body;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestNote", _title);
                    Assert.AreEqual("A test note", _body);

                    Assert.IsNotEmpty(_foundNotes);
                });
            }
        }

        [Test]
        public void MakeSureThatTheNoteIsNotReturnedWhenAttemptedToSearchForAnUnrelatedString()
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

                List<Note> _foundNotes = _searcher.SearchNotes("I can't find the note");

                string _title = "";
                string _body = "";

                foreach (var item in _foundNotes)
                {
                    _title = item.Title;
                    _body = item.Body;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("", _title);
                    Assert.AreEqual("", _body);

                    Assert.IsEmpty(_foundNotes);
                });
            }
        }

        [Test]
        public void MakeSureThatEverythingThatMatchesIsReturnedWhenAttemptingToSearch()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Project _testProj = new Project()
                {
                    Title = "TestProj",
                    Description = "This is a test project",
                    Status = 1,
                    Link = "No Link"
                };

                Project _testProj2 = new Project()
                {
                    Title = "TestProj2",
                    Description = "This is a testing project",
                    Status = 1,
                    Link = "No Link"
                };

                Project _testProj3 = new Project()
                {
                    Title = "TestProj3",
                    Description = "This is a test project I think",
                    Status = 1,
                    Link = "No Link"
                };


                pc.Projects.Add(_testProj);
                pc.Projects.Add(_testProj2);
                pc.Projects.Add(_testProj3);

                pc.SaveChanges();

                List<Project> _foundProjects = _searcher.SearchProjects("This is a test project");

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestProj", _foundProjects[0].Title);
                    Assert.AreEqual("TestProj3", _foundProjects[1].Title);
                    Assert.AreEqual("This is a test project", _foundProjects[0].Description);
                    Assert.AreEqual("This is a test project I think", _foundProjects[1].Description);
                    Assert.AreEqual(2, _foundProjects.Count);
                });
            }
        }
    }
}