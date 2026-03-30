using ConsoleApp17.Money;

namespace ConsoleApp17.Menus
{
    internal class JobMenu : Menu
    {
        JobGenerator jobGenerator = new JobGenerator();
        Truck truckManager;
        List<Truck.TruckData> trucks;

        public JobMenu(Truck truckManager)
        {
            this.truckManager = truckManager;
            trucks = truckManager.Data ?? new List<Truck.TruckData>();
        }
        public override async void Show()
        {
            Console.Clear();
            var generatedJob = jobGenerator.Generate();

            if (generatedJob != null)
            {
                Console.WriteLine($"Job from {generatedJob.From} to {generatedJob.To} | Pay: {generatedJob.Pay} | Distance: {generatedJob.Distance}");
                Console.WriteLine("1) Accept | 2) Reject");

                var input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.D1)
                {
                    await StartTheJob(generatedJob);
                }
                else if (input == ConsoleKey.D2)
                {
                    Console.WriteLine("Job rejected.");
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }

            Console.WriteLine("Press any key to return...");
            Console.ReadLine();
        }

        private async Task StartTheJob(JobGenerator.Job job)
        {
            var user = Session.User;
            var ownedTrucks = trucks.Where(t => user.OwnedTruckIds.Contains(t.Id)).ToList();

            if (!ownedTrucks.Any())
            {
                Console.WriteLine("You have no trucks to send on this job!");
                return;
            }

            Console.WriteLine("Select Which Truck To Send By Id");
            foreach (var truck in ownedTrucks)
            {
                Console.WriteLine(truck);
            }

            int input = int.Parse(Console.ReadLine());
            var selectedTruck = ownedTrucks.FirstOrDefault(t => t.Id == input);

            if (selectedTruck == null)
            {
                Console.WriteLine("Invalid truck ID!");
                return;
            }

            if (selectedTruck.IsBusy)
            {
                Console.WriteLine("This truck is currently busy!");
                return;
            }

            await truckManager.Move(selectedTruck.Id, job);
            Console.Clear();
        }
    }
}