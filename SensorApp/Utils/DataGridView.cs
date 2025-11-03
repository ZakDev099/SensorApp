using SensorApp.Data;
using SensorApp.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SensorApp.Utils
{
    /// <summary>
    /// Handles the GUI display of the active dataset, creates WPF controls and determines foreground/background
    /// colour associated with the user-given target value, min and max bounds.
    /// </summary>
    /// <param name="columns">Determines the horizontal cell count for the grid display</param>
    /// <param name="rows">Determines the vertical cell count for the grid display</param>
    public class DataGridView(int columns, int rows)
    {
        public int Columns => columns;
        public int Rows => rows;

        // Clears the displayed datagrid completely and rebuilds it with updated information
        public void UpdateDataGrid(Dataset? dataset, MainWindow mainWindow, Application application)
        {
            var brushes = LoadBrushes(application);
            int counter = 0;

            mainWindow.DataGrid_UniformGrid.Children.Clear();
            mainWindow.DataGrid_UniformGrid.Rows = rows;
            mainWindow.DataGrid_UniformGrid.Columns = columns;

            // Iterates through the dataset's data, creating datagrid children for each value
            if (dataset != null)
            {
                var bounds = (dataset.UpperBound, dataset.LowerBound);

                foreach (double[] row in dataset.Data)
                {
                    foreach (double column in row)
                    {
                        SolidColorBrush foregroundBrush = CellValueBrushConverter(column, brushes, bounds);

                        var newCell = BuildCustomDataGridCell(brushes["background"], foregroundBrush, column);

                        mainWindow.DataGrid_UniformGrid.Children.Add(newCell);
                        counter++;
                    }
                }
            }

            // Iterates through target values, highlighting the background of each corresponding cell
            if (dataset != null && dataset.TargetValueLocations != null)
            {
                foreach (int targetValues in dataset.TargetValueLocations)
                {
                    if (mainWindow.DataGrid_UniformGrid.Children[targetValues] is Border border &&
                    border.Child is Grid grid)
                    {
                        grid.Background = brushes["highlightedValue"];
                    }
                }
            }

            // Fills remaining cells in the grid with empty datagrid children
            while (counter < rows * columns)
            {
                SolidColorBrush foregroundBrush = Brushes.Black;
                var newCell = BuildCustomDataGridCell(brushes["background"], foregroundBrush);

                mainWindow.DataGrid_UniformGrid.Children.Add(newCell);
                counter++;
            }
        }

        public static Border BuildCustomDataGridCell(SolidColorBrush backgroundBrush, SolidColorBrush foregroundBrush, double? value = null)
        {
            TextBlock textBlock = new()
            {
                Style = (Style)Application.Current.Resources["DataGrid.CellTextBlock"],
                Text = $"{value}",
                Foreground = foregroundBrush
            };
            Grid grid = new()

            {
                Style = (Style)Application.Current.Resources["DataGrid.CellGrid"],
                Background = backgroundBrush
            };
            grid.Children.Add(textBlock);

            Border? border = new()
            {
                Style = (Style)Application.Current.Resources["DataGrid.CellBorder"],
                Child = grid
            };

            return border;
        }

        public static SolidColorBrush CellValueBrushConverter(double? value, Dictionary<string, SolidColorBrush> brushes, (double?, double?) bounds)
        {
            var upperBound = bounds.Item1;
            var lowerBound = bounds.Item2;

            if (value != null &&
                upperBound > lowerBound)
            {
                switch (value)
                {
                    case double col when col > upperBound:
                        return brushes["highValue"];
                    case double col when col < lowerBound:
                        return brushes["lowValue"];
                    case double col when col >= lowerBound && col <= upperBound:
                        return brushes["acceptableValue"];
                }
            }

            if (upperBound != null && lowerBound != null)
            {
                Dashboard.Instance.SystemFeedback = "ERROR: Invalid Lower/Upper Bounds";
            }

            return new SolidColorBrush(Colors.Black);
        }

        public static Dictionary<string, SolidColorBrush> LoadBrushes(Application application)
        {
            // Checks if brushes exist
            if (application.Resources["Secondary.MainBrush"] is SolidColorBrush background &&
                 application.Resources["Datagrid.HighlightedValueBrush"] is SolidColorBrush highlightedValue &&
                 application.Resources["Datagrid.AcceptableValueBrush"] is SolidColorBrush acceptableValue &&
                 application.Resources["Datagrid.HighValueBrush"] is SolidColorBrush highValue &&
                 application.Resources["Datagrid.LowValueBrush"] is SolidColorBrush lowValue) {}
            // Otherwise assigns default B/W brushes
            else
            {
                background = Brushes.White;
                highlightedValue = Brushes.Black;
                acceptableValue = Brushes.Black;
                highValue = Brushes.Black;
                lowValue = Brushes.Black;
            }

            Dictionary<string, SolidColorBrush> brushes = new()
            {
                { "background", background },
                { "highlightedValue", highlightedValue },
                { "acceptableValue", acceptableValue },
                { "highValue", highValue },
                { "lowValue", lowValue }
            };

            return brushes;
        }
    }
}
