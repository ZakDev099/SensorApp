using SensorApp.Data;
using SensorApp.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace SensorApp.Utils
{
    public class DataProcessing
    {
        private static readonly DataProcessing _instance = new DataProcessing();
        public static DataProcessing Instance => _instance;
        private DataProcessing() { }

        private List<Dataset> datasets = [];
        public List<Dataset> Datasets => datasets;

        public void LoadFile()
        {

        }

        public void SaveFile()
        {
            
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