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
using ProjectPlannerModel;

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
        private bool _hasChangedFromComboBox = false;

        private string _currentView = "p";
        private string _updateView = "";
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
            //Useful for finding the Uri String of an image or object
            //string _uriString = TestImage.Source.ToString();

            InitializeComponent();

            PopulateComboBox();
            ButtonSelected(ProjectHeaderButton);
            HideProjectSubheadingButtons();

            HideCrudButtons();
            AddButton.Visibility = Visibility.Visible;

            ProjectStatusComboBox.ItemsSource = _projectStatus;
            FeatureStatusComboBox.ItemsSource = _featureStatus;
            IssueStatusComboBox.ItemsSource = _issueStatus;
        }

        private void ProjectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _hasChangedFromComboBox = true;

            ShowProjectSubheadingButtons();

            HideFeatureFields();
            HideFeatureLists();
            HideIssueFields();
            HideIssueLists();
            HideNoteFields();
            HideNoteList();

            HideCrudButtons();
            AddButton.Visibility = Visibility.Visible;

            if (ProjectComboBox.SelectedItem != null)
            {
                _crudManager.SetSelectedProject(ProjectComboBox.SelectedItem);
                PopulateProjectFields();
                ShowProjectFields();

                DeleteButton.Visibility = Visibility.Visible;
            }

            ButtonSelected(ProjectOverviewButton);
            ButtonDeselected(ProjectFeaturesButton);
            ButtonDeselected(ProjectIssuesButton);

            ProjectOverviewButton.FontSize = 45; ProjectFeaturesButton.FontSize = 37; ProjectIssuesButton.FontSize = 37;

            _hasChangedFromComboBox = false;
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
                
                HideNoteList();
                HideNoteFields();

                HideCrudButtons();
                AddButton.Visibility = Visibility.Visible;

                _currentView = "p";

                if (ProjectComboBox.SelectedItem != null)
                {
                    ShowProjectSubheadingButtons();

                    ShowProjectFields();
                    PopulateProjectFields();

                    ButtonSelected(ProjectOverviewButton);
                    ButtonDeselected(ProjectFeaturesButton);
                    ButtonDeselected(ProjectIssuesButton);

                    ProjectOverviewButton.FontSize = 45; ProjectFeaturesButton.FontSize = 37; ProjectIssuesButton.FontSize = 37;

                    DeleteButton.Visibility = Visibility.Visible;
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

                HideProjectComboBox();
                HideProjectSubheadingButtons();
                HideProjectFields();

                HideFeatureFields();
                HideFeatureLists();

                HideIssueFields();
                HideIssueLists();

                HideCrudButtons();
                AddButton.Visibility = Visibility.Visible;

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
                PopulateProjectFields();
                HideFeatureFields();
                HideFeatureLists();
                HideIssueFields();
                HideIssueLists();

                HideCrudButtons();
                AddButton.Visibility = Visibility.Visible;

                if (ProjectComboBox.SelectedItem != null)
                {
                    DeleteButton.Visibility = Visibility.Visible;
                }

                _currentView = "p";
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

                HideCrudButtons();
                AddButton.Visibility = Visibility.Visible;

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

                PopulateIssueLists();

                HideCrudButtons();
                AddButton.Visibility = Visibility.Visible;

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
                    HideProjectFields();
                    ShowProjectFields();

                    HideCrudButtons();
                    ConfirmButton.Visibility = Visibility.Visible;
                    Cancelbutton.Visibility = Visibility.Visible;
                    break;
                case "f":
                    ShowFeatureFields();
                    FeatureProjectIDText.Content = _crudManager.SelectedProject.ProjectId;

                    HideCrudButtons();
                    ConfirmButton.Visibility = Visibility.Visible;
                    Cancelbutton.Visibility = Visibility.Visible;
                    break;
                case "i":
                    ShowIssueFields();
                    IssueProjectIDText.Content = _crudManager.SelectedProject.ProjectId;

                    HideCrudButtons();
                    ConfirmButton.Visibility = Visibility.Visible;
                    Cancelbutton.Visibility = Visibility.Visible;
                    break;
                case "n":
                    ShowNoteFields();

                    HideCrudButtons();
                    ConfirmButton.Visibility = Visibility.Visible;
                    Cancelbutton.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }

            HideFeatureLists();
            HideIssueLists();
            HideNoteList();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isAdding == true)
            {
                switch (_currentView)
                {
                    case "p":
                        if (ProjectStatusComboBox.SelectedIndex == -1)
                        {
                            MessageBox.Show("Please enter a status for this project");
                        }
                        else
                        {
                            _crudManager.CreateNewProject(ProjectTitleTextBox.Text, ProjectDescriptionTextBox.Text, ProjectStatusComboBox.SelectedIndex, ProjectLinkTextBox.Text);
                            
                            HideProjectFields();
                            PopulateComboBox();

                            ProjectComboBox.SelectedIndex = _crudManager.RetrieveIndexOfNewProject();
                            ShowProjectFields();

                            HideCrudButtons();
                            AddButton.Visibility = Visibility.Visible;
                            DeleteButton.Visibility = Visibility.Visible;
                        }
                        break;
                    case "f":
                        if (Int32.TryParse(FeaturePriorityTextBox.Text, out int _featurePriority) == true)
                        {
                            if (FeatureStatusComboBox.SelectedIndex == -1)
                            {
                                MessageBox.Show("Please enter a status for this feature");
                            }
                            else
                            {
                                _crudManager.CreateNewFeature(FeatureTitleTextBox.Text, FeatureDescriptionTextBox.Text, FeatureStatusComboBox.SelectedIndex, _featurePriority, FeatureNotesTextBox.Text);

                                HideFeatureFields();
                                PopulateFeatureLists();
                                ShowFeatureLists();

                                HideCrudButtons();
                                AddButton.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Priority has to be a valid number, you entered { FeaturePriorityTextBox.Text }");
                        }
                        break;
                    case "i":
                        if (Int32.TryParse(IssuePriorityTextBox.Text, out int _issuePriority) == true)
                        {
                            if (IssueStatusComboBox.SelectedIndex == -1)
                            {
                                MessageBox.Show("Please enter a status for this feature");
                            }
                            else
                            {
                                _crudManager.CreateNewIssue(IssueTitleTextBox.Text, IssueDescriptionTextBox.Text, IssueStatusComboBox.SelectedIndex, _issuePriority, IssueNotesTextBox.Text);

                                HideIssueFields();
                                PopulateIssueLists();
                                ShowIssueLists();

                                HideCrudButtons();
                                AddButton.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Priority has to be a valid number, you entered { IssuePriorityTextBox.Text }");
                        }
                        break;
                    case "n":
                        _crudManager.CreateNewNote(NoteTitleTextBox.Text, NoteBodyTextBox.Text);

                        HideNoteFields();
                        PopulateNoteList();
                        ShowNoteList();

                        HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (_updateView)
                {
                    case "p":

                        if (ProjectStatusComboBox.SelectedIndex == -1)
                        {
                            MessageBox.Show("Please enter a status for this project");
                        }
                        else
                        {
                            _crudManager.UpdateProject(ProjectTitleTextBox.Text, ProjectDescriptionTextBox.Text, ProjectStatusComboBox.SelectedIndex, ProjectLinkTextBox.Text);

                            int _chosenIndex = ProjectComboBox.SelectedIndex;

                            HideProjectFields();
                            PopulateComboBox();

                            ProjectComboBox.SelectedIndex = _chosenIndex;

                            HideCrudButtons();
                            AddButton.Visibility = Visibility.Visible;
                            DeleteButton.Visibility = Visibility.Visible;
                        }
                        break;
                    case "f":
                        if (Int32.TryParse(FeaturePriorityTextBox.Text, out int _featurePriority) == true)
                        {
                            if (FeatureStatusComboBox.SelectedIndex == -1)
                            {
                                MessageBox.Show("Please enter a status for this feature");
                            }
                            else
                            {
                                _crudManager.UpdateFeature(FeatureTitleTextBox.Text, FeatureDescriptionTextBox.Text, FeatureStatusComboBox.SelectedIndex, _featurePriority, FeatureNotesTextBox.Text);

                                HideFeatureFields();
                                ShowFeatureLists();
                                PopulateFeatureLists();

                                HideCrudButtons();
                                AddButton.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Priority has to be a valid number, you entered { FeaturePriorityTextBox.Text }");
                        }
                        break;
                    case "i":
                        if (Int32.TryParse(IssuePriorityTextBox.Text, out int _issuePriority) == true)
                        {
                            if (IssueStatusComboBox.SelectedIndex == -1)
                            {
                                MessageBox.Show("Please enter a status for this issue");
                            }
                            else
                            {
                                _crudManager.UpdateIssue(IssueTitleTextBox.Text, IssueDescriptionTextBox.Text, IssueStatusComboBox.SelectedIndex, _issuePriority, IssueNotesTextBox.Text);

                                HideIssueFields();
                                PopulateIssueLists();
                                ShowIssueLists();

                                HideCrudButtons();
                                AddButton.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Priority has to be a valid number, you entered { IssuePriorityTextBox.Text }");
                        }
                        break;
                    case "n":
                        _crudManager.UpdateNote(NoteTitleTextBox.Text, NoteBodyTextBox.Text);

                        HideNoteFields();
                        PopulateNoteList();
                        ShowNoteList();

                        HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
            }

            _isAdding = false;
            _updateView = "";
        }

        private void Cancelbutton_Click(object sender, RoutedEventArgs e)
        {
            if (_isAdding == true)
            {
                switch (_currentView)
                {
                    case "p":
                        HideProjectFields();

                        ProjectComboBox.SelectedItem = null;

                        HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;                      
                        break;
                    case "f":
                        HideFeatureFields();
                        PopulateFeatureLists();
                        ShowFeatureLists();

                        HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        break;
                    case "i":
                        HideIssueFields();
                        PopulateIssueLists();
                        ShowIssueLists();

                        HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        break;
                    case "n":
                        HideNoteFields();
                        PopulateNoteList();
                        ShowNoteList();

                        HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (_updateView)
                {
                    case "p":
                        HideProjectFields();
                        PopulateProjectFields();
                        ShowProjectFields();

                        HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        DeleteButton.Visibility = Visibility.Visible;
                        break;
                    case "f":
                        HideFeatureFields();
                        PopulateFeatureLists();
                        ShowFeatureLists();

                        HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        break;
                    case "i":
                        HideIssueFields();
                        PopulateIssueLists();
                        ShowIssueLists();

                        HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        break;
                    case "n":
                        HideNoteFields();
                        PopulateNoteList();
                        ShowNoteList();

                        HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
            }

            _isAdding = false;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentView == "p" && ProjectComboBox.SelectedItem != null)
            {
                _crudManager.DeleteProject();

                HideProjectFields();
                PopulateComboBox();

                HideCrudButtons();
                AddButton.Visibility = Visibility.Visible;
            }
        }

        private void ButtonModifyFeature_Click(object sender, RoutedEventArgs e)
        {
            _crudManager.SetSelectedFeature(((Button)sender).Tag);

            _updateView = "f";

            _isAdding = false;

            HideFeatureLists();
            HideIssueLists();
            HideNoteList();

            ShowFeatureFields();
            PopulateFeatureFields();

            HideCrudButtons();
            ConfirmButton.Visibility = Visibility.Visible;
            Cancelbutton.Visibility = Visibility.Visible;
        }

        private void ButtonDeleteFeature_Click(object sender, RoutedEventArgs e)
        {
            _crudManager.SetSelectedFeature(((Button)sender).Tag);

            _crudManager.DeleteFeature();

            HideFeatureLists();
            PopulateFeatureLists();
            ShowFeatureLists();
        }

        private void ButtonModifyIssue_Click(object sender, RoutedEventArgs e)
        {
            _crudManager.SetSelectedIssue(((Button)sender).Tag);

            _updateView = "i";

            _isAdding = false;

            HideFeatureLists();
            HideIssueLists();
            HideNoteList();

            ShowIssueFields();
            PopulateIssueFields();

            HideCrudButtons();
            ConfirmButton.Visibility = Visibility.Visible;
            Cancelbutton.Visibility = Visibility.Visible;
        }

        private void ButtonDeleteIssue_Click(object sender, RoutedEventArgs e)
        {
            _crudManager.SetSelectedIssue(((Button)sender).Tag);

            _crudManager.DeleteIssue();

            HideIssueLists();
            PopulateIssueLists();
            ShowIssueLists();
        }

        private void ButtonModifyNote_Click(object sender, RoutedEventArgs e)
        {
            _crudManager.SetSelectedNote(((Button)sender).Tag);

            _updateView = "n";

            _isAdding = false;

            HideFeatureLists();
            HideIssueLists();
            HideNoteList();

            ShowNoteFields();
            PopulateNoteFields();

            HideCrudButtons();
            ConfirmButton.Visibility = Visibility.Visible;
            Cancelbutton.Visibility = Visibility.Visible;
        }

        private void ButtonDeleteNote_Click(object sender, RoutedEventArgs e)
        {
            _crudManager.SetSelectedNote(((Button)sender).Tag);

            _crudManager.DeleteNote();

            HideNoteList();
            PopulateNoteList();
            ShowNoteList();
        }

        private void ProjectDetailsChanged()
        {
            if (_hasChangedFromComboBox == false)
            {
                HideCrudButtons();
                ConfirmButton.Visibility = Visibility.Visible;
                Cancelbutton.Visibility = Visibility.Visible;

                _updateView = "p";
            }
        }

        private void ProjectTitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProjectDetailsChanged();
        }

        private void ProjectDescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProjectDetailsChanged();
        }

        private void ProjectStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProjectDetailsChanged();
        }

        private void ProjectLinkTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProjectDetailsChanged();
        }
    }
}