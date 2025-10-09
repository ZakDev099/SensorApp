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

namespace SensorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Dashboard mainDashboard = new();
            //double[][] sampleData = new double[][]
            //{
            //    new double[] { 1, 2, 3, 7, 8, 9, 11, 12, 11, 11, 15, 10, 1},
            //    new double[] { 15, 20, 25, 70, 30, 46 },
            //    new double[] { 12, 100, 120, 150 }
            //}
            //;
            //Dataset dataset = new Dataset("MyDataset", sampleData);
            //mainDashboard.SetActiveDataset(dataset);
            //mainDashboard.UpdateDataGridDisplay();

            //DataContext = mainDashboard;

            InitializeComponent();
        }
    }
}