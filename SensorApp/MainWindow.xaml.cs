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
        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            Dashboard.Instance.LoadNewDataset();
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Dashboard.Instance;
            Dashboard.Instance.ActiveDataset = new Dataset("TEST", [[1, 2, 3], [12, 13, 15]]);
        }
    }
}