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

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void BinarySearchTest_Typical()
        {
            var sampleTarget = 19.51;
            List<(int, double)> sampleData =
            [
                (8, 7.48),
                (9, 9.95),
                (6, 12.15),
                (2, 15.47),
                (10, 16.54),
                (3, 19.51),
                (1, 20.15),
                (4, 75.46),
                (7, 89.36),
                (5, 89.72),
            ];
            List<int> expected = [3];

            var result = DataProcessing.BinarySearch(sampleTarget, sampleData);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void BinarySearchTest_DuplicateValues()
        {
            var sampleTarget = 19.51;
            List<(int, double)> sampleData =
            [
                (8, 7.48),
                (9, 9.95),
                (6, 12.15),
                (2, 15.47),
                (10, 16.54),
                (3, 19.51),
                (11, 19.51),
                (13, 19.51),
                (1, 20.15),
                (4, 75.46),
                (7, 89.36),
                (12, 89.37),
                (5, 89.72),
            ];
            List<int> expected = [3, 11, 13];
            var result = DataProcessing.BinarySearch(sampleTarget, sampleData);
            bool targetsFound = true;

            foreach (int address in expected) 
            { 
                if (result.Contains(address)) {}
                else
                {
                    targetsFound = false;
                    break;
                }
            }

            Assert.IsTrue(targetsFound);
        }

        [TestMethod()]
        public void BinarySearchTest_Edge()
        {
            var sampleTarget = 19.51;
            List<(int, double)> sampleData =
            [
                (15, -489183429.09),
                (16, -127.89),
                (14, -68.87),
                (8, -7.48),
                (9, 9.95),
                (6, 12.15),
                (2, 15.47),
                (10, 16.54),
                (3, 19.51),
                (11, 19.51),
                (13, 19.51),
                (1, 20.15),
                (4, 75.46),
                (7, 89.36),
                (12, 89.37),
                (5, 89.72),
                (17, 14000.71),
                (18, 87987723402.89)
            ];
            List<int> expected = [3, 11, 13];
            var result = DataProcessing.BinarySearch(sampleTarget, sampleData);
            bool targetsFound = true;

            foreach (int address in expected)
            {
                if (result.Contains(address)) { }
                else
                {
                    targetsFound = false;
                    break;
                }
            }

            Assert.IsTrue(targetsFound);
        }

        [TestMethod()]
        public void BinarySearchTest_Null()
        {
            double? sampleTarget = null;
            List<(int, double)> sampleData = [];
            List<int> expected = [];

            var result = DataProcessing.BinarySearch(sampleTarget, sampleData);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}