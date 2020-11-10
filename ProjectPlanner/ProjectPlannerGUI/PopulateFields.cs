using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore.Query;
using ProjectPlannerBusiness;

namespace ProjectPlannerGUI
{
    public class PopulateFields
    {
        private CalculateProgress _calculateProgress = new CalculateProgress();
        private AddExtender _addExtender = new AddExtender();

        public void PopulateComboBox()
        {
            ProjectPlannerMain.window.ProjectComboBox.ItemsSource = ProjectPlannerMain.window._crudProjectManager.RetrieveAllProjects();
        }

        public void PopulateProjectFields()
        {
            ProjectPlannerMain.window.ProjectIDText.Content = ProjectPlannerMain.window._crudProjectManager.SelectedProject.ProjectId;
            ProjectPlannerMain.window.ProjectTitleTextBox.Text = ProjectPlannerMain.window._crudProjectManager.SelectedProject.Title;
            ProjectPlannerMain.window.ProjectDescriptionTextBox.Text = ProjectPlannerMain.window._crudProjectManager.SelectedProject.Description;

            if (ProjectPlannerMain.window._crudProjectManager.SelectedProject.Status == -1)
            {
                ProjectPlannerMain.window.ProjectStatusComboBox.SelectedIndex = 0;
            }
            else
            {
                ProjectPlannerMain.window.ProjectStatusComboBox.SelectedIndex = ProjectPlannerMain.window._crudProjectManager.SelectedProject.Status;
            }

            ProjectPlannerMain.window.ProjectLinkTextBox.Text = ProjectPlannerMain.window._crudProjectManager.SelectedProject.Link;

            string _completeText = "";

            if (ProjectPlannerMain.window._crudFeatureManager.RetrieveCompleteFeatures(ProjectPlannerMain.window._crudProjectManager).Count > 0)
            {
                foreach (var item in ProjectPlannerMain.window._crudFeatureManager.RetrieveCompleteFeatures(ProjectPlannerMain.window._crudProjectManager))
                {
                    _completeText += $", { item.Title }";
                }
            }

            string _toDoText = "";

            foreach (var item in ProjectPlannerMain.window._crudFeatureManager.RetrieveToDoFeatures(ProjectPlannerMain.window._crudProjectManager))
            {
                if (item.Status > -1)
                {
                    _toDoText += $", { item.Title } - {ProjectPlannerMain.window.FeatureStatus[item.Status]}";
                }
                else
                {
                    _toDoText += $", { item.Title } - {ProjectPlannerMain.window.FeatureStatus[0]}";
                }
            }
            
            string _issueText = "";

            foreach (var item in ProjectPlannerMain.window._crudIssueManager.RetrieveProjectIssues(ProjectPlannerMain.window._crudProjectManager))
            {
                if (item.Status > -1)
                {
                    _issueText += $", { item.Title } - { ProjectPlannerMain.window.IssueStatus[item.Status] }";
                }
                else
                {
                    _issueText += $", { item.Title } - { ProjectPlannerMain.window.IssueStatus[0]}";
                }    
            }

            if (_completeText.Length > 0)
            {
                _completeText = _completeText.Remove(0, 2);
                ProjectPlannerMain.window.ProjectCompletedText.Text = _completeText;
            }
            else
            {
                ProjectPlannerMain.window.ProjectCompletedText.Text = "";
            }

            if (_toDoText.Length > 0)
            {
                _toDoText = _toDoText.Remove(0, 2);
                ProjectPlannerMain.window.ProjectToDoText.Text = _toDoText;
            }
            else
            {
                ProjectPlannerMain.window.ProjectToDoText.Text = "";
            }

            if (_issueText.Length > 0)
            {
                _issueText = _issueText.Remove(0, 2);
                ProjectPlannerMain.window.ProjectIssuesText.Text = _issueText;
            }
            else
            {
                ProjectPlannerMain.window.ProjectIssuesText.Text = "";
            }

            ProjectPlannerMain.window.ProjectProgressBar.Value = _calculateProgress.CalculateProjectProgress(ProjectPlannerMain.window._crudProjectManager.SelectedProject);
            ProjectPlannerMain.window.ProgressBarValue.Text = ProjectPlannerMain.window.ProjectProgressBar.Value.ToString() + "%";
        }

