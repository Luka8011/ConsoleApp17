using ConsoleApp17.Money;

namespace ConsoleApp17
{
    class Truck
    {
        #region Properties
        private Loader TruckLoader = new Loader("Trucks");
        public List<TruckData> Data;
        private JobGenerator JobGenerator = new JobGenerator();

        public Truck()
        {
            Data = TruckLoader.Load<List<TruckData>>();
        }
        #endregion

        #region Methods

        public async Task Move(int id, JobGenerator.Job job)
        {
            if (job == null)
            {
                Console.WriteLine("No job assigned");
                return;
            }

            var truck = Data.FirstOrDefault(x => x.Id == id);
            if (truck == null)
            {
                Console.WriteLine("Truck not found");
                return;
            }

            double fuelNeeded = (job.Distance / 100.0) * truck.FuelUse;

            if (truck.Location != job.From)
            {
                var emptyJob = new JobGenerator.Job
                {
                    Pay = 0,
                    From = truck.Location,
                    To = job.From,
                    Distance = JobGenerator.GetDistance(truck.Location, job.From)
                };

                Console.WriteLine($"Truck is at {truck.Location}, not {job.From}. Fuel and Time needed will increase | 1) Accept | 2) Deny");



                int input1 = int.Parse(Console.ReadLine());
                if (input1 == 1)
                {
                    await MoveInternal(truck, emptyJob);
                    Refuel(truck, 1);
                    await MoveInternal(truck, job);
                }
                else
                {
                    Console.WriteLine("Cancelled.");
                }

                return;
            }

            if (truck.FuelInTank >= fuelNeeded && !truck.IsBusy)
            {
                await MoveInternal(truck, job);
            }
            else if (truck.FuelUse <= fuelNeeded && !truck.IsBusy)
            {
                Console.WriteLine("Not enough fuel. Refuel? (y/n)");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    Refuel(truck);
                }
                await MoveInternal(truck, job);
                Console.WriteLine("Press Enter To Continue");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Truck is busy");
            }
        }

        private async Task MoveInternal(TruckData truck, JobGenerator.Job job)
        {
            double fuelNeeded = (job.Distance / 100.0) * truck.FuelUse;

            if (truck.IsBusy)
            {
                Console.WriteLine("Truck is busy");
                return;
            }
            if (truck.FuelInTank < fuelNeeded)
            {
                Refuel(truck);
            }

            truck.TargetLocation = job.To;
            truck.IsBusy = true;
            truck.FuelInTank -= (int)fuelNeeded;

            truck.EstimatedArrival = DateTime.Now.AddSeconds(job.Distance / (double)truck.Speed);
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            while (DateTime.Now < truck.EstimatedArrival)
            {
                await Task.Delay(1000);
            }

            truck.Location = job.To;
            truck.TargetLocation = null;
            truck.IsBusy = false;

            if (job.Pay > 0)
                Session.CurrentUserMoney.AddMoney(job.Pay);

            UserHandler.SaveUser(Session.User);
        }

        public void Refuel(TruckData truck)
        {
            var user = Session.User;
            var money = Session.CurrentUserMoney;

            double fuelNeeded = truck.TankSize - truck.FuelInTank;
            double cost = fuelNeeded * 1.35;

            Console.WriteLine($"Refuel for ${cost}? | 1) Accept | 2) Deny");
            if (int.Parse(Console.ReadLine()) == 1)
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

            UserHandler.SaveUser(user);
        }

        public void Refuel(TruckData truck, int id)
        {
            var user = Session.User;
            var money = Session.CurrentUserMoney;

            double fuelNeeded = truck.TankSize - truck.FuelInTank;
            double cost = fuelNeeded * 1.35;

            if (user.Money >= cost)
            {
                truck.FuelInTank = truck.TankSize;
                money.SpendMoney(cost);
            }

            UserHandler.SaveUser(user);
        }
        #endregion

        #region NestedClasses
        public class TruckData
        {
            public string Location { get; set; }
            public string TargetLocation { get; set; } = null;
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
                return $"Model: {ModelName} | Fuel/100km: {FuelUse} | Tank: {FuelInTank} | Id: {Id} | Speed: {Speed} | Busy: {IsBusy} | At: {Location}";
            }

            public string GetStatus()
            {
                var remaining = EstimatedArrival - DateTime.Now;
                if (IsBusy || remaining.TotalSeconds > 0)
                {
                    if (remaining.TotalSeconds < 0) remaining = TimeSpan.Zero;
                    return $"Truck {Id} moving From:{Location} To: {TargetLocation} | ETA: {Math.Ceiling(remaining.TotalSeconds)}s";
                }
                else
                {
                    return ToString();
                }
            }
        }
        #endregion
    }
}