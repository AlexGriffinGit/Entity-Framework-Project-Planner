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
            ProjectStatusComboBox.SelectedIndex = _crudManager.SelectedProject.Status;
            ProjectLinkTextBox.Text = _crudManager.SelectedProject.Link;

            string _completeText = "";

            foreach (var item in _crudManager.RetrieveCompleteFeatures())
            {
                _completeText += $", { item.Title }";
            }

            string _toDoText = "";

            foreach (var item in _crudManager.RetrieveToDoFeatures())
            {
                _toDoText += $", { item.Title } - {_featureStatus[item.Status]}";
            }

            string _issueText = "";

            foreach (var item in _crudManager.RetrieveProjectIssues())
            {
                _issueText += $", { item.Title } - { _issueStatus[item.Status] }";
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

            ProjectProgressBar.Value = CalculateProjectProgress();
            ProgressBarValue.Text = CalculateProjectProgress().ToString() + "%";
        }

        private int CalculateProjectProgress()
        {
            int progress = 0;
            int numOfFeatures = _crudManager.RetrieveCompleteFeatures().Count + _crudManager.RetrieveToDoFeatures().Count;
            int featureWorth = 100 / numOfFeatures;

            foreach (var item in _crudManager.RetrieveToDoFeatures())
            {
                if (item.Status == 0)
                {
                    progress += featureWorth / 5;
                }
                else if (item.Status == 1)
                {
                    progress += featureWorth / 2;
                }
                else if (item.Status == 2)
                {
                    progress += (featureWorth / 4) * 3;
                }
            }

            progress += _crudManager.RetrieveCompleteFeatures().Count * featureWorth;

            return progress;
        }

        private void PopulateFeatureLists()
        {
            foreach (var item in _crudManager.RetrieveProjectFeatures())
            {
                if (item.Status == 0)
                {
                    CreateExpander(item.FeatureId, item.Title, item.Description, item.ProjectId, item.Status, item.Priority, item.Notes, PlannedFeatures);
                }
                else if (item.Status == 1)
                {
                    CreateExpander(item.FeatureId, item.Title, item.Description, item.ProjectId, item.Status, item.Priority, item.Notes, InProgressFeatures);
                }
                else if (item.Status == 2)
                {
                    CreateExpander(item.FeatureId, item.Title, item.Description, item.ProjectId, item.Status, item.Priority, item.Notes, TestingFeatures);
                }
                else
                {
                    CreateExpander(item.FeatureId, item.Title, item.Description, item.ProjectId, item.Status, item.Priority, item.Notes, CompleteFeatures);
                }
            }
        }

        private void PopulateIssueLists()
        {
            foreach (var item in _crudManager.RetrieveProjectIssues())
            {
                if (item.Status == 0)
                {
                    CreateExpander(item.IssueId, item.Title, item.Description, item.ProjectId, item.Status, item.Priority, item.Notes, KnownIssues);
                }
                else if (item.Status == 1)
                {
                    CreateExpander(item.IssueId, item.Title, item.Description, item.ProjectId, item.Status, item.Priority, item.Notes, InProgressIssues);
                }
                else if (item.Status == 2)
                {
                    CreateExpander(item.IssueId, item.Title, item.Description, item.ProjectId, item.Status, item.Priority, item.Notes, TestingIssues);
                }
                else
                {
                    CreateExpander(item.IssueId, item.Title, item.Description, item.ProjectId, item.Status, item.Priority, item.Notes, ResolvedIssues);
                }
            }
        }

        private void PopulateNoteList()
        {
            foreach (var item in _crudManager.RetrieveAllNotes())
            {
                CreateExpander(item.Title, item.Body);
            }
        }

        private void ResetFeatureLists()
        {
            PlannedFeatures.Children.Clear();
            InProgressFeatures.Children.Clear();
            TestingFeatures.Children.Clear();
            CompleteFeatures.Children.Clear();
        }

        private void ResetIssueLists()
        {
            KnownIssues.Children.Clear();
            InProgressIssues.Children.Clear();
            TestingIssues.Children.Clear();
            ResolvedIssues.Children.Clear();
        }

        private void ResetNoteList()
        {
            NoteStackPanel.Children.Clear();
        }
    }
}