        public void PopulateFeatureFields()
        {
            ProjectPlannerMain.window.FeatureIDText.Content = ProjectPlannerMain.window._crudFeatureManager.SelectedFeature.FeatureId;
            ProjectPlannerMain.window.FeatureTitleTextBox.Text = ProjectPlannerMain.window._crudFeatureManager.SelectedFeature.Title;
            ProjectPlannerMain.window.FeatureDescriptionTextBox.Text = ProjectPlannerMain.window._crudFeatureManager.SelectedFeature.Description;
            ProjectPlannerMain.window.FeatureProjectIDText.Content = ProjectPlannerMain.window._crudFeatureManager.SelectedFeature.ProjectId;
            ProjectPlannerMain.window.FeatureStatusComboBox.SelectedIndex = ProjectPlannerMain.window._crudFeatureManager.SelectedFeature.Status;
            ProjectPlannerMain.window.FeaturePriorityTextBox.Text = ProjectPlannerMain.window._crudFeatureManager.SelectedFeature.Priority.ToString();
            ProjectPlannerMain.window.FeatureNotesTextBox.Text = ProjectPlannerMain.window._crudFeatureManager.SelectedFeature.Notes;
        }

        public void PopulateIssueFields()
        {
            ProjectPlannerMain.window.IssueIDText.Content = ProjectPlannerMain.window._crudIssueManager.SelectedIssue.IssueId;
            ProjectPlannerMain.window.IssueTitleTextBox.Text = ProjectPlannerMain.window._crudIssueManager.SelectedIssue.Title;
            ProjectPlannerMain.window.IssueDescriptionTextBox.Text = ProjectPlannerMain.window._crudIssueManager.SelectedIssue.Description;
            ProjectPlannerMain.window.IssueProjectIDText.Content = ProjectPlannerMain.window._crudIssueManager.SelectedIssue.ProjectId;
            ProjectPlannerMain.window.IssueStatusComboBox.SelectedIndex = ProjectPlannerMain.window._crudIssueManager.SelectedIssue.Status;
            ProjectPlannerMain.window.IssuePriorityTextBox.Text = ProjectPlannerMain.window._crudIssueManager.SelectedIssue.Priority.ToString();
            ProjectPlannerMain.window.IssueNotesTextBox.Text = ProjectPlannerMain.window._crudIssueManager.SelectedIssue.Notes;
        }

        public void PopulateNoteFields()
        {
            ProjectPlannerMain.window.NoteIDText.Content = ProjectPlannerMain.window._crudNoteManager.SelectedNote.NoteId;
            ProjectPlannerMain.window.NoteTitleTextBox.Text = ProjectPlannerMain.window._crudNoteManager.SelectedNote.Title;
            ProjectPlannerMain.window.NoteBodyTextBox.Text = ProjectPlannerMain.window._crudNoteManager.SelectedNote.Body;
        }

        public void PopulateFeatureLists()
        {
            foreach (var item in ProjectPlannerMain.window._crudFeatureManager.RetrieveProjectFeatures(ProjectPlannerMain.window._crudProjectManager))
            {
                if (item.Status == 0)
                {
                    _addExtender.CreateExpander(ProjectPlannerMain.window.PlannedFeatures, item, false);
                }
                else if (item.Status == 1)
                {
                    _addExtender.CreateExpander(ProjectPlannerMain.window.InProgressFeatures, item, false);
                }
                else if (item.Status == 2)
                {
                    _addExtender.CreateExpander(ProjectPlannerMain.window.TestingFeatures, item, false);
                }
                else
                {
                    _addExtender.CreateExpander(ProjectPlannerMain.window.CompleteFeatures, item, false);
                }
            }
        }

        public void PopulateIssueLists()
        {
            foreach (var item in ProjectPlannerMain.window._crudIssueManager.RetrieveProjectIssues(ProjectPlannerMain.window._crudProjectManager))
            {
                if (item.Status == 0)
                {
                    _addExtender.CreateExpander(ProjectPlannerMain.window.KnownIssues, item, false);
                }
                else if (item.Status == 1)
                {
                    _addExtender.CreateExpander(ProjectPlannerMain.window.InProgressIssues, item, false);
                }
                else if (item.Status == 2)
                {
                    _addExtender.CreateExpander(ProjectPlannerMain.window.TestingIssues, item, false);
                }
                else if (item.Status == 3)
                {
                    _addExtender.CreateExpander(ProjectPlannerMain.window.ResolvedIssues, item, false);
                }
            }
        }

        public void PopulateNoteList()
        {
            foreach (var item in ProjectPlannerMain.window._crudNoteManager.RetrieveAllNotes())
            {
                _addExtender.CreateExpander(ProjectPlannerMain.window.NoteStackPanel, item, false);
            }
        }
    }
}
