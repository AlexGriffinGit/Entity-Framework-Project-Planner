using NUnit.Framework;
using ProjectPlannerModel;
using ProjectPlannerBusiness;
using System.Linq;

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
                var selectedNote =
                   from n in pc.Notes
                   where n.Title == "TestNote"
                   select n;

                pc.Notes.RemoveRange(selectedNote);

                pc.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var selectedNote =
                   from n in pc.Notes
                   where n.Title == "TestNote"
                   select n;

                pc.Notes.RemoveRange(selectedNote);

                pc.SaveChanges();
            }
        }

        [Test]
        public void WhenANewNoteIsAddedTheNumberOfNotesIncreasesByOne()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewNote("TestNote", "A test note");

                var noteCount =
                    from n in pc.Notes
                    select n;

                Assert.AreEqual(1, noteCount.Count());
            }
        }

        [Test]
        public void WhenANewNoteIsAddedTheDetailsAreCorrect()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                _crudManager.CreateNewNote("TestNote", "A test note");

                var noteDetails =
                   from n in pc.Notes
                   where n.Title == "TestNote"
                   select n;

                string title = "", body = "";

                foreach (var item in noteDetails)
                {
                    title = item.Title;
                    body = item.Body;
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("TestNote", title);
                    Assert.AreEqual("A test note", body);

                });
            }
        }
    }
}
