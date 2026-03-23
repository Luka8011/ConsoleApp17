using ConsoleApp17.Misc;
using ConsoleApp17.Money;
using System.Text.Json;

namespace ConsoleApp17
{
    class Truck
    {
        #region Properties
        Loader TruckLoader = new Loader("Trucks");
        MoneyManager money = Session.CurrentUserMoney;
        UserHandler user = Session.UserHandler;

        List<TruckData> Data;

        public Truck()
        {
            Data = TruckLoader.Load<List<TruckData>>();
        }
        #endregion

        #region Methods
        public void Move(int id, JobGenerator.Job job)
        {
            if (job == null)
            {
                Console.WriteLine("no job assigned");
                return;
            }
            var truck = Data.FirstOrDefault(x => x.Id == id);
            if (truck != null)
            {
                if (truck.FuelInTank >= (job.Distance / 100) * truck.FuelUse && !truck.IsBusy)
                {
                    double hours = CalculateTravelTime(job.Distance, truck.Speed);
                    truck.IsBusy = true;
                    truck.FuelInTank -= (job.Distance / 100) * truck.FuelUse;
                    int left = Console.CursorLeft;
                    int top = Console.CursorTop;

                    truck.EstimatedArrival = DateTime.Now.AddSeconds(hours);
                    while (DateTime.Now < truck.EstimatedArrival)
                    {
                        var remaining = truck.EstimatedArrival - DateTime.Now;
                        Console.SetCursorPosition(left, top);
                        Console.Write($"Truck {truck.Id} Arrives In {job.To} | ETA: {Math.Ceiling(remaining.TotalSeconds)}s   ");
                        Thread.Sleep(1000);
                    }
                    truck.IsBusy = false;
                    truck.Location = job.To;

                    money.AddMoney(job.Pay);
                }
                else
                {
                    Console.Write("Not enough fuel in tank.");
                    string accept = Console.ReadLine();
                    if (accept.ToLower() == "y")
                    {
                        Refuel(truck);
                    }
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
                    if (user.Money >= cost)
                    {
                        truck.FuelInTank = truck.TankSize;
                        money.SpendMoney(cost);
                        Console.WriteLine($"Truck {truck.Id} refueled {fuelNeeded}L for ${cost}");
                    }
                    else
                    {
                        Console.WriteLine($"Not enough money to refuel Truck {truck.Id}. Need ${cost}, have ${user.Money}");
                    }
                }
            }
        }
        public void TestSetup()
        {
            Data = new List<TruckData>
            {
                new TruckData
                {
                    Id = 1,
                    ModelName = "Volvo FH",
                    FuelUse = 30,
                    TankSize = 600,
                    FuelInTank = 500,
                    Speed = 80,
                    Location = "Tbilisi"
                }
            };
        }
        double CalculateTravelTime(int distance, int speed)
        {
            return distance / (double)speed;
        }

    }
    #endregion

    #region NestedClasses
    public class TruckData
    {
        public string Location { get; set; }
        public string ModelName { get; set; }
        public int FuelUse { get; set; }
        public int TankSize { get; set; }
        public int Id { get; set; }
        public int Speed { get; set; }
        public int FuelInTank { get; set; }
        public int Price { get; set; }
        public bool IsBusy { get; set; } = false;
        public DateTime EstimatedArrival { get; set; } = DateTime.MinValue;

        public override string ToString()
        {
            return $"Model: {ModelName} Fuel used for 100km: {FuelUse} TankSize: {TankSize} Id: {Id}";
        }
    }
    #endregion
}
