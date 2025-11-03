using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SensorApp.Data;
using SensorApp.UI;
using SensorApp.Utils;

namespace SensorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Dashboard.Instance;
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            Dashboard.Instance.LoadNewDataset();
        }

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            Dashboard.Instance.PreviousDataset();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            Dashboard.Instance.NextDataset();
        }
        private void RefreshPropertiesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Dashboard.Instance.ActiveDataset != null)
            {
                Dashboard.Instance.UpdateDataGridView();
            }
        }

        //private void SearchBtn_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}