using NUnit.Framework;
using ProjectPlannerModel;
using ProjectPlannerBusiness;
using System.Linq;
using System.Collections.Generic;

namespace ProjectPlannerTESTS
{
    class CRUDNoteTests
    {
        CRUDManager _crudManager = new CRUDManager();

        [SetUp]
        public void Setup()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _selectedNote =
                   from n in pc.Notes
                   where n.Title == "TestNote"
                   select n;

                pc.Notes.RemoveRange(_selectedNote);

                pc.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _selectedNote =
                   from n in pc.Notes
                   where n.Title == "TestNote"
                   select n;

                pc.Notes.RemoveRange(_selectedNote);

                pc.SaveChanges();
            }
        }

        [Test]
        public void WhenANewNoteIsAddedTheNumberOfNotesIncreasesByOne()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewNote("TestNote", "A test note");

                var _noteCount =
                    from n in pc.Notes
                    select n;

                Assert.AreEqual(1, _noteCount.Count());
            }
        }

        [Test]
        public void WhenANewNoteIsAddedTheDetailsAreCorrect()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewNote("TestNote", "A test note");

                var _noteDetails =
                   from n in pc.Notes
                   where n.Title == "TestNote"
                   select n;

                string _title = "", _body = "";

                foreach (var item in _noteDetails)
                {
                    _title = item.Title;
                    _body = item.Body;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestNote", _title);
                    Assert.AreEqual("A test note", _body);

                });
            }
        }

        [Test]
        public void WhenAListOfNotesIsRetrievedMakeSureItIsNotEmptyIfThereAreNotesInTheDatabase()
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

                List<Note> _noteList = new List<Note>();

                _noteList = _crudManager.RetrieveAllNotes();

                Assert.IsNotEmpty(_noteList);
            }
        }

        [Test]
        public void WhenANoteIsSelectedMakeSureItIsTheSelectedNoteInTheApplication()
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

                _crudManager.SetSelectedNote(_testNote);

                Assert.AreEqual(_testNote, _crudManager.SelectedNote);
            }
        }
    }
}
