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
using ProjectPlannerBusiness;

namespace ProjectPlannerGUI
{
    public class ShowAndHide
    {
        public void HideProjectFields()
        {
            ProjectPlannerMain.window.ProjectScrollView.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.ProjectScrollView.ScrollToTop();

            ProjectPlannerMain.window.ProjectIDText.Content = String.Empty;
            ProjectPlannerMain.window.ProjectTitleTextBox.Text = String.Empty;
            ProjectPlannerMain.window.ProjectDescriptionTextBox.Text = String.Empty;
            ProjectPlannerMain.window.ProjectStatusComboBox.SelectedIndex = -1;
            ProjectPlannerMain.window.ProjectLinkTextBox.Text = String.Empty;
            ProjectPlannerMain.window.ProjectCompletedText.Text = String.Empty;
            ProjectPlannerMain.window.ProjectToDoText.Text = String.Empty;
            ProjectPlannerMain.window.ProjectIssuesText.Text = String.Empty;
            ProjectPlannerMain.window.ProjectProgressBar.Value = 0;
        }

        public void ShowProjectFields()
        {
            ProjectPlannerMain.window.ProjectScrollView.Visibility = Visibility.Visible;
        }

        public void HideFeatureFields()
        {
            ProjectPlannerMain.window.FeatureScrollView.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.FeatureScrollView.ScrollToTop();

            ProjectPlannerMain.window.FeatureIDText.Content = String.Empty;
            ProjectPlannerMain.window.FeatureTitleTextBox.Text = String.Empty;
            ProjectPlannerMain.window.FeatureDescriptionTextBox.Text = String.Empty;
            ProjectPlannerMain.window.FeatureProjectIDText.Content = String.Empty;
            ProjectPlannerMain.window.FeatureStatusComboBox.SelectedIndex = -1;
            ProjectPlannerMain.window.FeaturePriorityTextBox.Text = String.Empty;
            ProjectPlannerMain.window.FeatureNotesTextBox.Text = String.Empty;
        }

        public void ShowFeatureFields()
        {
            ProjectPlannerMain.window.FeatureScrollView.Visibility = Visibility.Visible;
        }

        public void HideFeatureLists()
        {
            ProjectPlannerMain.window.FeaturePlannedListTitle.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.FeatureInProgressListTitle.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.FeatureTestingListTitle.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.FeatureCompleteListTitle.Visibility = Visibility.Hidden;

            ProjectPlannerMain.window.FeaturePlannedView.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.FeatureInProgressView.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.FeatureTestingView.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.FeatureCompleteView.Visibility = Visibility.Hidden;

            ProjectPlannerMain.window.PlannedFeatures.Children.Clear();
            ProjectPlannerMain.window.InProgressFeatures.Children.Clear();
            ProjectPlannerMain.window.TestingFeatures.Children.Clear();
            ProjectPlannerMain.window.CompleteFeatures.Children.Clear();
        }

        public void ShowFeatureLists()
        {
            ProjectPlannerMain.window.FeaturePlannedListTitle.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.FeatureInProgressListTitle.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.FeatureTestingListTitle.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.FeatureCompleteListTitle.Visibility = Visibility.Visible;

            ProjectPlannerMain.window.FeaturePlannedView.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.FeatureInProgressView.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.FeatureTestingView.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.FeatureCompleteView.Visibility = Visibility.Visible;
        }

        public void HideIssueFields()
        {
            ProjectPlannerMain.window.IssueScrollView.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.IssueScrollView.ScrollToTop();

            ProjectPlannerMain.window.IssueIDText.Content = String.Empty;
            ProjectPlannerMain.window.IssueTitleTextBox.Text = String.Empty;
            ProjectPlannerMain.window.IssueDescriptionTextBox.Text = String.Empty;
            ProjectPlannerMain.window.IssueProjectIDText.Content = String.Empty;
            ProjectPlannerMain.window.IssueStatusComboBox.SelectedIndex = -1;
            ProjectPlannerMain.window.IssuePriorityTextBox.Text = String.Empty;
            ProjectPlannerMain.window.IssueNotesTextBox.Text = String.Empty;
        }

        public void ShowIssueFields()
        {
            ProjectPlannerMain.window.IssueScrollView.Visibility = Visibility.Visible;
        }

