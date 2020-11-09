using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Text;
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
    public partial class ProjectPlannerMain : Window
    {
        //Create expander for feature lists
        private void CreateExpander(StackPanel panelToAddTo, Feature relatedFeature)
        {
            Expander _expander = new Expander();

            _expander.Header = relatedFeature.Title;
            _expander.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"));
            _expander.Background = new SolidColorBrush(Colors.Transparent);
            _expander.IsExpanded = false;
            _expander.Tag = relatedFeature;

            Style style = this.FindResource("ExpanderStyle") as Style;
            _expander.Style = style;

            _expander.AddHandler(Expander.ExpandedEvent, new RoutedEventHandler(_expander_Feature_Expanded));

            StackPanel _baseStackPanel = new StackPanel { Name = "BaseStackPanel", Orientation = Orientation.Vertical };

            TextBlock _idLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Feature ID" };
            TextBlock _titleLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Feature Title" };
            TextBlock _descriptionLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Feature Description" };
            TextBlock _projectIDLabel = new TextBlock { Text = "Project ID", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };
            TextBlock _statusLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Feature Status" };
            TextBlock _priorityLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Feature Priority" };
            TextBlock _notesLabel = new TextBlock { Text = "Notes", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };

            TextBlock _idText = new TextBlock { Text = relatedFeature.FeatureId.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _titleText = new TextBlock { Text = relatedFeature.Title , TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _descriptionText = new TextBlock { Text = relatedFeature.Description, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _projectIDText = new TextBlock { Text = relatedFeature.ProjectId.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _statusText = new TextBlock() { TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), Text = _featureStatus[relatedFeature.Status] };
            TextBlock _PriorityText = new TextBlock { Text = relatedFeature.Priority.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _notesText = new TextBlock { Text = relatedFeature.Notes, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };

            StackPanel _updateFeatureStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            StackPanel _deleteFeatureStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };

            TextBlock _updateFeatureTextBlock = new TextBlock() { Text = "Update", FontSize = 27, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(-8, 0, 0, 0) };
            TextBlock _deleteFeatureTextBlock = new TextBlock() { Text = "Delete", FontSize = 27, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(-8, 0, 0, 0) };

            Image _updateFeatureIcon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/ProjectPlannerGUI;component/Images/ModifySymbol.png")), Margin = new Thickness(28, 0, 0, 0), Width = 28 };
            Image _deleteFeatureIcon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/ProjectPlannerGUI;component/Images/DeleteSymbol.png")), Margin = new Thickness(38, 0, 0, 0), Width = 28 };

            _updateFeatureStackPanel.Children.Add(_updateFeatureTextBlock);
            _updateFeatureStackPanel.Children.Add(_updateFeatureIcon);

            _deleteFeatureStackPanel.Children.Add(_deleteFeatureTextBlock);
            _deleteFeatureStackPanel.Children.Add(_deleteFeatureIcon);

            Button _updateFeatureButton = new Button() { Content = _updateFeatureStackPanel, HorizontalContentAlignment = (HorizontalAlignment)TextAlignment.Left, Padding = new Thickness(54, 0, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#908F91")), Style = this.FindResource("CRUDButtons") as Style, Width = 235, Margin = new Thickness(0, 8, 0, 0), Height = 35 };
            _updateFeatureButton.Click += ButtonModifyFeature_Click;

            Button _deleteFeatureButton = new Button() { Content = _deleteFeatureStackPanel, HorizontalContentAlignment = (HorizontalAlignment)TextAlignment.Left, Padding = new Thickness(54, 0, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#908F91")), Style = this.FindResource("CRUDButtons") as Style, Width = 235, Margin = new Thickness(0, 8, 0, 0), Height = 35 };
            _deleteFeatureButton.Click += ButtonDeleteFeature_Click;

            //These tags pass the feature that they relate to and set that as the selected feature in the CRUDManager
            _updateFeatureButton.Tag = relatedFeature;
            _deleteFeatureButton.Tag = relatedFeature;

            _baseStackPanel.Children.Add(_idLabel);
            _baseStackPanel.Children.Add(_idText);
            _baseStackPanel.Children.Add(_titleLabel);
            _baseStackPanel.Children.Add(_titleText);
            _baseStackPanel.Children.Add(_descriptionLabel);
            _baseStackPanel.Children.Add(_descriptionText);
            _baseStackPanel.Children.Add(_projectIDLabel);
            _baseStackPanel.Children.Add(_projectIDText);
            _baseStackPanel.Children.Add(_statusLabel);
            _baseStackPanel.Children.Add(_statusText);
            _baseStackPanel.Children.Add(_priorityLabel);
            _baseStackPanel.Children.Add(_PriorityText);
            _baseStackPanel.Children.Add(_notesLabel);
            _baseStackPanel.Children.Add(_notesText);
            _baseStackPanel.Children.Add(_updateFeatureButton);
            _baseStackPanel.Children.Add(_deleteFeatureButton);

            _expander.Content = _baseStackPanel;

            panelToAddTo.Children.Add(_expander);
        }

        //Overloaded to create expander for issue lists
        private void CreateExpander(StackPanel panelToAddTo, Issue relatedIssue)
        {
            Expander _expander = new Expander();

            _expander.Header = relatedIssue.Title;
            _expander.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"));
            _expander.Background = new SolidColorBrush(Colors.Transparent);
            _expander.IsExpanded = false;
            _expander.Tag = relatedIssue;

            Style style = this.FindResource("ExpanderStyle") as Style;
            _expander.Style = style;

            _expander.AddHandler(Expander.ExpandedEvent, new RoutedEventHandler(_expander_Issue_Expanded));

            StackPanel _baseStackPanel = new StackPanel { Name = "BaseStackPanel", Orientation = Orientation.Vertical };

            TextBlock _idLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Issue ID" };
            TextBlock _titleLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Issue Title" };
            TextBlock _descriptionLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Issue Description" };
            TextBlock _projectIDLabel = new TextBlock { Text = "Project ID", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };
            TextBlock _statusLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Issue Status" };
            TextBlock _priorityLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Issue Priority" };
            TextBlock _notesLabel = new TextBlock { Text = "Notes", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };

            TextBlock _idText = new TextBlock { Text = relatedIssue.IssueId.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _titleText = new TextBlock { Text = relatedIssue.Title, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _descriptionText = new TextBlock { Text = relatedIssue.Description, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _projectIDText = new TextBlock { Text = relatedIssue.ProjectId.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _statusText = new TextBlock() { TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), Text = _issueStatus[relatedIssue.Status] };
            TextBlock _PriorityText = new TextBlock { Text = relatedIssue.Priority.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _notesText = new TextBlock { Text = relatedIssue.Notes, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };

            StackPanel _updateIssueStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            StackPanel _deleteIssueStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };

            TextBlock _updateIssueTextBlock = new TextBlock() { Text = "Update", FontSize = 27, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(-8, 0, 0, 0) };
            TextBlock _deleteIssueTextBlock = new TextBlock() { Text = "Delete", FontSize = 27, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(-8, 0, 0, 0) };

            Image _updateIssueIcon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/ProjectPlannerGUI;component/Images/ModifySymbol.png")), Margin = new Thickness(28, 0, 0, 0), Width = 28 };
            Image _deleteIssueIcon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/ProjectPlannerGUI;component/Images/DeleteSymbol.png")), Margin = new Thickness(38, 0, 0, 0), Width = 28 };

            _updateIssueStackPanel.Children.Add(_updateIssueTextBlock);
            _updateIssueStackPanel.Children.Add(_updateIssueIcon);

            _deleteIssueStackPanel.Children.Add(_deleteIssueTextBlock);
            _deleteIssueStackPanel.Children.Add(_deleteIssueIcon);

            Button _updateIssueButton = new Button() { Content = _updateIssueStackPanel, FontSize = 28, HorizontalContentAlignment = (HorizontalAlignment)TextAlignment.Left, Padding = new Thickness(54, 0, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#908F91")), Style = this.FindResource("CRUDButtons") as Style, Width = 235, Margin = new Thickness(0, 8, 0, 0), Height = 35 };
            _updateIssueButton.Click += ButtonModifyIssue_Click;

            Button _deleteIssueButton = new Button() { Content = _deleteIssueStackPanel, FontSize = 28, HorizontalContentAlignment = (HorizontalAlignment)TextAlignment.Left, Padding = new Thickness(54, 0, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#908F91")), Style = this.FindResource("CRUDButtons") as Style, Width = 235, Margin = new Thickness(0, 8, 0, 0), Height = 35 };
            _deleteIssueButton.Click += ButtonDeleteIssue_Click;

            //These tags pass the issue that they relate to and set that as the selected issue in the CRUDManager
            _updateIssueButton.Tag = relatedIssue;
            _deleteIssueButton.Tag = relatedIssue;

            _baseStackPanel.Children.Add(_idLabel);
            _baseStackPanel.Children.Add(_idText);
            _baseStackPanel.Children.Add(_titleLabel);
            _baseStackPanel.Children.Add(_titleText);
            _baseStackPanel.Children.Add(_descriptionLabel);
            _baseStackPanel.Children.Add(_descriptionText);
            _baseStackPanel.Children.Add(_projectIDLabel);
            _baseStackPanel.Children.Add(_projectIDText);
            _baseStackPanel.Children.Add(_statusLabel);
            _baseStackPanel.Children.Add(_statusText);
            _baseStackPanel.Children.Add(_priorityLabel);
            _baseStackPanel.Children.Add(_PriorityText);
            _baseStackPanel.Children.Add(_notesLabel);
            _baseStackPanel.Children.Add(_notesText);
            _baseStackPanel.Children.Add(_updateIssueButton);
            _baseStackPanel.Children.Add(_deleteIssueButton);

            _expander.Content = _baseStackPanel;

            panelToAddTo.Children.Add(_expander);
        }

        //Overloaded to create expander for note lists
        private void CreateExpander(Note note)
        {
            Expander _expander = new Expander();

            _expander.Header = note.Title;

            _expander.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"));
            _expander.Background = new SolidColorBrush(Colors.Transparent);
            _expander.IsExpanded = false;
            _expander.Tag = note;
            _expander.FontSize = 32;
            _expander.Margin = Margin = new Thickness(0, 0, 0, 10);

            Style style = this.FindResource("ExpanderStyle") as Style;
            _expander.Style = style;

            _expander.AddHandler(Expander.ExpandedEvent, new RoutedEventHandler(_expander_Note_Expanded));       

            StackPanel _baseStackPanel = new StackPanel { Name = "BaseStackPanel", Orientation = Orientation.Vertical };

            TextBlock _noteBody = new TextBlock { Text = note.Body, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), Margin = new Thickness(0, 10, 0, 0), FontSize = 29 };

            StackPanel _buttonStackPanel = new StackPanel { Name = "BaseStackPanel", Orientation = Orientation.Horizontal };

            StackPanel _updateNoteStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            StackPanel _deleteNoteStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };

            TextBlock _updateNoteTextBlock = new TextBlock() { Text = "Update", FontSize = 29, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(35, 0, 0, 0) };
            TextBlock _deleteNoteTextBlock = new TextBlock() { Text = "Delete", FontSize = 29, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(35, 0, 0, 0) };

            Image _updateNoteIcon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/ProjectPlannerGUI;component/Images/ModifySymbol.png")), Margin = new Thickness(175, 0, 0, 0), Width = 32 };
            Image _deleteNoteIcon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/ProjectPlannerGUI;component/Images/DeleteSymbol.png")), Margin = new Thickness(185, 0, 0, 0), Width = 32 };

            _updateNoteStackPanel.Children.Add(_updateNoteTextBlock);
            _updateNoteStackPanel.Children.Add(_updateNoteIcon);

            _deleteNoteStackPanel.Children.Add(_deleteNoteTextBlock);
            _deleteNoteStackPanel.Children.Add(_deleteNoteIcon);

            Button _updateNoteButton = new Button() { Content = _updateNoteStackPanel, FontSize = 30, HorizontalContentAlignment = (HorizontalAlignment)TextAlignment.Left, Padding = new Thickness(60, 0, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#908F91")), Style = this.FindResource("CRUDButtons") as Style, Margin = new Thickness(0, 8, 15, 0), Height = 40, Width = 525, HorizontalAlignment = HorizontalAlignment.Left };
            _updateNoteButton.Click += ButtonModifyNote_Click;

            Button _deleteNoteButton = new Button() { Content = _deleteNoteStackPanel, FontSize = 30, HorizontalContentAlignment = (HorizontalAlignment)TextAlignment.Left, Padding = new Thickness(60, 0, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#908F91")), Style = this.FindResource("CRUDButtons") as Style, Margin = new Thickness(15, 8, 0, 0), Height = 40, Width = 525, HorizontalAlignment = HorizontalAlignment.Right };
            _deleteNoteButton.Click += ButtonDeleteNote_Click;

            //These tags pass the note that they relate to and set that as the selected note in the CRUDManager
            _updateNoteButton.Tag = note;
            _deleteNoteButton.Tag = note;

            _buttonStackPanel.Children.Add(_updateNoteButton);
            _buttonStackPanel.Children.Add(_deleteNoteButton);

            _baseStackPanel.Children.Add(_noteBody);
            _baseStackPanel.Children.Add(_buttonStackPanel);

            _expander.Content = _baseStackPanel;

            NoteStackPanel.Children.Add(_expander);
        }

        private void _expander_Feature_Expanded(object sender, RoutedEventArgs e)
        {
            _crudManager.SetSelectedFeature(((Expander)sender).Tag);
        }

        private void _expander_Issue_Expanded(object sender, RoutedEventArgs e)
        {
            _crudManager.SetSelectedIssue(((Expander)sender).Tag);
        }

        private void _expander_Note_Expanded(object sender, RoutedEventArgs e)
        {
            _crudManager.SetSelectedNote(((Expander)sender).Tag);
        }

        private void CreateSearchExpander(Project project)
        {
            Expander _expander = new Expander();

            _expander.Header = "Project: " + project.Title;
            _expander.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"));
            _expander.Background = new SolidColorBrush(Colors.Transparent);
            _expander.IsExpanded = false;
            _expander.FontSize = 32;

            Style style = this.FindResource("ExpanderStyle") as Style;
            _expander.Style = style;

            StackPanel _baseStackPanel = new StackPanel { Name = "BaseStackPanel", Orientation = Orientation.Vertical };

            TextBlock _idLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Project ID" };
            TextBlock _titleLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Project Title" };
            TextBlock _descriptionLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Project Description" };
            TextBlock _statusLabel = new TextBlock { Text = "Project Status", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };
            TextBlock _linkLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Project Link" };

            TextBlock _idText = new TextBlock { Text = project.ProjectId.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _titleText = new TextBlock { Text = project.Title, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _descriptionText = new TextBlock { Text = project.Description, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _statusText = new TextBlock { Text = _projectStatus[project.Status], TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _linkText = new TextBlock() { TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), Text = project.Link };

            _baseStackPanel.Children.Add(_idLabel);
            _baseStackPanel.Children.Add(_idText);
            _baseStackPanel.Children.Add(_titleLabel);
            _baseStackPanel.Children.Add(_titleText);
            _baseStackPanel.Children.Add(_descriptionLabel);
            _baseStackPanel.Children.Add(_descriptionText);
            _baseStackPanel.Children.Add(_statusLabel);
            _baseStackPanel.Children.Add(_statusText);
            _baseStackPanel.Children.Add(_linkLabel);
            _baseStackPanel.Children.Add(_linkText);

            _expander.Content = _baseStackPanel;

            SearchStackPanel.Children.Add(_expander);
        }

        private void CreateSearchExpander(Feature feature)
        {
            Expander _expander = new Expander();

            _expander.Header = "Feature: " + feature.Title;
            _expander.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"));
            _expander.Background = new SolidColorBrush(Colors.Transparent);
            _expander.IsExpanded = false;
            _expander.FontSize = 32;

            Style style = this.FindResource("ExpanderStyle") as Style;
            _expander.Style = style;

            StackPanel _baseStackPanel = new StackPanel { Name = "BaseStackPanel", Orientation = Orientation.Vertical };

            TextBlock _idLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Feature ID" };
            TextBlock _titleLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Feature Title" };
            TextBlock _descriptionLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Feature Description" };
            TextBlock _projectIDLabel = new TextBlock { Text = "Project ID", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };
            TextBlock _statusLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Feature Status" };
            TextBlock _priorityLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Feature Priority" };
            TextBlock _notesLabel = new TextBlock { Text = "Notes", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };

            TextBlock _idText = new TextBlock { Text = feature.FeatureId.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _titleText = new TextBlock { Text = feature.Title, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _descriptionText = new TextBlock { Text = feature.Description, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _projectIDText = new TextBlock { Text = feature.ProjectId.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _statusText = new TextBlock() { TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), Text = _featureStatus[feature.Status] };
            TextBlock _PriorityText = new TextBlock { Text = feature.Priority.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _notesText = new TextBlock { Text = feature.Notes, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };

            _baseStackPanel.Children.Add(_idLabel);
            _baseStackPanel.Children.Add(_idText);
            _baseStackPanel.Children.Add(_titleLabel);
            _baseStackPanel.Children.Add(_titleText);
            _baseStackPanel.Children.Add(_descriptionLabel);
            _baseStackPanel.Children.Add(_descriptionText);
            _baseStackPanel.Children.Add(_projectIDLabel);
            _baseStackPanel.Children.Add(_projectIDText);
            _baseStackPanel.Children.Add(_statusLabel);
            _baseStackPanel.Children.Add(_statusText);
            _baseStackPanel.Children.Add(_priorityLabel);
            _baseStackPanel.Children.Add(_PriorityText);
            _baseStackPanel.Children.Add(_notesLabel);
            _baseStackPanel.Children.Add(_notesText);

            _expander.Content = _baseStackPanel;

            SearchStackPanel.Children.Add(_expander);
        }

        private void CreateSearchExpander(Issue issue)
        {
            Expander _expander = new Expander();

            _expander.Header = "Issue: " + issue.Title;
            _expander.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"));
            _expander.Background = new SolidColorBrush(Colors.Transparent);
            _expander.IsExpanded = false;
            _expander.FontSize = 32;

            Style style = this.FindResource("ExpanderStyle") as Style;
            _expander.Style = style;

            StackPanel _baseStackPanel = new StackPanel { Name = "BaseStackPanel", Orientation = Orientation.Vertical };

            TextBlock _idLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Issue ID" };
            TextBlock _titleLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Issue Title" };
            TextBlock _descriptionLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Issue Description" };
            TextBlock _projectIDLabel = new TextBlock { Text = "Project ID", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };
            TextBlock _statusLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Issue Status" };
            TextBlock _priorityLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")), Text = "Issue Priority" };
            TextBlock _notesLabel = new TextBlock { Text = "Notes", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };

            TextBlock _idText = new TextBlock { Text = issue.IssueId.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _titleText = new TextBlock { Text = issue.Title, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _descriptionText = new TextBlock { Text = issue.Description, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _projectIDText = new TextBlock { Text = issue.ProjectId.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _statusText = new TextBlock() { TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), Text = _issueStatus[issue.Status] };
            TextBlock _PriorityText = new TextBlock { Text = issue.Priority.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _notesText = new TextBlock { Text = issue.Notes, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };

            _baseStackPanel.Children.Add(_idLabel);
            _baseStackPanel.Children.Add(_idText);
            _baseStackPanel.Children.Add(_titleLabel);
            _baseStackPanel.Children.Add(_titleText);
            _baseStackPanel.Children.Add(_descriptionLabel);
            _baseStackPanel.Children.Add(_descriptionText);
            _baseStackPanel.Children.Add(_projectIDLabel);
            _baseStackPanel.Children.Add(_projectIDText);
            _baseStackPanel.Children.Add(_statusLabel);
            _baseStackPanel.Children.Add(_statusText);
            _baseStackPanel.Children.Add(_priorityLabel);
            _baseStackPanel.Children.Add(_PriorityText);
            _baseStackPanel.Children.Add(_notesLabel);
            _baseStackPanel.Children.Add(_notesText);

            _expander.Content = _baseStackPanel;

            SearchStackPanel.Children.Add(_expander);
        }

        private void CreateSearchExpander(Note note)
        {
            Expander _expander = new Expander();

            _expander.Header = "Note: " + note.Title;
            _expander.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"));
            _expander.Background = new SolidColorBrush(Colors.Transparent);
            _expander.IsExpanded = false;
            _expander.FontSize = 32;

            Style style = this.FindResource("ExpanderStyle") as Style;
            _expander.Style = style;

            StackPanel _baseStackPanel = new StackPanel { Name = "BaseStackPanel", Orientation = Orientation.Vertical };

            TextBlock _noteBody = new TextBlock { Text = note.Body, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), Margin = new Thickness(0, 10, 0, 0), FontSize = 29 };

            _baseStackPanel.Children.Add(_noteBody);

            _expander.Content = _baseStackPanel;

            SearchStackPanel.Children.Add(_expander);
        }
    }
}
