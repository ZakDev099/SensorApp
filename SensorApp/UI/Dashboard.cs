using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SensorApp.Data;
using SensorApp.Utils;

namespace SensorApp.UI
{
    public class Dashboard
    {
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
        public int DataGridDisplayRows = dataGridDisplayRows;


        public Dashboard()
        {
            UpdateDataGridDisplay();
        }

        public Dataset NextDataset()
        {
            return DataProcessing.Instance.Datasets[++position];
        }

        public Dataset PreviousDataset()
        {
            return DataProcessing.Instance.Datasets[--position];
        }

        public void SetActiveDataset(Dataset dataset)
        {
            ActiveDataset = dataset;
        }

        public void UpdateDataGridDisplay()
        {
            if (DataGridDisplay == null)
            {
                DataGridDisplay = new double?[DataGridDisplayRows][];
                for (int row = 0; row < DataGridDisplay.Length; row++)
                {
                    DataGridDisplay[row] = new double?[DataGridDisplayColumns];
                    for (int column = 0; column < DataGridDisplay[row].Length; column++)
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