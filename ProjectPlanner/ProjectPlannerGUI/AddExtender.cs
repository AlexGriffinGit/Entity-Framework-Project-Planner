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
            _expander.AddHandler(Expander.ExpandedEvent, new RoutedEventHandler(_expander_Feature_Expanded));

            Style style = this.FindResource("ExpanderStyle") as Style;
            _expander.Style = style;

            StackPanel _basestackPanel = new StackPanel { Name = "BaseStackPanel", Orientation = Orientation.Vertical };

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

            Button _updateFeatureButton = new Button() { Content = "Update", FontSize = 26, HorizontalContentAlignment = (HorizontalAlignment)TextAlignment.Left, Padding = new Thickness(30, 0, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#908F91")), Style = this.FindResource("CRUDButtons") as Style, Width = 225 };
            _updateFeatureButton.Click += ButtonModifyFeature_Click;

            _basestackPanel.Children.Add(_idLabel);
            _basestackPanel.Children.Add(_idText);
            _basestackPanel.Children.Add(_titleLabel);
            _basestackPanel.Children.Add(_titleText);
            _basestackPanel.Children.Add(_descriptionLabel);
            _basestackPanel.Children.Add(_descriptionText);
            _basestackPanel.Children.Add(_projectIDLabel);
            _basestackPanel.Children.Add(_projectIDText);
            _basestackPanel.Children.Add(_statusLabel);
            _basestackPanel.Children.Add(_statusText);
            _basestackPanel.Children.Add(_priorityLabel);
            _basestackPanel.Children.Add(_PriorityText);
            _basestackPanel.Children.Add(_notesLabel);
            _basestackPanel.Children.Add(_notesText);
            _basestackPanel.Children.Add(_updateFeatureButton);

            _expander.Content = _basestackPanel;

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
            _expander.AddHandler(Expander.ExpandedEvent, new RoutedEventHandler(_expander_Issue_Expanded));

            Style style = this.FindResource("ExpanderStyle") as Style;
            _expander.Style = style;

            StackPanel _basestackPanel = new StackPanel { Name = "BaseStackPanel", Orientation = Orientation.Vertical };

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

            Button _updateIssueButton = new Button() { Content = "Update", FontSize = 26, HorizontalContentAlignment = (HorizontalAlignment)TextAlignment.Left, Padding = new Thickness(30, 0, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#908F91")), Style = this.FindResource("CRUDButtons") as Style, Width = 225 };
            _updateIssueButton.Click += ButtonModifyIssue_Click;

            _basestackPanel.Children.Add(_idLabel);
            _basestackPanel.Children.Add(_idText);
            _basestackPanel.Children.Add(_titleLabel);
            _basestackPanel.Children.Add(_titleText);
            _basestackPanel.Children.Add(_descriptionLabel);
            _basestackPanel.Children.Add(_descriptionText);
            _basestackPanel.Children.Add(_projectIDLabel);
            _basestackPanel.Children.Add(_projectIDText);
            _basestackPanel.Children.Add(_statusLabel);
            _basestackPanel.Children.Add(_statusText);
            _basestackPanel.Children.Add(_priorityLabel);
            _basestackPanel.Children.Add(_PriorityText);
            _basestackPanel.Children.Add(_notesLabel);
            _basestackPanel.Children.Add(_notesText);
            _basestackPanel.Children.Add(_updateIssueButton);

            _expander.Content = _basestackPanel;

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
            _expander.AddHandler(Expander.ExpandedEvent, new RoutedEventHandler(_expander_Note_Expanded));

            _expander.FontSize = 32;
            _expander.Margin = Margin = new Thickness(0, 0, 0, 10);

            Style style = this.FindResource("ExpanderStyle") as Style;
            _expander.Style = style;

            StackPanel _basestackPanel = new StackPanel { Name = "BaseStackPanel", Orientation = Orientation.Vertical };

            TextBlock _noteBody = new TextBlock { Text = note.Body, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")), Margin = new Thickness(0, 10, 0, 0), FontSize = 29 };
            Button _updateNoteButton = new Button() { Content = "Update", FontSize = 26, HorizontalContentAlignment = (HorizontalAlignment)TextAlignment.Left, Padding = new Thickness(30, 0, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#908F91")), Style = this.FindResource("CRUDButtons") as Style, Width = 225 };
            _updateNoteButton.Click += ButtonModifyNote_Click;

            _basestackPanel.Children.Add(_noteBody);
            _basestackPanel.Children.Add(_updateNoteButton);

            _expander.Content = _basestackPanel;

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
    }
}
