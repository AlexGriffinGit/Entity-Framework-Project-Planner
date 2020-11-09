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
    public partial class ProjectPlannerMain : Window
    {
        private void HideProjectFields()
        {
            ProjectScrollView.Visibility = Visibility.Hidden;
            ProjectScrollView.ScrollToTop();

            ProjectIDText.Content = String.Empty;
            ProjectTitleTextBox.Text = String.Empty;
            ProjectDescriptionTextBox.Text = String.Empty;
            ProjectStatusComboBox.SelectedIndex = -1;
            ProjectLinkTextBox.Text = String.Empty;
            ProjectCompletedText.Text = String.Empty;
            ProjectToDoText.Text = String.Empty;
            ProjectIssuesText.Text = String.Empty;
            ProjectProgressBar.Value = 0;
        }

        private void ShowProjectFields()
        {
            ProjectScrollView.Visibility = Visibility.Visible;
        }

        private void HideFeatureFields()
        {
            FeatureScrollView.Visibility = Visibility.Hidden;
            FeatureScrollView.ScrollToTop();

            FeatureIDText.Content = String.Empty;
            FeatureTitleTextBox.Text = String.Empty;
            FeatureDescriptionTextBox.Text = String.Empty;
            FeatureProjectIDText.Content = String.Empty;
            FeatureStatusComboBox.SelectedIndex = -1;
            FeaturePriorityTextBox.Text = String.Empty;
            FeatureNotesTextBox.Text = String.Empty;
        }

        private void ShowFeatureFields()
        {
            FeatureScrollView.Visibility = Visibility.Visible;
        }

        private void HideFeatureLists()
        {
            FeaturePlannedView.Visibility = Visibility.Hidden;
            FeatureInProgressView.Visibility = Visibility.Hidden;
            FeatureTestingView.Visibility = Visibility.Hidden;
            FeatureCompleteView.Visibility = Visibility.Hidden;

            PlannedFeatures.Children.Clear();
            InProgressFeatures.Children.Clear();
            TestingFeatures.Children.Clear();
            CompleteFeatures.Children.Clear();
        }

        private void ShowFeatureLists()
        {
            FeaturePlannedView.Visibility = Visibility.Visible;
            FeatureInProgressView.Visibility = Visibility.Visible;
            FeatureTestingView.Visibility = Visibility.Visible;
            FeatureCompleteView.Visibility = Visibility.Visible;
        }

        private void HideIssueFields()
        {
            IssueScrollView.Visibility = Visibility.Hidden;
            IssueScrollView.ScrollToTop();

            IssueIDText.Content = String.Empty;
            IssueTitleTextBox.Text = String.Empty;
            IssueDescriptionTextBox.Text = String.Empty;
            IssueProjectIDText.Content = String.Empty;
            IssueStatusComboBox.SelectedIndex = -1;
            IssuePriorityTextBox.Text = String.Empty;
            IssueNotesTextBox.Text = String.Empty;
        }

        private void ShowIssueFields()
        {
            IssueScrollView.Visibility = Visibility.Visible;
        }

        private void HideIssueLists()
        {
            IssueKnownView.Visibility = Visibility.Hidden;
            IssueInProgressView.Visibility = Visibility.Hidden;
            IssueTestingView.Visibility = Visibility.Hidden;
            IssueResolvedView.Visibility = Visibility.Hidden;

            KnownIssues.Children.Clear();
            InProgressIssues.Children.Clear();
            TestingIssues.Children.Clear();
            ResolvedIssues.Children.Clear();
        }

        private void ShowIssueLists()
        {
            IssueKnownView.Visibility = Visibility.Visible;
            IssueInProgressView.Visibility = Visibility.Visible;
            IssueTestingView.Visibility = Visibility.Visible;
            IssueResolvedView.Visibility = Visibility.Visible;
        }

        private void HideNoteFields()
        {
            NoteScrollView.Visibility = Visibility.Hidden;
            NoteScrollView.ScrollToTop();

            NoteIDText.Content = String.Empty;
            NoteTitleTextBox.Text = String.Empty;
            NoteBodyTextBox.Text = String.Empty;
        }

        private void ShowNoteFields()
        {
            NoteScrollView.Visibility = Visibility.Visible;
        }

        private void HideNoteList()
        {
            NotesView.Visibility = Visibility.Hidden;
            NoteStackPanel.Children.Clear();
        }

        private void ShowNoteList()
        {
            NotesView.Visibility = Visibility.Visible;
        }

        private void HideCrudButtons()
        {
            AddButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
            ConfirmButton.Visibility = Visibility.Hidden;
            Cancelbutton.Visibility = Visibility.Hidden;
        }

        private void HideProjectComboBox()
        {
            ProjectComboBox.Visibility = Visibility.Hidden;
        }

        private void ShowProjectComboBox()
        {
            ProjectComboBox.Visibility = Visibility.Visible;
        }

        private void HideProjectSubheadingButtons()
        {
            ProjectOverviewButton.Visibility = Visibility.Hidden;
            ProjectFeaturesButton.Visibility = Visibility.Hidden;
            ProjectIssuesButton.Visibility = Visibility.Hidden;
        }

        private void ShowProjectSubheadingButtons()
        {
            ProjectOverviewButton.Visibility = Visibility.Visible;
            ProjectFeaturesButton.Visibility = Visibility.Visible;
            ProjectIssuesButton.Visibility = Visibility.Visible;
        }

        private void HideSearch()
        {
            SearchFields.Visibility = Visibility.Hidden;
            SearchScrollView.Visibility = Visibility.Hidden;
            SearchStackPanel.Children.Clear();

            SearchProjectsCheckBox.IsChecked = false;
            SearchFeaturesCheckBox.IsChecked = false;
            SearchIssuesCheckBox.IsChecked = false;
            SearchNotesCheckBox.IsChecked = false;

            SearchTextBox.Text = String.Empty;
        }
    }
}
