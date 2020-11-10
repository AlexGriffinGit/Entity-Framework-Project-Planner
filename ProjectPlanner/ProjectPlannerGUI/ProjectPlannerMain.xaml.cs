using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectPlannerBusiness;

namespace ProjectPlannerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProjectPlannerMain : Window
    {
        public CRUDProjectManager _crudProjectManager = new CRUDProjectManager();
        public CRUDFeatureManager _crudFeatureManager = new CRUDFeatureManager();
        public CRUDIssueManager _crudIssueManager = new CRUDIssueManager();
        public CRUDNoteManager _crudNoteManager = new CRUDNoteManager();

        private Searcher _searcher = new Searcher();
        private XMLExporter _xmlExporter = new XMLExporter();
        private JSONExporter _jsonExporter = new JSONExporter();

        private PopulateFields _populator = new PopulateFields();
        private AddExtender _addExtender = new AddExtender();
        private ShowAndHide _showAndHide = new ShowAndHide();

        private bool _projectsSelected = true;
        private bool _notesSelected = false;
        private bool _searchSelected = false;
        private bool _exportSelected = false;

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

        internal static ProjectPlannerMain window;

        public List<string> ProjectStatus { get; } = new List<string>()
        {
            "Planning",
            "In Progress",
            "Testing",
            "Releasing",
            "Complete"
        };

        public List<string> FeatureStatus { get; } = new List<string>()
        {
            "Planning",
            "In Development",
            "Testing",
            "Complete"
        };

        public List<string> IssueStatus { get; } = new List<string>()
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

            window = this;

            InitializeComponent();

            _populator.PopulateComboBox();
            ButtonSelected(ProjectHeaderButton);
            ProjectHeaderButton.FontSize = 55;
            _showAndHide.HideProjectSubheadingButtons();

            _showAndHide.HideCrudButtons();
            AddButton.Visibility = Visibility.Visible;

            ProjectStatusComboBox.ItemsSource = ProjectStatus;
            FeatureStatusComboBox.ItemsSource = FeatureStatus;
            IssueStatusComboBox.ItemsSource = IssueStatus;
        }

        private void ProjectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _hasChangedFromComboBox = true;

            _showAndHide.ShowProjectSubheadingButtons();

            _showAndHide.HideFeatureFields();
            _showAndHide.HideFeatureLists();
            _showAndHide.HideIssueFields();
            _showAndHide.HideIssueLists();
            _showAndHide.HideNoteFields();
            _showAndHide.HideNoteList();

            _showAndHide.HideCrudButtons();
            AddButton.Visibility = Visibility.Visible;

            if (ProjectComboBox.SelectedItem != null)
            {
                _crudProjectManager.SetSelectedProject(ProjectComboBox.SelectedItem);
                _populator.PopulateProjectFields();
                _showAndHide.ShowProjectFields();

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
                _projectsSelected = true; _notesSelected = false; _searchSelected = false; _exportSelected = false;

                ButtonSelected(ProjectHeaderButton); 
                ButtonDeselected(NotesHeaderButton);
                ButtonDeselected(SearchHeaderButton);
                ButtonDeselected(ExportHeaderButton);

                ProjectHeaderButton.FontSize = 55; NotesHeaderButton.FontSize = 45; SearchHeaderButton.FontSize = 45; ExportHeaderButton.FontSize = 45;

                _showAndHide.ShowProjectComboBox();

                _showAndHide.HideNoteList();
                _showAndHide.HideNoteFields();

                _showAndHide.HideSearch();

                _showAndHide.HideExport();

                _showAndHide.HideCrudButtons();
                AddButton.Visibility = Visibility.Visible;

                _currentView = "p";

                if (ProjectComboBox.SelectedItem != null)
                {
                    _showAndHide.ShowProjectSubheadingButtons();

                    _showAndHide.ShowProjectFields();
                    _populator.PopulateProjectFields();

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
                _notesSelected = true; _projectsSelected = false; _searchSelected = false; _exportSelected = false;

                ButtonSelected(NotesHeaderButton);
                ButtonDeselected(ProjectHeaderButton);
                ButtonDeselected(SearchHeaderButton);
                ButtonDeselected(ExportHeaderButton);

                NotesHeaderButton.FontSize = 55; ProjectHeaderButton.FontSize = 45; SearchHeaderButton.FontSize = 45; ExportHeaderButton.FontSize = 45;

                _currentView = "n";

                _showAndHide.HideProjectComboBox();
                _showAndHide.HideProjectSubheadingButtons();
                _showAndHide.HideProjectFields();

                _showAndHide.HideFeatureFields();
                _showAndHide.HideFeatureLists();

                _showAndHide.HideIssueFields();
                _showAndHide.HideIssueLists();

                _showAndHide.HideSearch();

                _showAndHide.HideExport();

                _showAndHide.HideCrudButtons();
                AddButton.Visibility = Visibility.Visible;

                _populator.PopulateNoteList();
                _showAndHide.ShowNoteList();
            }
        }

        private void SearchHeaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (_searchSelected == false)
            {
                _searchSelected = true; _projectsSelected = false; _notesSelected = false; _exportSelected = false;

                ButtonSelected(SearchHeaderButton); 
                ButtonDeselected(ProjectHeaderButton);
                ButtonDeselected(NotesHeaderButton);
                ButtonDeselected(ExportHeaderButton);

                SearchHeaderButton.FontSize = 55; ProjectHeaderButton.FontSize = 45; NotesHeaderButton.FontSize = 45; ExportHeaderButton.FontSize = 45;

                _showAndHide.HideProjectComboBox();
                _showAndHide.HideProjectSubheadingButtons();
                _showAndHide.HideProjectFields();

                _showAndHide.HideFeatureFields();
                _showAndHide.HideFeatureLists();

                _showAndHide.HideIssueFields();
                _showAndHide.HideIssueLists();

                _showAndHide.HideNoteFields();
                _showAndHide.HideNoteList();

                _showAndHide.HideCrudButtons();

                _showAndHide.HideExport();

                SearchFields.Visibility = Visibility.Visible;
            }
        }


        private void ExportHeaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (_exportSelected == false)
            {
                _exportSelected = true; _projectsSelected = false; _notesSelected = false; _searchSelected = false;

                ButtonSelected(ExportHeaderButton);
                ButtonDeselected(ProjectHeaderButton);
                ButtonDeselected(NotesHeaderButton);
                ButtonDeselected(SearchHeaderButton);

                ExportHeaderButton.FontSize = 55; SearchHeaderButton.FontSize = 45; ProjectHeaderButton.FontSize = 45; NotesHeaderButton.FontSize = 45;

                _showAndHide.HideProjectComboBox();
                _showAndHide.HideProjectSubheadingButtons();
                _showAndHide.HideProjectFields();

                _showAndHide.HideFeatureFields();
                _showAndHide.HideFeatureLists();

                _showAndHide.HideIssueFields();
                _showAndHide.HideIssueLists();

                _showAndHide.HideNoteFields();
                _showAndHide.HideNoteList();

                _showAndHide.HideSearch();

                _showAndHide.HideCrudButtons();

                SearchFields.Visibility = Visibility.Hidden;

                ExportFields.Visibility = Visibility.Visible;
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

                _showAndHide.ShowProjectFields();
                _populator.PopulateProjectFields();
                _showAndHide.HideFeatureFields();
                _showAndHide.HideFeatureLists();
                _showAndHide.HideIssueFields();
                _showAndHide.HideIssueLists();

                _showAndHide.HideCrudButtons();
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

                _showAndHide.ShowFeatureLists();

                _showAndHide.HideFeatureFields();
                _showAndHide.HideProjectFields();
                _showAndHide.HideIssueFields();
                _showAndHide.HideIssueLists();

                _populator.PopulateFeatureLists();

                _showAndHide.HideCrudButtons();
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

                _showAndHide.ShowIssueLists();

                _showAndHide.HideIssueFields();
                _showAndHide.HideProjectFields();
                _showAndHide.HideFeatureFields();
                _showAndHide.HideFeatureLists();

                _populator.PopulateIssueLists();

                _showAndHide.HideCrudButtons();
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
                    _showAndHide.HideProjectFields();
                    _showAndHide.ShowProjectFields();

                    _showAndHide.HideCrudButtons();
                    ConfirmButton.Visibility = Visibility.Visible;
                    Cancelbutton.Visibility = Visibility.Visible;
                    break;
                case "f":
                    _showAndHide.ShowFeatureFields();
                    FeatureProjectIDText.Content = _crudProjectManager.SelectedProject.ProjectId;

                    _showAndHide.HideCrudButtons();
                    ConfirmButton.Visibility = Visibility.Visible;
                    Cancelbutton.Visibility = Visibility.Visible;
                    break;
                case "i":
                    _showAndHide.ShowIssueFields();
                    IssueProjectIDText.Content = _crudProjectManager.SelectedProject.ProjectId;

                    _showAndHide.HideCrudButtons();
                    ConfirmButton.Visibility = Visibility.Visible;
                    Cancelbutton.Visibility = Visibility.Visible;
                    break;
                case "n":
                    _showAndHide.ShowNoteFields();

                    _showAndHide.HideCrudButtons();
                    ConfirmButton.Visibility = Visibility.Visible;
                    Cancelbutton.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }

            _showAndHide.HideFeatureLists();
            _showAndHide.HideIssueLists();
            _showAndHide.HideNoteList();
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
                            _crudProjectManager.CreateNewProject(ProjectTitleTextBox.Text, ProjectDescriptionTextBox.Text, ProjectStatusComboBox.SelectedIndex, ProjectLinkTextBox.Text);

                            _showAndHide.HideProjectFields();
                            _populator.PopulateComboBox();

                            ProjectComboBox.SelectedIndex = _crudProjectManager.RetrieveIndexOfNewProject();
                            _showAndHide.ShowProjectFields();

                            _showAndHide.HideCrudButtons();
                            AddButton.Visibility = Visibility.Visible;
                            DeleteButton.Visibility = Visibility.Visible;

                            _isAdding = false;
                            _updateView = "";
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
                                _crudFeatureManager.CreateNewFeature(FeatureTitleTextBox.Text, FeatureDescriptionTextBox.Text, FeatureStatusComboBox.SelectedIndex, _featurePriority, FeatureNotesTextBox.Text, ProjectPlannerMain.window._crudProjectManager);

                                _showAndHide.HideFeatureFields();
                                _populator.PopulateFeatureLists();
                                _showAndHide.ShowFeatureLists();

                                _showAndHide.HideCrudButtons();
                                AddButton.Visibility = Visibility.Visible;

                                _isAdding = false;
                                _updateView = "";
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
                                _crudIssueManager.CreateNewIssue(IssueTitleTextBox.Text, IssueDescriptionTextBox.Text, IssueStatusComboBox.SelectedIndex, _issuePriority, IssueNotesTextBox.Text, ProjectPlannerMain.window._crudProjectManager);

                                _showAndHide.HideIssueFields();
                                _populator.PopulateIssueLists();
                                _showAndHide.ShowIssueLists();

                                _showAndHide.HideCrudButtons();
                                AddButton.Visibility = Visibility.Visible;

                                _isAdding = false;
                                _updateView = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Priority has to be a valid number, you entered { IssuePriorityTextBox.Text }");
                        }
                        break;
                    case "n":
                        _crudNoteManager.CreateNewNote(NoteTitleTextBox.Text, NoteBodyTextBox.Text);

                        _showAndHide.HideNoteFields();
                        _populator.PopulateNoteList();
                        _showAndHide.ShowNoteList();

                        _showAndHide.HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;

                        _isAdding = false;
                        _updateView = "";
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
                            _crudProjectManager.UpdateProject(ProjectTitleTextBox.Text, ProjectDescriptionTextBox.Text, ProjectStatusComboBox.SelectedIndex, ProjectLinkTextBox.Text);

                            int _chosenIndex = ProjectComboBox.SelectedIndex;

                            _showAndHide.HideProjectFields();
                            _populator.PopulateComboBox();

                            ProjectComboBox.SelectedIndex = _chosenIndex;

                            _showAndHide.HideCrudButtons();
                            AddButton.Visibility = Visibility.Visible;
                            DeleteButton.Visibility = Visibility.Visible;

                            _isAdding = false;
                            _updateView = "";
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
                                _crudFeatureManager.UpdateFeature(FeatureTitleTextBox.Text, FeatureDescriptionTextBox.Text, FeatureStatusComboBox.SelectedIndex, _featurePriority, FeatureNotesTextBox.Text);

                                _showAndHide.HideFeatureFields();
                                _showAndHide.ShowFeatureLists();
                                _populator.PopulateFeatureLists();

                                _showAndHide.HideCrudButtons();
                                AddButton.Visibility = Visibility.Visible;

                                _isAdding = false;
                                _updateView = "";
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
                                _crudIssueManager.UpdateIssue(IssueTitleTextBox.Text, IssueDescriptionTextBox.Text, IssueStatusComboBox.SelectedIndex, _issuePriority, IssueNotesTextBox.Text);

                                _showAndHide.HideIssueFields();
                                _populator.PopulateIssueLists();
                                _showAndHide.ShowIssueLists();

                                _showAndHide.HideCrudButtons();
                                AddButton.Visibility = Visibility.Visible;

                                _isAdding = false;
                                _updateView = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Priority has to be a valid number, you entered { IssuePriorityTextBox.Text }");
                        }
                        break;
                    case "n":
                        _crudNoteManager.UpdateNote(NoteTitleTextBox.Text, NoteBodyTextBox.Text);

                        _showAndHide.HideNoteFields();
                        _populator.PopulateNoteList();
                        _showAndHide.ShowNoteList();

                        _showAndHide.HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;

                        _isAdding = false;
                        _updateView = "";
                        break;
                    default:
                        break;
                }
            }
        }

        private void Cancelbutton_Click(object sender, RoutedEventArgs e)
        {
            if (_isAdding == true)
            {
                switch (_currentView)
                {
                    case "p":
                        _showAndHide.HideProjectFields();

                        ProjectComboBox.SelectedItem = null;

                        _showAndHide.HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;                      
                        break;
                    case "f":
                        _showAndHide.HideFeatureFields();
                        _populator.PopulateFeatureLists();
                        _showAndHide.ShowFeatureLists();

                        _showAndHide.HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        break;
                    case "i":
                        _showAndHide.HideIssueFields();
                        _populator.PopulateIssueLists();
                        _showAndHide.ShowIssueLists();

                        _showAndHide.HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        break;
                    case "n":
                        _showAndHide.HideNoteFields();
                        _populator.PopulateNoteList();
                        _showAndHide.ShowNoteList();

                        _showAndHide.HideCrudButtons();
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
                        _showAndHide.HideProjectFields();
                        _populator.PopulateProjectFields();
                        _showAndHide.ShowProjectFields();

                        _showAndHide.HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        DeleteButton.Visibility = Visibility.Visible;
                        break;
                    case "f":
                        _showAndHide.HideFeatureFields();
                        _populator.PopulateFeatureLists();
                        _showAndHide.ShowFeatureLists();

                        _showAndHide.HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        break;
                    case "i":
                        _showAndHide.HideIssueFields();
                        _populator.PopulateIssueLists();
                        _showAndHide.ShowIssueLists();

                        _showAndHide.HideCrudButtons();
                        AddButton.Visibility = Visibility.Visible;
                        break;
                    case "n":
                        _showAndHide.HideNoteFields();
                        _populator.PopulateNoteList();
                        _showAndHide.ShowNoteList();

                        _showAndHide.HideCrudButtons();
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
                _crudProjectManager.DeleteProject();

                _showAndHide.HideProjectFields();
                _populator.PopulateComboBox();

                _showAndHide.HideCrudButtons();
                AddButton.Visibility = Visibility.Visible;
            }
        }

        public void ButtonModifyFeature_Click(object sender, RoutedEventArgs e)
        {
            _crudFeatureManager.SetSelectedFeature(((Button)sender).Tag);

            _updateView = "f";

            _isAdding = false;

            _showAndHide.HideFeatureLists();
            _showAndHide.HideIssueLists();
            _showAndHide.HideNoteList();

            _showAndHide.ShowFeatureFields();
            _populator.PopulateFeatureFields();

            _showAndHide.HideCrudButtons();
            ConfirmButton.Visibility = Visibility.Visible;
            Cancelbutton.Visibility = Visibility.Visible;
        }

        public void ButtonDeleteFeature_Click(object sender, RoutedEventArgs e)
        {
            _crudFeatureManager.SetSelectedFeature(((Button)sender).Tag);

            _crudFeatureManager.DeleteFeature();

            _showAndHide.HideFeatureLists();
            _populator.PopulateFeatureLists();
            _showAndHide.ShowFeatureLists();
        }

        public void ButtonModifyIssue_Click(object sender, RoutedEventArgs e)
        {
            _crudIssueManager.SetSelectedIssue(((Button)sender).Tag);

            _updateView = "i";

            _isAdding = false;

            _showAndHide.HideFeatureLists();
            _showAndHide.HideIssueLists();
            _showAndHide.HideNoteList();

            _showAndHide.ShowIssueFields();
            _populator.PopulateIssueFields();

            _showAndHide.HideCrudButtons();
            ConfirmButton.Visibility = Visibility.Visible;
            Cancelbutton.Visibility = Visibility.Visible;
        }

        public void ButtonDeleteIssue_Click(object sender, RoutedEventArgs e)
        {
            _crudIssueManager.SetSelectedIssue(((Button)sender).Tag);

            _crudIssueManager.DeleteIssue();

            _showAndHide.HideIssueLists();
            _populator.PopulateIssueLists();
            _showAndHide.ShowIssueLists();
        }

        public void ButtonModifyNote_Click(object sender, RoutedEventArgs e)
        {
            _crudNoteManager.SetSelectedNote(((Button)sender).Tag);

            _updateView = "n";

            _isAdding = false;

            _showAndHide.HideFeatureLists();
            _showAndHide.HideIssueLists();
            _showAndHide.HideNoteList();

            _showAndHide.ShowNoteFields();
            _populator.PopulateNoteFields();

            _showAndHide.HideCrudButtons();
            ConfirmButton.Visibility = Visibility.Visible;
            Cancelbutton.Visibility = Visibility.Visible;
        }

        public void ButtonDeleteNote_Click(object sender, RoutedEventArgs e)
        {
            _crudNoteManager.SetSelectedNote(((Button)sender).Tag);

            _crudNoteManager.DeleteNote();

            _showAndHide.HideNoteList();
            _populator.PopulateNoteList();
            _showAndHide.ShowNoteList();
        }

        private void ProjectDetailsChanged()
        {
            if (_hasChangedFromComboBox == false)
            {
                _showAndHide.HideCrudButtons();
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchStackPanel.Children.Clear();
            SearchScrollView.Visibility = Visibility.Visible;

            if (SearchProjectsCheckBox.IsChecked == true)
            {
                foreach (var item in _searcher.SearchProjects(SearchTextBox.Text))
                {
                    _addExtender.CreateExpander(item);
                }
            }

            if (SearchFeaturesCheckBox.IsChecked == true)
            {
                foreach (var item in _searcher.SearchFeatures(SearchTextBox.Text))
                {
                    _addExtender.CreateExpander(SearchStackPanel, item, true);
                }
            }

            if (SearchIssuesCheckBox.IsChecked == true)
            {
                foreach (var item in _searcher.SearchIssues(SearchTextBox.Text))
                {
                    _addExtender.CreateExpander(SearchStackPanel, item, true);
                }
            }

            if (SearchNotesCheckBox.IsChecked == true)
            {
                foreach (var item in _searcher.SearchNotes(SearchTextBox.Text))
                {
                    _addExtender.CreateExpander(SearchStackPanel, item, true);
                }
            }
        }

        private void XMLButton_Click(object sender, RoutedEventArgs e)
        {
            ExportOutPutStackPanel.Children.Clear();
            _xmlExporter.InitSerialisation();

            TextBlock _projectOutput;
            TextBlock _featuretOutput;
            TextBlock _issueOutput;
            TextBlock _noteOutput;

            if (ExportProjectsCheckBox.IsChecked == true)
            {
                string _outputMessage = _xmlExporter.SerialiseProjects();

                _projectOutput = new TextBlock() { Text = _outputMessage, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), FontSize = 25, Margin = new Thickness(45, 20, 45, 0), TextAlignment = TextAlignment.Center , TextWrapping = TextWrapping.Wrap };

                ExportOutPutStackPanel.Children.Add(_projectOutput);
            }
            
            if (ExportFeaturesCheckBox.IsChecked == true)
            {
                string _outputMessage = _xmlExporter.SerialiseFeatures();

                _featuretOutput = new TextBlock() { Text = _outputMessage, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), FontSize = 25, Margin = new Thickness(45, 20, 45, 0), TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap };

                ExportOutPutStackPanel.Children.Add(_featuretOutput);
            }

            if (ExportIssuesCheckBox.IsChecked == true)
            {
                string _outputMessage = _xmlExporter.SerialiseIssues();

                _issueOutput = new TextBlock() { Text = _outputMessage, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), FontSize = 25, Margin = new Thickness(45, 20, 45, 0), TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap };

                ExportOutPutStackPanel.Children.Add(_issueOutput);
            }

            if (ExportNotesCheckBox.IsChecked == true)
            {
                string _outputMessage = _xmlExporter.SerialiseNotes();

                _noteOutput = new TextBlock() { Text = _outputMessage, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), FontSize = 25, Margin = new Thickness(45, 20, 45, 0), TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap };

                ExportOutPutStackPanel.Children.Add(_noteOutput);
            }
        }

        private void JSONButton_Click(object sender, RoutedEventArgs e)
        {
            ExportOutPutStackPanel.Children.Clear();
            _jsonExporter.InitSerialisation();

            TextBlock _projectOutput;
            TextBlock _featuretOutput;
            TextBlock _issueOutput;
            TextBlock _noteOutput;

            if (ExportProjectsCheckBox.IsChecked == true)
            {
                string _outputMessage = _jsonExporter.SerialiseProjects();

                _projectOutput = new TextBlock() { Text = _outputMessage, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), FontSize = 25, Margin = new Thickness(45, 20, 45, 0), TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap };

                ExportOutPutStackPanel.Children.Add(_projectOutput);
            }

            if (ExportFeaturesCheckBox.IsChecked == true)
            {
                string _outputMessage = _jsonExporter.SerialiseFeatures();

                _featuretOutput = new TextBlock() { Text = _outputMessage, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), FontSize = 25, Margin = new Thickness(45, 20, 45, 0), TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap };

                ExportOutPutStackPanel.Children.Add(_featuretOutput);
            }

            if (ExportIssuesCheckBox.IsChecked == true)
            {
                string _outputMessage = _jsonExporter.SerialiseIssues();

                _issueOutput = new TextBlock() { Text = _outputMessage, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), FontSize = 25, Margin = new Thickness(45, 20, 45, 0), TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap };

                ExportOutPutStackPanel.Children.Add(_issueOutput);
            }

            if (ExportNotesCheckBox.IsChecked == true)
            {
                string _outputMessage = _jsonExporter.SerialiseNotes();

                _noteOutput = new TextBlock() { Text = _outputMessage, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), FontSize = 25, Margin = new Thickness(45, 20, 45, 0), TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap };

                ExportOutPutStackPanel.Children.Add(_noteOutput);
            }
        }
    }
}