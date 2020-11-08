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
    public partial class ProjectPlannerMain : Window
    {
        private void PopulateComboBox()
        {
            ProjectComboBox.ItemsSource = _crudManager.RetrieveAllProjects();
        }

        private void PopulateProjectFields()
        {
            ProjectIDText.Content = _crudManager.SelectedProject.ProjectId;
            ProjectTitleTextBox.Text = _crudManager.SelectedProject.Title;
            ProjectDescriptionTextBox.Text = _crudManager.SelectedProject.Description;

            if (_crudManager.SelectedProject.Status == -1)
            {
                ProjectStatusComboBox.SelectedIndex = 0;
            }
            else
            {
                ProjectStatusComboBox.SelectedIndex = _crudManager.SelectedProject.Status;
            }
            
            ProjectLinkTextBox.Text = _crudManager.SelectedProject.Link;

            string _completeText = "";

            if (_crudManager.RetrieveCompleteFeatures().Count > 0)
            {
                foreach (var item in _crudManager.RetrieveCompleteFeatures())
                {
                    _completeText += $", { item.Title }";
                }
            }

            string _toDoText = "";

            foreach (var item in _crudManager.RetrieveToDoFeatures())
            {
                if (item.Status > -1)
                {
                    _toDoText += $", { item.Title } - {_featureStatus[item.Status]}";
                }
                else
                {
                    _toDoText += $", { item.Title } - {_featureStatus[0]}";
                }
            }
            
            string _issueText = "";

            foreach (var item in _crudManager.RetrieveProjectIssues())
            {
                if (item.Status > -1)
                {
                    _issueText += $", { item.Title } - { _issueStatus[item.Status] }";
                }
                else
                {
                    _issueText += $", { item.Title } - { _issueStatus[0]}";
                }    
            }

            if (_completeText.Length > 0)
            {
                _completeText = _completeText.Remove(0, 2);
                ProjectCompletedText.Text = _completeText;
            }
            else
            {
                ProjectCompletedText.Text = "";
            }

            if (_toDoText.Length > 0)
            {
                _toDoText = _toDoText.Remove(0, 2);
                ProjectToDoText.Text = _toDoText;
            }
            else
            {
                ProjectToDoText.Text = "";
            }

            if (_issueText.Length > 0)
            {
                _issueText = _issueText.Remove(0, 2);
                ProjectIssuesText.Text = _issueText;
            }
            else
            {
                ProjectIssuesText.Text = "";
            }

            ProjectProgressBar.Value = _crudManager.CalculateProjectProgress();
            ProgressBarValue.Text = ProjectProgressBar.Value.ToString() + "%";
        }

        private void PopulateFeatureFields()
        {
            FeatureIDText.Content = _crudManager.SelectedFeature.FeatureId;
            FeatureTitleTextBox.Text = _crudManager.SelectedFeature.Title;
            FeatureDescriptionTextBox.Text = _crudManager.SelectedFeature.Description;
            FeatureProjectIDText.Content = _crudManager.SelectedFeature.ProjectId;
            FeatureStatusComboBox.SelectedIndex = _crudManager.SelectedFeature.Status;
            FeaturePriorityTextBox.Text = _crudManager.SelectedFeature.Priority.ToString();
            FeatureNotesTextBox.Text = _crudManager.SelectedFeature.Notes;
        }

        private void PopulateIssueFields()
        {
            IssueIDText.Content = _crudManager.SelectedIssue.IssueId;
            IssueTitleTextBox.Text = _crudManager.SelectedIssue.Title;
            IssueDescriptionTextBox.Text = _crudManager.SelectedIssue.Description;
            IssueProjectIDText.Content = _crudManager.SelectedIssue.ProjectId;
            IssueStatusComboBox.SelectedIndex = _crudManager.SelectedIssue.Status;
            IssuePriorityTextBox.Text = _crudManager.SelectedIssue.Priority.ToString();
            IssueNotesTextBox.Text = _crudManager.SelectedIssue.Notes;
        }

        private void PopulateNoteFields()
        {
            NoteIDText.Content = _crudManager.SelectedNote.NoteId;
            NoteTitleTextBox.Text = _crudManager.SelectedNote.Title;
            NoteBodyTextBox.Text = _crudManager.SelectedNote.Body;
        }

        private void PopulateFeatureLists()
        {
            foreach (var item in _crudManager.RetrieveProjectFeatures())
            {
                if (item.Status == 0)
                {
                    CreateExpander(PlannedFeatures, item);
                }
                else if (item.Status == 1)
                {
                    CreateExpander(InProgressFeatures, item);
                }
                else if (item.Status == 2)
                {
                    CreateExpander(TestingFeatures, item);
                }
                else
                {
                    CreateExpander(CompleteFeatures, item);
                }
            }
        }

        private void PopulateIssueLists()
        {
            foreach (var item in _crudManager.RetrieveProjectIssues())
            {
                if (item.Status == 0)
                {
                    CreateExpander(KnownIssues, item);
                }
                else if (item.Status == 1)
                {
                    CreateExpander(InProgressIssues, item);
                }
                else if (item.Status == 2)
                {
                    CreateExpander(TestingIssues, item);
                }
                else
                {
                    CreateExpander(ResolvedIssues, item);
                }
            }
        }

        private void PopulateNoteList()
        {
            foreach (var item in _crudManager.RetrieveAllNotes())
            {
                CreateExpander(item);
            }
        }
    }
}
