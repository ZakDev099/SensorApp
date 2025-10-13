using SensorApp.Data;
using SensorApp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SensorApp.UI
{
    public class Dashboard : INotifyPropertyChanged
    {
        private static readonly Dashboard _instance = new();
        public static Dashboard Instance => _instance;
        private Dashboard()
        {
            UpdateDataGridDisplay();
        }

        private Dataset? activeDataset;
        public Dataset? ActiveDataset
        {
            get { return activeDataset; }
            set
            {
                activeDataset = value;
                UpdateDataGridDisplay();
                OnPropertyChanged();
            }
        }
        private int position = 0;
        private int Position
        {
            get => position;
            set
            {
                if (value < 0)
                {
                    position = DataProcessing.Instance.AllDatasets.Count - 1;
                }
                else if (value > DataProcessing.Instance.AllDatasets.Count - 1)
                {
                    position = 0;
                }
                else
                {
                    position = value;
                }
            }
        }

        private double?[][]? dataGridDisplay;
        public double?[][]? DataGridDisplay 
        { 
            get => dataGridDisplay; 
            set
            {
                dataGridDisplay = value;
                OnPropertyChanged();
            }
        }

        private const int dataGridDisplayColumns = 20;
        public int DataGridDisplayColumns => dataGridDisplayColumns;

        private const int dataGridDisplayRows = 100;

        public event PropertyChangedEventHandler? PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadNewDataset()
        {
            var dp = DataProcessing.Instance;
            
            dp.LoadFile();
            ActiveDataset = dp.AllDatasets[^1];
            Position = dp.AllDatasets.Count - 1;
        }

        public void NextDataset()
        {
            if (DataProcessing.Instance.AllDatasets.Count > 1)
            {
                Position++;
                ActiveDataset = DataProcessing.Instance.AllDatasets[Position];
            }
        }

        public void PreviousDataset()
        {
            if (DataProcessing.Instance.AllDatasets.Count > 1)
            {
                Position--;
                ActiveDataset = DataProcessing.Instance.AllDatasets[Position];
            }
        }

        public void UpdateDataGridDisplay()
        {
            
            var dataGrid = new double?[dataGridDisplayRows][];
            for (int row = 0; row < dataGridDisplayRows; row++)
            {
                dataGrid[row] = new double?[DataGridDisplayColumns];
                for (int column = 0; column < DataGridDisplayColumns; column++)
                {
                    dataGrid[row][column] = null;
                }
            }

            if (ActiveDataset != null)
            {
                var ads = ActiveDataset.Data;
                int rowCounter = 0;
                int columnCounter = 0;

                foreach (double[] ads_Row in ads)
                {

                    foreach (double ads_Column in ads_Row)
                    {
                        if (rowCounter < 100)
                        {
                            dataGrid[rowCounter][columnCounter++] = ads_Column;
                            if (columnCounter >= 20)
                            {
                                columnCounter = 0;
                                rowCounter++;
                            }
                        }
                        else
                        {
                            // Alert user of error
                            return;
                        }

                    }
                }
            }

            DataGridDisplay = dataGrid;
        }
    }
}