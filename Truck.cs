using ConsoleApp17.Money;
using System.Text.Json;

namespace ConsoleApp17
{
    class Truck
    {
        Loader TruckLoader = new Loader("Trucks");
        MoneyManager money = new MoneyManager();

        List<TruckData> Data;

        public Truck()
        {
            Data = TruckLoader.Load<List<TruckData>>();
        }

        public void Move(int id,int distance,string location)
        {
             var truck = Data.FirstOrDefault(x => x.Id == id);
            if (truck != null)
            {
                if (truck.FuelInTank >= (distance / 100) * truck.FuelUse && !truck.IsBusy)
                {
                    double hours = CalculateTravelTime(distance, truck.Speed);
                    truck.IsBusy = true;
                    truck.FuelInTank -= (distance / 100) * truck.FuelUse;
                    truck.Location = location;

                    truck.EstimatedArrival = DateTime.Now.AddSeconds(hours * 5);
                    while (truck.IsBusy)
                    {
                        Console.Clear();
                        Console.WriteLine($"Truck {truck.Id} is moving to {location}. Arrives in {truck.EstimatedArrival} seconds");

                        Thread.Sleep(100);
                    }
                }
                else
                {
                    Console.WriteLine("Not enough fuel in tank.");
                    Refuel(truck);
                }
            }
        }
        public void Refuel(TruckData truck)
        {

            double fuelNeeded = truck.TankSize - truck.FuelInTank;
            double cost = fuelNeeded * 1.35;
            Console.WriteLine($"Refuel for {cost}?");
            string input = Console.ReadLine();
            if (input != null)
            {
                if (input.ToLower() == "y")
                {
                    if (money.Money >= cost)
                    {
                        truck.FuelInTank = truck.TankSize;
                        money.SpendMoney(cost);
                        Console.WriteLine($"Truck {truck.Id} refueled {fuelNeeded}L for ${cost}");
                    }
                    else
                    {
                        Console.WriteLine($"Not enough money to refuel Truck {truck.Id}. Need ${cost}, have ${money.Money}");
                    }
                }
            }
        }

        double CalculateTravelTime(int distance, int speed)
        {
            return distance / (double)speed;
        }

    }
    public class TruckData
    {
        public string Location { get; set; }
        public string Model { get; set; }
        public int FuelUse { get; set; }
        public int TankSize { get; set; }
        public int Id { get; set; }
        public int Speed { get; set; }
        public int FuelInTank { get; set; }
        public bool IsBusy { get; set; } = false;
        public DateTime EstimatedArrival { get; set; } = DateTime.MinValue;

        public override string ToString()
        {
            return $"Model: {Model} Fuel used for 100km: {FuelUse} TankSize: {TankSize} Id: {Id}";
        }
    }
}
