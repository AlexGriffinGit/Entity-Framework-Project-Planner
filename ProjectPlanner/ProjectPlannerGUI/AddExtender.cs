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
        private void CreateExpander(int id, string title, string description, int projectID, int status, int priority, string notes, StackPanel panelToAddTo)
        {
            Expander _expander = new Expander();

            _expander.Header = title;
            _expander.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"));
            _expander.Background = new SolidColorBrush(Colors.Transparent);
            _expander.IsExpanded = false;

            Style style = this.FindResource("ExpanderStyle") as Style;
            _expander.Style = style;

            StackPanel _basestackPanel = new StackPanel { Name = "BaseStackPanel", Orientation = Orientation.Vertical };

            TextBlock _idLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };
            TextBlock _titleLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };
            TextBlock _descriptionLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };
            TextBlock _projectIDLabel = new TextBlock { Text = "Project ID", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };
            TextBlock _statusLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };
            TextBlock _priorityLabel = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };
            TextBlock _notesLabel = new TextBlock { Text = "Notes", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 10, 0, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF")) };

            TextBlock _idText = new TextBlock { Text = id.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _titleText = new TextBlock { Text = title, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _descriptionText = new TextBlock { Text = description, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _projectIDText = new TextBlock { Text = projectID.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _statusText = new TextBlock() { TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _PriorityText = new TextBlock { Text = priority.ToString(), TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };
            TextBlock _notesText = new TextBlock { Text = notes, TextWrapping = TextWrapping.Wrap, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2D1D3")) };

            if (_currentView == "f")
            {
                _idLabel.Text = "Feature ID";
                _titleLabel.Text = "Feature Title";
                _descriptionLabel.Text = "Feature Description";
                _statusLabel.Text = "Feature Status"; 
                _statusText.Text = _featureStatus[status]; 
                _priorityLabel.Text = "Feature Priority"; 
            }
            else
            {
                _idLabel.Text = "Issue ID";
                _titleLabel.Text = "Issue Title";
                _descriptionLabel.Text = "Issue Description";
                _statusLabel.Text = "Issue Status";
                _statusText.Text = _issueStatus[status];
                _priorityLabel.Text = "Issue Priority";
            }

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

            _expander.Content = _basestackPanel;

            panelToAddTo.Children.Add(_expander);
        }
    }
}
