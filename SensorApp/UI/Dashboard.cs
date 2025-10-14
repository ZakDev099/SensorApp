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
        private Dashboard() => UpdateDataGridDisplay();

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

        private double? upperBound = null;
        public double? UpperBound 
        { 
            get { return upperBound; }
            set
            {
                if (upperBound != value)
                {
                    upperBound = value;
                    OnPropertyChanged();
                }
            }
        }
        private double? lowerBound = null;
        public double? LowerBound
        {
            get { return lowerBound; }
            set
            {
                if (lowerBound != value)
                {
                    lowerBound = value;
                    OnPropertyChanged();
                }
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

        private double?[]? dataGridDisplay;
        public double?[]? DataGridDisplay
        {
            get => dataGridDisplay;
            set
            {
                dataGridDisplay = value;
                OnPropertyChanged();
            }
        }

        public int DataGridDisplayColumns { get; } = 20;
        public int DataGridDisplayRows { get; } = 100;

        public event PropertyChangedEventHandler? PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadNewDataset()
        {
            var dp = DataProcessing.Instance;
            
            dp.LoadFile();
            if (dp.AllDatasets.Count > 0)
            {
                ActiveDataset = dp.AllDatasets[^1];
                Position = dp.AllDatasets.Count - 1;
            }
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
            int cellCount = DataGridDisplayColumns * DataGridDisplayRows;
            int counter = 0;
            
            var dataGrid = new double?[cellCount];

            if (ActiveDataset != null)
            {
                foreach (double[] Row in ActiveDataset.Data)
                {
                    foreach (double Column in Row)
                    {
                        dataGrid[counter++] = Column;
                    }
                }
            }

            while (counter < cellCount)
            {
                dataGrid[counter++] = null;
            }

            DataGridDisplay = dataGrid;
        }
    }
}