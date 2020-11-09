using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using ProjectPlannerModel;

namespace ProjectPlannerBusiness
{
    public class Searcher
    {
        private List<Project> _projectsFound = new List<Project>();
        private List<Feature> _featuresFound = new List<Feature>();
        private List<Issue> _issuesFound = new List<Issue>();
        private List<Note> _notesFound = new List<Note>();

        private CRUDManager _crudManager = new CRUDManager();

        private List<string> _projectStatus = new List<string>()
        {
            "Planning",
            "In Progress",
            "Testing",
            "Releasing",
            "Complete"
        };

        private List<string> _featureStatus = new List<string>()
        {
            "Planning",
            "In Development",
            "Testing",
            "Complete"
        };

        private List<string> _issueStatus = new List<string>()
        {
            "Aware",
            "In Progress",
            "Testing",
            "Resolved"
        };

        public List<Project> SearchProjects(string _searchTerm)
        {
            _searchTerm = _searchTerm.ToLower();

            _projectsFound.Clear();

            foreach (Project _project in _crudManager.RetrieveAllProjects())
            {
                bool _isAdded = false;

                if ( _project.ProjectId.ToString().ToLower().Contains(_searchTerm))
                {
                    _projectsFound.Add(_project);
                    _isAdded = true;
                }
                else if (_project.Title.ToLower().Contains(_searchTerm) || _project.Description.ToLower().Contains(_searchTerm) || _project.Link.ToLower().Contains(_searchTerm))
                {
                    if (_isAdded == false)
                    {
                        _projectsFound.Add(_project);
                        _isAdded = true;
                    }
                }
                else if (_projectStatus[_project.Status].ToLower().Contains(_searchTerm))
                {
                    if (_isAdded == false)
                    {
                        _projectsFound.Add(_project);
                        _isAdded = true;
                    }
                }

                foreach (Feature _features in _project.Features)
                {
                    if (_features.Title.ToLower().Contains(_searchTerm) || _features.Description.ToLower().Contains(_searchTerm))
                    {
                        if (_isAdded == false)
                        {
                            _projectsFound.Add(_project);
                            _isAdded = true;
                        }
                    }
                }

                foreach (Issue _issues in _project.Issues)
                {
                    if (_issues.Title.ToLower().Contains(_searchTerm) || _issues.Description.ToLower().Contains(_searchTerm))
                    {
                        if (_isAdded == false)
                        {
                            _projectsFound.Add(_project);
                            _isAdded = true;
                        }
                    }
                }
            }

            return _projectsFound;
        }

        public List<Feature> SearchFeatures(string _searchTerm)
        {
            _searchTerm = _searchTerm.ToLower();

            _featuresFound.Clear();

            foreach (Feature _feature in _crudManager.RetrieveAllFeatures())
            {
                bool _isAdded = false;

                if (_feature.FeatureId.ToString().Contains(_searchTerm) || _feature.ProjectId.ToString().Contains(_searchTerm) || _feature.Priority.ToString().Contains(_searchTerm))
                {
                    _featuresFound.Add(_feature);
                    _isAdded = true;
                }
                else if (_feature.Title.ToLower().Contains(_searchTerm) || _feature.Description.ToLower().Contains(_searchTerm) || _feature.Notes.ToLower().Contains(_searchTerm))
                {
                    if (_isAdded == false)
                    {
                        _featuresFound.Add(_feature);
                        _isAdded = true;
                    }
                }
                else if (_featureStatus[_feature.Status].ToLower().Contains(_searchTerm))
                {
                    if (_isAdded == false)
                    {
                        _featuresFound.Add(_feature);
                        _isAdded = true;
                    }
                }
            }

            return _featuresFound;
        }

        public List<Issue> SearchIssues(string _searchTerm)
        {
            _searchTerm = _searchTerm.ToLower();

            _issuesFound.Clear();

            foreach (Issue _issue in _crudManager.RetrieveAllIssues())
            {
                bool _isAdded = false;

                if (_issue.IssueId.ToString().Contains(_searchTerm) || _issue.ProjectId.ToString().Contains(_searchTerm) || _issue.Priority.ToString().Contains(_searchTerm))
                {
                    _issuesFound.Add(_issue);
                    _isAdded = true;
                }
                else if (_issue.Title.ToLower().Contains(_searchTerm) || _issue.Description.ToLower().Contains(_searchTerm) || _issue.Notes.ToLower().Contains(_searchTerm))
                {
                    if (_isAdded == false)
                    {
                        _issuesFound.Add(_issue);
                        _isAdded = true;
                    }
                }
                else if (_issueStatus[_issue.Status].ToLower().Contains(_searchTerm))
                {
                    if (_isAdded == false)
                    {
                        _issuesFound.Add(_issue);
                        _isAdded = true;
                    }
                }
            }

            return _issuesFound;
        }

        public List<Note> SearchNotes(string _searchTerm)
        {
            _searchTerm = _searchTerm.ToLower();

            _notesFound.Clear();

            foreach (Note _note in _crudManager.RetrieveAllNotes())
            {
                bool _isAdded = false;

                if (_note.NoteId.ToString().Contains(_searchTerm))
                {
                    _notesFound.Add(_note);
                    _isAdded = true;
                }
                else if (_note.Title.ToLower().Contains(_searchTerm) || _note.Body.ToLower().Contains(_searchTerm))
                {
                    if (_isAdded == false)
                    {
                        _notesFound.Add(_note);
                        _isAdded = true;
                    } 
                }
            }

            return _notesFound;
        }
    }
}
