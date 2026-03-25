using ConsoleApp17;
using ConsoleApp17.Menus;
using ConsoleApp17.Money;

public class ShopMenu : Menu
{
    Shop shop = new Shop();
    UserHandler user = Session.UserHandler;

    Loader TruckLoader;
    List<TruckData> trucks;
    public ShopMenu()
    {
        TruckLoader = new Loader("Trucks");
        trucks = TruckLoader.Load<List<TruckData>>();
    }
    public override void Show()
    {
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
        Console.WriteLine("---Select A Truck By Id---");
        var notOwnedTrucks = trucks
            .Where(t => !user.OwnedTruckIds.Contains(t.Id));
        foreach (var truck in notOwnedTrucks)
        {
            Console.WriteLine(truck);
        }
        int input = int.Parse(Console.ReadLine());

        var selectedTruck = trucks.FirstOrDefault(t => t.Id == input);

        shop.BuyTruck(selectedTruck.Id);
    }
}