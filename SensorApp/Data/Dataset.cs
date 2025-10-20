using SensorApp.Utils;
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
        public List<KeyValuePair<int, double>>? SortedData { get; set; }
        public double? TargetValue { get; set; }
        public List<int>? TargetValueLocations { get; set; }
        public double? UpperBound { get; set; }
        public double? LowerBound { get; set; }
        public double AverageValue { get; set; }
    }
}