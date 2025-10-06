using SensorApp.classes;

namespace SensorApp;

public class Program
{
    static void Main(string[] args)
    {
        Double[,] sampleData = { { 1, 0, 1 }, { 2, 1, 4 }, { 3, 0, 6 }, { 4, 9, 1 } };
        Dataset dataset = new Dataset("MyDataset", sampleData);
        foreach (Double i in dataset.Data) {
            Console.WriteLine(i);
        }
        Console.ReadLine();
    }
}