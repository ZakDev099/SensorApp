using SensorApp.Data;
using SensorApp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SensorApp.UI
{
    public class Dashboard : INotifyPropertyChanged
    {
        private static readonly Dashboard _instance = new Dashboard();
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
            }
        }
        private int position = 0;

        public double?[][]? DataGridDisplay { get; set; }

        private const int dataGridDisplayColumns = 20;
        public int DataGridDisplayColumns => dataGridDisplayColumns;

        private const int dataGridDisplayRows = 100;
        public int DataGridDisplayRows => dataGridDisplayRows;

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
        }

        public void NextDataset()
        {
            ActiveDataset = DataProcessing.Instance.AllDatasets[++position];
        }

        public void PreviousDataset()
        {
            ActiveDataset = DataProcessing.Instance.AllDatasets[--position];
        }

        public void UpdateDataGridDisplay()
        {
            if (DataGridDisplay == null)
            {
                DataGridDisplay = new double?[DataGridDisplayRows][];
                for (int row = 0; row < DataGridDisplayRows; row++)
                {
                    DataGridDisplay[row] = new double?[DataGridDisplayColumns];
                    for (int column = 0; column < DataGridDisplayColumns; column++)
                    {
                        DataGridDisplay[row][column] = null;
                    }
                }
            }

            if (ActiveDataset != null && DataGridDisplay != null)
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
                            DataGridDisplay[rowCounter][columnCounter++] = ads_Column;
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
        }
    }
}