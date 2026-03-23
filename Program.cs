using ConsoleApp17;
using ConsoleApp17.Money;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main()
    {
        Truck truckManager = new Truck();
        JobGenerator generator = new JobGenerator();

        truckManager.TestSetup();

        truckManager.Move(1 , generator.Generate(1));

        Console.ReadLine();

    }
}