using SensorApp.classes;

namespace SensorApp;

public class Program
{
    static void Main(string[] args)
    {
        Double[,] sampleData = { { 1, 0 }, { 2, 1 }, { 3, 0 }, { 4, 9 } };
        Dataset dataset = new Dataset("MyDataset", sampleData);
        foreach (Double i in dataset.Data) {
            Console.WriteLine(i);
        }
        Console.ReadLine();
    }
}