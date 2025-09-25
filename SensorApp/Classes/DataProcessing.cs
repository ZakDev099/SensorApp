using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace SensorApp.Classes
{
    public class DataProcessing
    {
        private static readonly DataProcessing _instance = new DataProcessing();
        public static DataProcessing Instance => _instance;

        private List<Dataset> datasets = [];
        public List<Dataset> Datasets => datasets;

        private DataProcessing() { }

        public void LoadFile()
        {

        }

        public void SaveFile()
        {
            
        }

        public double findAverage(Dataset dataset)
        {
            double sum = 0;
            int divisor = 0;

            foreach (Double i in dataset.Data)
            {
                sum += i;
                divisor++;
            }

            return sum / divisor;
        }

        //return array of matching value locations in active dataset
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