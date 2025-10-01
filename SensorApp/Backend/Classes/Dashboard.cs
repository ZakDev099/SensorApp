using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SensorApp.Classes;

namespace SensorApp.classes
{
    public class Dashboard
    {
        public Dataset? ActiveDataset;
        private int Position = 0;

        public Dataset NextDataset()
        {
            return DataProcessing.Instance.Datasets[++Position];
        }

        public Dataset PreviousDataset()
        {
            return DataProcessing.Instance.Datasets[--Position];
        }

        public void SetActiveDataset(Dataset dataset)
        {
            ActiveDataset = dataset;
        }
    }
}