using System.Text.Json;

namespace ConsoleApp17
{
    class Truck
    {
        MoneyManager money = new MoneyManager();

        string TruckPath = @"C:\Users\PC\source\repos\ConsoleApp17\ConsoleApp17\Jsons\Trucks.Json";
        string TruckJson => File.ReadAllText(TruckPath);

        int FuelNeeded;

        List<TruckData> Data;

        public Truck()
        {
            Data = JsonSerializer.Deserialize<List<TruckData>>(TruckJson);
        }

        public void Move(int id,int distance,string location)
        {
             var truck = Data.FirstOrDefault(x => x.Id == id);
            if (truck.FuelInTank >= (distance / 100) * truck.FuelUse && !truck.IsBusy)
            {
                double hours = CalculateTravelTime(distance, truck.Speed);
                truck.IsBusy = true;
                truck.FuelInTank -= (distance / 100) * truck.FuelUse;
                truck.Location = location;

                truck.EstimatedArrival = DateTime.Now.AddSeconds(hours * 5);
                Console.WriteLine($"Truck {truck.Id} is moving to {location}. Arrives in {truck.EstimatedArrival} seconds");
            }
            else
            {
                Console.WriteLine("Not enough fuel in tank.");
                Refuel(truck);
            }
        }
        public void Refuel(TruckData truck)
        {

            double fuelNeeded = truck.TankSize - truck.FuelInTank;
            double cost = fuelNeeded * money.FuelPrice;
            Console.WriteLine($"Refuel for {cost}?");
            string input = Console.ReadLine();
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
                    Console.WriteLine($"Not enough money to refuel Truck {truck.Id}. Need ${cost:F2}, have ${money.Money:F2}");
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
            return $"Model: {Model} FuelUse: {FuelUse} TankSize: {TankSize} Id: {Id}";
        }
    }
}
