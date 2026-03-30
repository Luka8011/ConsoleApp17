using ConsoleApp17;
using ConsoleApp17.Menus;
using ConsoleApp17.Money;

public class ShopMenu : Menu
{
    Loader TruckLoader;
    List<Truck.TruckData> trucks;
    Shop shop = new Shop();
    public ShopMenu()
    {
        TruckLoader = new Loader("Trucks");
        trucks = TruckLoader.Load<List<Truck.TruckData>>();
    }
    public override void Show()
    {
        var user = Session.User;

        Console.WriteLine("---Shop---");
        Console.WriteLine($"1) Buy next license lvl. Max lvl 3. Current lvl {user.LicenseGrade}");
        Console.WriteLine("2) Buy a truck");
        Console.WriteLine("3) Back");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.WriteLine("Buy next level of id for $8000? Max job payout will be increased by 1500 (y/n)");
                string input1 = Console.ReadLine();
                if (input1.ToLower() == "y")
                {
                    shop.BuyId();
                }
                else
                {
                    return;
                }
                break;
            case "2":
                PickATruck();
                return;
            case "3":
                return;
        }
    }
    public void PickATruck()
    {
        var user = Session.User;
        var notOwnedTrucks = trucks
            .Where(t => !user.OwnedTruckIds.Contains(t.Id))
            .ToList();

        if (!notOwnedTrucks.Any())
        {
            Console.WriteLine("No more trucks left to buy");
        }

        Console.WriteLine("---Select A Truck By Id---");
        Console.WriteLine("Press x to go back");

        foreach (var truck in notOwnedTrucks)
        {
            Console.WriteLine($"{truck} | Price: {truck.Price}");
        }
        bool work = true;
        while (work)
        {
            string input = Console.ReadLine();

            if (input == "x")
            {
                return;
            }
            if (!notOwnedTrucks.Any(t => t.Id == int.Parse(input)))
            {
                Console.WriteLine("Already own that truck. Enter to go back");
                Console.ReadLine();
                return;
            }
            var selectedTruck = trucks.FirstOrDefault(t => t.Id == int.Parse(input));

            shop.BuyTruck(selectedTruck.Id);

            work = false;
        }
    }
}