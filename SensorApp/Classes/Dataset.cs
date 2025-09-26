using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SensorApp.classes
{
    public class Dataset(string name, double[,] data)
    {
        public string Name = name;
        public double[,] Data = data;
        public double UpperBound;
        public double LowerBound;

        public void SetBounds(double upperBound, double lowerBound)
        {
            UpperBound = upperBound;
            LowerBound = lowerBound;
        }
    }
}