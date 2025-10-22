using Microsoft.VisualStudio.TestTools.UnitTesting;
using SensorApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorApp.Utils.Tests
{
    [TestClass()]
    public class DataProcessingTests
    {
        [TestMethod()]
        public void FindAverageTest_Typical()
        {
            double[][] sampleData =
            [
                [4.57, 6.81, 9.90, 27.21, 34.65, 91.76, 87.10],
                [12, 18, 24],
                [15.2, 25.6, 31.1, 89.9]
            ];
            double expected = 34.13;
            var result = DataProcessing.FindAverage(sampleData);
            result = Math.Round(result, 2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void FindAverageTest_Edge()
        {
            double[][] sampleData =
                [
                    [9.67]
                ];
            double expected = 9.67;
            var result = DataProcessing.FindAverage(sampleData);
            result = Math.Round(result, 2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void FindAverageTest_Edge2()
        {
            double[][] sampleData =
                [
                    [99987.12, 787821.98, 8763.07],
                    [9997783.18, 92938193.78]
                ];
            double expected = 20766509.83;
            var result = DataProcessing.FindAverage(sampleData);
            result = Math.Round(result, 2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void FindAverageTest_Null()
        {
            double[][] sampleData = [];
            double expected = double.NaN;
            var result = DataProcessing.FindAverage(sampleData);

            Assert.AreEqual(result, expected);
        }
    }
}