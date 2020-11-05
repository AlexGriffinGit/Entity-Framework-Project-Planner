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

        public ProjectPlannerMain()
        {
            InitializeComponent();
            PopulateComboBox();
            ButtonSelected(ProjectHeaderButton);
        }

        private void PopulateComboBox()
        {
            ProjectComboBox.ItemsSource = _crudManager.RetrieveAllProjects();
        }

        private void PopulateProjectFields()
        {
            ProjectIDLabel.Content = $"Project ID: { _crudManager.SelectedProject.ProjectId }";
        }

        private void ProjectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _crudManager.SetSelectedProject(ProjectComboBox.SelectedItem);
        }

        private void ProjectHeaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (_projectsSelected == false)
            {
                _projectsSelected = true;
                _notesSelected = false;

                ButtonSelected(ProjectHeaderButton); 
                ButtonDeselected(NotesHeaderButton);

                ProjectHeaderButton.FontSize = 55;
                NotesHeaderButton.FontSize = 45;
            }
        }

        private void NotesHeaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (_notesSelected == false)
            {
                _notesSelected = true;
                _projectsSelected = false;

                ButtonSelected(NotesHeaderButton);
                ButtonDeselected(ProjectHeaderButton);

                NotesHeaderButton.FontSize = 55;
                ProjectHeaderButton.FontSize = 45; 
            }
        }

        private void ProjectOverviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (_overviewSelected == false)
            {
                _overviewSelected = true;
                _featuresSelected = false;
                _issuesSelected = false;

                ButtonSelected(ProjectOverviewButton);
                ButtonDeselected(ProjectFeaturesButton);
                ButtonDeselected(ProjectIssuesButton);

                ProjectOverviewButton.FontSize = 45;
                ProjectFeaturesButton.FontSize = 37;
                ProjectIssuesButton.FontSize = 37;
            }
        }

        private void ProjectFeaturesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_featuresSelected == false)
            {
                _featuresSelected = true;
                _overviewSelected = false;
                _issuesSelected = false;

                ButtonSelected(ProjectFeaturesButton);
                ButtonDeselected(ProjectOverviewButton); 
                ButtonDeselected(ProjectIssuesButton);

                ProjectFeaturesButton.FontSize = 45;
                ProjectOverviewButton.FontSize = 37;
                ProjectIssuesButton.FontSize = 37;
            }
        }

        private void ProjectIssuesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_issuesSelected == false)
            {
                _issuesSelected = true;
                _overviewSelected = false;
                _featuresSelected = false;

                ButtonSelected(ProjectIssuesButton); 
                ButtonDeselected(ProjectOverviewButton);
                ButtonDeselected(ProjectFeaturesButton);

                ProjectIssuesButton.FontSize = 45;
                ProjectOverviewButton.FontSize = 37;
                ProjectFeaturesButton.FontSize = 37;
            }
        }

        private void ButtonSelected(Button button)
        {
            button.FontWeight = FontWeights.Bold;
            button.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"));
        }

        private void ButtonDeselected(Button button)
        {
            button.FontWeight = FontWeights.Normal;
            button.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3"));
        }

        
    }
}
