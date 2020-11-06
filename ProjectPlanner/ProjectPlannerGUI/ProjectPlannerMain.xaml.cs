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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProjectPlannerMain : Window
    {
        private CRUDManager _crudManager = new CRUDManager();

        private bool _projectsSelected = true;
        private bool _notesSelected = false;

        private bool _overviewSelected = false;
        private bool _featuresSelected = false;
        private bool _issuesSelected = false;

        private bool _isAdding = false;

        private string _currentView = "p";
        // p denotes the program is in project view
        // f denotes the program is in feature view
        // i denotes the program is in issue view
        // n denotes the program is in notes view

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

        public ProjectPlannerMain()
        {
            InitializeComponent();
            PopulateComboBox();
            ButtonSelected(ProjectHeaderButton);

            ProjectStatusComboBox.ItemsSource = _projectStatus;
            FeatureStatusComboBox.ItemsSource = _featureStatus;
            IssueStatusComboBox.ItemsSource = _issueStatus;
        }

        private void ProjectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _crudManager.SetSelectedProject(ProjectComboBox.SelectedItem);

            DeleteButton.Visibility = Visibility.Visible;

            HideFeatureFields();
            HideFeatureLists();
            HideIssueFields();
            HideIssueLists();
            HideNoteFields();
            HideNoteList();

            PopulateProjectFields();
            ShowProjectFields();

            ButtonSelected(ProjectOverviewButton);
            ButtonDeselected(ProjectFeaturesButton);
            ButtonDeselected(ProjectIssuesButton);

            ProjectOverviewButton.FontSize = 45; ProjectFeaturesButton.FontSize = 37; ProjectIssuesButton.FontSize = 37;
        }

        private void ProjectHeaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (_projectsSelected == false)
            {
                _projectsSelected = true; _notesSelected = false;

                ButtonSelected(ProjectHeaderButton); 
                ButtonDeselected(NotesHeaderButton);

                ProjectHeaderButton.FontSize = 55; NotesHeaderButton.FontSize = 45;

                ShowProjectComboBox();
                ShowProjectSubheadingButtons();

                HideNoteList();
                HideNoteFields();
                ResetNoteList();

                _currentView = "p";

                if (ProjectComboBox.SelectedItem != null)
                {
                    ShowProjectFields();
                    PopulateProjectFields();

                    ButtonSelected(ProjectOverviewButton);
                    ButtonDeselected(ProjectFeaturesButton);
                    ButtonDeselected(ProjectIssuesButton);

                    ProjectOverviewButton.FontSize = 45; ProjectFeaturesButton.FontSize = 37; ProjectIssuesButton.FontSize = 37;
                }
            }
        }

        private void NotesHeaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (_notesSelected == false)
            {
                _notesSelected = true; _projectsSelected = false;

                ButtonSelected(NotesHeaderButton);
                ButtonDeselected(ProjectHeaderButton);

                NotesHeaderButton.FontSize = 55; ProjectHeaderButton.FontSize = 45;

                _currentView = "n";

                ResetFeatureLists();
                ResetIssueLists();

                HideProjectComboBox();
                HideProjectSubheadingButtons();
                HideProjectFields();

                HideFeatureFields();
                HideFeatureLists();

                HideIssueFields();
                HideIssueLists();

                PopulateNoteList();

                ShowNoteList();
            }
        }

        private void ProjectOverviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (_overviewSelected == false)
            {
                _overviewSelected = true; _featuresSelected = false; _issuesSelected = false;

                ButtonSelected(ProjectOverviewButton);
                ButtonDeselected(ProjectFeaturesButton);
                ButtonDeselected(ProjectIssuesButton);

                ProjectOverviewButton.FontSize = 45; ProjectFeaturesButton.FontSize = 37; ProjectIssuesButton.FontSize = 37;

                ShowProjectFields();
                HideFeatureFields();
                HideFeatureLists();
                HideIssueFields();
                HideIssueLists();
                
                _currentView = "p";

                ResetFeatureLists();
                ResetIssueLists();
            }
        }

        private void ProjectFeaturesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_featuresSelected == false && ProjectComboBox.SelectedItem != null)
            {
                _featuresSelected = true; _overviewSelected = false; _issuesSelected = false;

                ButtonSelected(ProjectFeaturesButton);
                ButtonDeselected(ProjectOverviewButton); 
                ButtonDeselected(ProjectIssuesButton);

                ProjectFeaturesButton.FontSize = 45; ProjectOverviewButton.FontSize = 37; ProjectIssuesButton.FontSize = 37;

                ShowFeatureLists();

                HideFeatureFields();
                HideProjectFields();
                HideIssueFields();
                HideIssueLists();

                PopulateFeatureLists();
                ResetIssueLists();

                _currentView = "f";
            }
        }

        private void ProjectIssuesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_issuesSelected == false && ProjectComboBox.SelectedItem != null)
            {
                _issuesSelected = true; _overviewSelected = false; _featuresSelected = false;

                ButtonSelected(ProjectIssuesButton); 
                ButtonDeselected(ProjectOverviewButton);
                ButtonDeselected(ProjectFeaturesButton);

                ProjectIssuesButton.FontSize = 45; ProjectOverviewButton.FontSize = 37; ProjectFeaturesButton.FontSize = 37;

                ShowIssueLists();

                HideIssueFields();
                HideProjectFields();
                HideFeatureFields();
                HideFeatureLists();

                ResetFeatureLists();
                PopulateIssueLists();

                _currentView = "i";
            }
        }

        private void ButtonSelected(Button button)
        {
            button.FontWeight = FontWeights.Bold;
            button.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"));
        }

        private void ButtonDeselected(Button button)
        {
            button.FontWeight = FontWeights.Normal;
            button.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3"));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _isAdding = true;
            switch (_currentView)
            {
                case "p":
                    ShowProjectFields();
                    HideAddAndDeleteButtons();
                    ShowConfirmAndCancelButtons();
                    break;
                case "f":
                    ShowFeatureFields();
                    HideAddAndDeleteButtons();
                    ShowConfirmAndCancelButtons();
                    break;
                case "i":
                    ShowIssueFields();
                    HideAddAndDeleteButtons();
                    ShowConfirmAndCancelButtons();
                    break;
                case "n":
                    ShowNoteFields();
                    HideAddAndDeleteButtons();
                    ShowConfirmAndCancelButtons();
                    break;
                default:
                    break;
            }

            HideFeatureLists();
            HideIssueLists();
            HideNoteList();

            ResetFeatureLists();
            ResetIssueLists();
            ResetNoteList();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isAdding == true)
            {
                switch (_currentView)
                {
                    case "p":
                        _crudManager.CreateNewProject(ProjectTitleTextBox.Text, ProjectDescriptionTextBox.Text, ProjectStatusComboBox.SelectedIndex, ProjectLinkTextBox.Text);
                        _isAdding = false;

                        PopulateComboBox();
                        HideProjectFields();
                        break;
                    case "f":
                        if (Int32.TryParse(FeaturePriorityTextBox.Text, out int _featurePriority) == true)
                        {
                            _crudManager.CreateNewFeature(FeatureTitleTextBox.Text, FeatureDescriptionTextBox.Text, FeatureStatusComboBox.SelectedIndex, _featurePriority, FeatureNotesTextBox.Text);
                            _isAdding = false;

                            HideFeatureFields();
                            PopulateFeatureLists();
                            ShowFeatureLists();
                        }
                        else
                        {
                            MessageBox.Show($"Priority has to be a valid number, you entered { FeaturePriorityTextBox.Text }");
                        }
                        break;
                    case "i":
                        if (Int32.TryParse(IssuePriorityTextBox.Text, out int _issuePriority) == true)
                        {
                            _crudManager.CreateNewIssue(IssueTitleTextBox.Text, IssueDescriptionTextBox.Text, IssueStatusComboBox.SelectedIndex, _issuePriority, IssueNotesTextBox.Text);
                            _isAdding = false;

                            HideIssueFields();
                            PopulateIssueLists();
                            ShowIssueLists();
                        }
                        else
                        {
                            MessageBox.Show($"Priority has to be a valid number, you entered { IssuePriorityTextBox.Text }");
                        }
                        break;
                    case "n":
                        _crudManager.CreateNewNote(NoteTitleTextBox.Text, NoteBodyTextBox.Text);
                        _isAdding = false;

                        HideNoteFields();
                        PopulateNoteList();
                        ShowNoteList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //update
            }

            ResetFeatureLists();
            ResetIssueLists();

            ShowAddAndDeleteButtons();

            HideConfirmAndCancelButtons();
        }

        private void Cancelbutton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}