using ConsoleApp17;
using ConsoleApp17.Menus;

public class TruckMenu : Menu
{
    JobMenu job;
    Truck truckManager;
    List<Truck.TruckData> trucks;

    public TruckMenu()
    {
        truckManager = new Truck();
        trucks = truckManager.Data ?? new List<Truck.TruckData>();
        job = new JobMenu(truckManager);
    }

    public override void Show()
    {
        var user = Session.User;

        user.OwnedTruckIds ??= new List<int>();


        bool inTruckMenu = true;
        while (inTruckMenu)
        {
            var ownedTrucks = trucks.Where(t => user.OwnedTruckIds.Contains(t.Id)).ToList();

            Console.WriteLine("---Trucks---");
            Console.WriteLine("Owned Trucks:");

            if (ownedTrucks.Any())
            {
                foreach (var truck in ownedTrucks)
                {
                    Console.WriteLine(truck.GetStatus());
                }
            }
            else
            {
                Console.WriteLine("You do not own any trucks.");
            }

            Console.WriteLine("1) Look for a job");
            Console.WriteLine("2) Back");

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        job.Show();
                        break;

                    case ConsoleKey.D2:
                        inTruckMenu = false;
                        break;
                }
            }

            Thread.Sleep(1000);
            Console.Clear();
        }
    }

}