        public void HideIssueLists()
        {
            ProjectPlannerMain.window.IssueKnownListTitle.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.IssueInProgressTitle.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.IssueTestingTitle.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.IssueResolvedTitle.Visibility = Visibility.Hidden;

            ProjectPlannerMain.window.IssueKnownView.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.IssueInProgressView.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.IssueTestingView.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.IssueResolvedView.Visibility = Visibility.Hidden;

            ProjectPlannerMain.window.KnownIssues.Children.Clear();
            ProjectPlannerMain.window.InProgressIssues.Children.Clear();
            ProjectPlannerMain.window.TestingIssues.Children.Clear();
            ProjectPlannerMain.window.ResolvedIssues.Children.Clear();
        }

        public void ShowIssueLists()
        {
            ProjectPlannerMain.window.IssueKnownListTitle.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.IssueInProgressTitle.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.IssueTestingTitle.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.IssueResolvedTitle.Visibility = Visibility.Visible;

            ProjectPlannerMain.window.IssueKnownView.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.IssueInProgressView.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.IssueTestingView.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.IssueResolvedView.Visibility = Visibility.Visible;
        }

        public void HideNoteFields()
        {
            ProjectPlannerMain.window.NoteScrollView.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.NoteScrollView.ScrollToTop();

            ProjectPlannerMain.window.NoteIDText.Content = String.Empty;
            ProjectPlannerMain.window.NoteTitleTextBox.Text = String.Empty;
            ProjectPlannerMain.window.NoteBodyTextBox.Text = String.Empty;
        }

        public void ShowNoteFields()
        {
            ProjectPlannerMain.window.NoteScrollView.Visibility = Visibility.Visible;
        }

        public void HideNoteList()
        {
            ProjectPlannerMain.window.NotesView.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.NoteStackPanel.Children.Clear();
        }

        public void ShowNoteList()
        {
            ProjectPlannerMain.window.NotesView.Visibility = Visibility.Visible;
        }

        public void HideCrudButtons()
        {
            ProjectPlannerMain.window.AddButton.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.DeleteButton.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.ConfirmButton.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.Cancelbutton.Visibility = Visibility.Hidden;
        }

        public void HideProjectComboBox()
        {
            ProjectPlannerMain.window.ProjectComboBox.Visibility = Visibility.Hidden;
        }

        public void ShowProjectComboBox()
        {
            ProjectPlannerMain.window.ProjectComboBox.Visibility = Visibility.Visible;
        }

        public void HideProjectSubheadingButtons()
        {
            ProjectPlannerMain.window.ProjectOverviewButton.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.ProjectFeaturesButton.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.ProjectIssuesButton.Visibility = Visibility.Hidden;
        }

        public void ShowProjectSubheadingButtons()
        {
            ProjectPlannerMain.window.ProjectOverviewButton.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.ProjectFeaturesButton.Visibility = Visibility.Visible;
            ProjectPlannerMain.window.ProjectIssuesButton.Visibility = Visibility.Visible;
        }

        public void HideSearch()
        {
            ProjectPlannerMain.window.SearchFields.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.SearchScrollView.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.SearchStackPanel.Children.Clear();

            ProjectPlannerMain.window.SearchProjectsCheckBox.IsChecked = false;
            ProjectPlannerMain.window.SearchFeaturesCheckBox.IsChecked = false;
            ProjectPlannerMain.window.SearchIssuesCheckBox.IsChecked = false;
            ProjectPlannerMain.window.SearchNotesCheckBox.IsChecked = false;

            ProjectPlannerMain.window.SearchTextBox.Text = String.Empty;
        }

        public void HideExport()
        {
            ProjectPlannerMain.window.ExportFields.Visibility = Visibility.Hidden;
            ProjectPlannerMain.window.ExportOutPutStackPanel.Children.Clear();

            ProjectPlannerMain.window.ExportProjectsCheckBox.IsChecked = false;
            ProjectPlannerMain.window.ExportFeaturesCheckBox.IsChecked = false;
            ProjectPlannerMain.window.ExportIssuesCheckBox.IsChecked = false;
            ProjectPlannerMain.window.ExportNotesCheckBox.IsChecked = false;
        }
    }
}
