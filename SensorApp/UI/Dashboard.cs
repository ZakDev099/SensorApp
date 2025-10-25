using SensorApp.Data;
using SensorApp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SensorApp.UI
{
    public class Dashboard : INotifyPropertyChanged
    {
        private static readonly Dashboard _instance = new();
        public static Dashboard Instance => _instance;
        private Dashboard()
        {
            ActiveDataGrid = new DataGridView(20, 100);
            UpdateDataGridView();
        }

        private Dataset? activeDataset;
        public Dataset? ActiveDataset
        {
            get => activeDataset;
            set
            {
                activeDataset = value;
                UpdateDataGridView();
                OnPropertyChanged();
                RefreshUI();
            }
        }

        private int datasetIndex = 0;
        private int DatasetIndex
        {
            get => datasetIndex;
            set
            {
                if (value < 0)
                {
                    datasetIndex = DataProcessing.Instance.AllDatasets.Count - 1;
                }
                else if (value > DataProcessing.Instance.AllDatasets.Count - 1)
                {
                    datasetIndex = 0;
                }
                else
                {
                    datasetIndex = value;
                }
            }
        }

        public DataGridView ActiveDataGrid { get; set; }

        public double? UpperBound
        {
            get
            {
                if (ActiveDataset != null)
                {
                    return ActiveDataset.UpperBound;
                }
                else return null;
            }
            set
            {
                if (UpperBound != value &&
                    ActiveDataset != null)
                {
                    ActiveDataset.UpperBound = value;
                }

                OnPropertyChanged();
            }
        }

        public double? LowerBound
        {
            get
            {
                if (ActiveDataset != null)
                {
                    return ActiveDataset.LowerBound;
                }
                else return null;
            }
            set
            {
                if (LowerBound != value &&
                    ActiveDataset != null)
                {
                    ActiveDataset.LowerBound = value;
                }

                OnPropertyChanged();
            }
        }

        public double? TargetValue
        {
            get
            {
                if (ActiveDataset != null)
                {
                    return ActiveDataset.TargetValue;
                }
                else return null;
            }
            set
            {
                if (TargetValue != value &&
                    ActiveDataset != null)
                {
                    ActiveDataset.TargetValue = value;
                }

                OnPropertyChanged();
            }
        }

        private string systemFeedback = "";
        public string SystemFeedback
        {
            get => systemFeedback;
            set
            {
                systemFeedback = value;
                OnPropertyChanged();
            }
        }

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
                DatasetIndex = dp.AllDatasets.Count - 1;
            }
        }

        public void NextDataset()
        {
            if (DataProcessing.Instance.AllDatasets.Count > 1)
            {
                DatasetIndex++;
                ActiveDataset = DataProcessing.Instance.AllDatasets[DatasetIndex];
            }
            else
            {
                SystemFeedback = "No dataset loaded";
            }
        }

        public void PreviousDataset()
        {
            if (DataProcessing.Instance.AllDatasets.Count > 1)
            {
                DatasetIndex--;
                ActiveDataset = DataProcessing.Instance.AllDatasets[DatasetIndex];
            }
            else
            {
                SystemFeedback = "No dataset loaded";
            }
        }

        public void UpdateDataGridView()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            Application application = Application.Current;

            if (ActiveDataset != null)
            {
                ActiveDataset.TargetValueLocations = DataProcessing.BinarySearch(ActiveDataset.TargetValue, ActiveDataset.SortedData);
            }

            ActiveDataGrid.UpdateDataGrid(ActiveDataset, mainWindow, application);
        }

        private void RefreshUI()
        {
            if (ActiveDataset != null)
            {
                UpperBound = ActiveDataset.UpperBound;
                LowerBound = ActiveDataset.LowerBound;
                TargetValue = ActiveDataset.TargetValue;
            }
        }

    }
}