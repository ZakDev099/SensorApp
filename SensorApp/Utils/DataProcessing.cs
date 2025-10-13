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
        private static readonly DataProcessing _instance = new DataProcessing();
        public static DataProcessing Instance => _instance;
        private DataProcessing() 
        {
            allDatasets = new List<Dataset>();
        }

        private List<Dataset> allDatasets;
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
                return;

            using (var reader = new BinaryReader(File.Open(userFile.FileName, FileMode.Open)))
            {
                int datasetRows = reader.ReadInt32();
                double[][] data = new double[datasetRows][];

                for (int row = 0; row < datasetRows; row++)
                {
                    int datasetColumns = reader.ReadInt32();
                    data[row] = new double[datasetColumns];
                    for (int column = 0; column < datasetColumns; column++)
                        data[row][column] = reader.ReadDouble();
                }

                Dataset dataset = new Dataset($"Dataset {(AllDatasets.Count()) + 1}", data);
                dataset.AverageValue = FindAverage(dataset);
                AllDatasets.Add(dataset);
            }
        }

        public static double FindAverage(Dataset dataset)
        {
            double sum = 0;
            int divisor = 0;

            foreach (double[] x in dataset.Data)
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

        //return array of matching target locations in the display grid *Come back to this*
        public List<Tuple<int>> binarySearch(string targetString, Dataset dataset)
        {
            List<Tuple<int>> result = [];
            var target = ParseStringToDouble(targetString);
            if (target != null)
            {
                //binary search algo here (account for duplicates)
            }

            return result;
        }

        public Double? ParseStringToDouble(String value)
        {
            if (double.TryParse(value, out double result))
            {
                return result;
            }
            else
            {
                UserFeedback.ShowError("Search Error, please enter a number.");
                return null;
            }
        }
    }
}