using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SensorApp.Data
{
    public class Dataset(string name, double[][] data)
    {
        public string Name { get; set; } = name;
        public double[][] Data { get; set; } = data;
        public double AverageValue;
        public double UpperBound { get; set; }
        public double LowerBound { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetBounds(double upperBound, double lowerBound)
        {
            UpperBound = upperBound;
            LowerBound = lowerBound;
        }
    }
}