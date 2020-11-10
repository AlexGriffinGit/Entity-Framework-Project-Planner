using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using ProjectPlannerModel;

namespace ProjectPlannerBusiness
{
    public class CRUDNoteManager
    {
        public Note SelectedNote { get; set; }

        public List<Note> RetrieveAllNotes()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                return pc.Notes.ToList();
            }
        }

        public void SetSelectedNote(object selected)
        {
            SelectedNote = (Note)selected;
        }

        public void CreateNewNote(string title, string body)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Note _newNote = new Note()
                {
                    Title = title,
                    Body = body
                };

                pc.Notes.Add(_newNote);

                pc.SaveChanges();
            }
        }

        public void UpdateNote(string title, string body)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _updateNote =
                    from n in pc.Notes
                    where n.NoteId == SelectedNote.NoteId
                    select n;

                foreach (var note in _updateNote)
                {
                    note.Title = title;
                    note.Body = body;

                    SelectedNote = note;
                }

                pc.SaveChanges();
            }
        }

        public void DeleteNote()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _deleteNote =
                    from n in pc.Notes
                    where n.NoteId == SelectedNote.NoteId
                    select n;

                foreach (var item in _deleteNote)
                {
                    pc.Notes.Remove(item);
                }

                pc.SaveChanges();
            }
        }
    }
}
