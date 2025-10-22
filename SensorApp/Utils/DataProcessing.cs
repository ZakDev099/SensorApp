using Microsoft.Win32;
using SensorApp.Data;
using SensorApp.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SensorApp.Utils
{
    public class DataProcessing
    {
        private static readonly DataProcessing _instance = new();
        public static DataProcessing Instance => _instance;
        private DataProcessing() 
        {
            allDatasets = [];
        }

        private readonly List<Dataset> allDatasets;
        public List<Dataset> AllDatasets => allDatasets;

        public void LoadFile()
        {
            var userFile = new OpenFileDialog
            {
                Title = "Select a file to load",
                Filter = "Binary Files (*.bin)|*.bin|All Files (*.*)|*.*"
            };

            bool? result = userFile.ShowDialog();
            if (result != true)
            {
                Dashboard.Instance.SystemFeedback = "Dataset failed to load.";
                return;
            }

            using var reader = new BinaryReader(File.Open(userFile.FileName, FileMode.Open));

            int datasetRows = reader.ReadInt32();
            double[][] data = new double[datasetRows][];

            for (int row = 0; row < datasetRows; row++)
            {
                int datasetColumns = reader.ReadInt32();
                data[row] = new double[datasetColumns];
                for (int column = 0; column < datasetColumns; column++)
                    data[row][column] = reader.ReadDouble();
            }

            Dataset dataset = new($"Dataset {(AllDatasets.Count) + 1}", data);
            dataset.AverageValue = FindAverage(dataset.Data);
            dataset.SortedData = SortDataset(dataset.Data);
            AllDatasets.Add(dataset);
            Dashboard.Instance.SystemFeedback = "Dataset loaded successfully.";
        }

        public static double FindAverage(double[][] data)
        {
            double sum = 0;
            int divisor = 0;

            foreach (double[] x in data)
            {
                foreach (double? y in x)
                {
                    if (y.HasValue)
                    {
                        sum += y.Value;
                        divisor++;
                    }
                }
            }

            return sum / divisor;
        }

        public static List<Tuple<int, double>> SortDataset(double[][] data) 
        {
            int iterator = 0;
            List<Tuple<int, double>> dataset = new();

            foreach (double[] row in data)
            {
                foreach (double column in row)
                {
                    dataset.Add(new Tuple<int, double>(iterator++, column));
                }
            }

            var sortDataset = from entry in dataset orderby entry.Item2 ascending select entry;

            List<Tuple<int, double>> sortedDataset = sortDataset.OrderBy(kvp => kvp.Item2).ToList();

            return sortedDataset;
        }

        public static void BinarySearch(Dataset? dataset)
        {
            List<int> targetLocations = [];

            if (dataset != null && 
                dataset.TargetValue != null && 
                dataset.SortedData != null &&
                dataset.SortedData.Count > 0)
            {
                var target = dataset.TargetValue;
                var data = dataset.SortedData;

                int floor = 0;
                int ceiling = data.Count - 1;
                int mid;

                while (floor <= ceiling)
                {
                    mid = (floor + ceiling) / 2;

                    if (data[mid].Item2 == target)
                    {
                        targetLocations.Add(data[mid].Item1);
                        int mid2 = mid;

                        while (data[++mid].Item2 == target)
                        {
                            targetLocations.Add(data[mid].Item1);
                        }
                        while (data[--mid2].Item2 == target)
                        {
                            targetLocations.Add(data[mid].Item1);
                        }

                        dataset.TargetValueLocations = targetLocations;
                        return;
                    }
                    else
                    {
                        if (data[mid].Item2 < target)
                        {
                            floor = mid + 1;
                        }
                        else
                        {
                            ceiling = mid - 1;
                        }
                    }
                }

                dataset.TargetValueLocations = targetLocations;
                return;
            }
            else
            {
                return;
            }
        }
    }
}