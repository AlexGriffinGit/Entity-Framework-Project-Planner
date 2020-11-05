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
        }

        private void ShowProjectFields()
        {
            ProjectScrollView.Visibility = Visibility.Visible;
        }

        private void HideFeatureFields()
        {
            FeatureScrollView.Visibility = Visibility.Hidden;
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
        }

        private void ShowIssueLists()
        {
            IssueKnownView.Visibility = Visibility.Visible;
            IssueInProgressView.Visibility = Visibility.Visible;
            IssueTestingView.Visibility = Visibility.Visible;
            IssueResolvedView.Visibility = Visibility.Visible;
        }

        private void HideAddAndDeleteButtons()
        {
            AddButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
        }

        private void ShowAddAndDeleteButtons()
        {
            AddButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
        }

        private void HideConfirmAndCancelButtons()
        {
            ConfirmButton.Visibility = Visibility.Hidden;
            Cancelbutton.Visibility = Visibility.Hidden;
        }

        private void ShowConfirmAndCancelButtons()
        {
            ConfirmButton.Visibility = Visibility.Visible;
            Cancelbutton.Visibility = Visibility.Visible;
        }
    }
}
