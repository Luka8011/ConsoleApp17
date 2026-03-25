using ConsoleApp17;
using ConsoleApp17.Menus;

public class TruckMenu : Menu
{
    UserHandler user = Session.UserHandler;

    Loader TruckLoader;
    List<TruckData> trucks;
    public TruckMenu()
    {
        TruckLoader = new Loader("Trucks");
        trucks = TruckLoader.Load<List<TruckData>>();
    }
    public override void Show()
    {

        var ownedTrucks = trucks
    .Where(t => user.OwnedTruckIds.Contains(t.Id));

        Console.WriteLine("---Trucks---");
        Console.WriteLine("Owned Trucks");
        foreach (var truck in ownedTrucks)
        {
            Console.WriteLine(truck);
        }


        Console.WriteLine("1) Look for a job");
        Console.WriteLine("2) ");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":

                break;
            case "2":

                return;
            case "3":
                return;
        }
    }
    public void PickATruck()
    {
        Console.WriteLine("---Select A Truck By Id---");
        foreach (var truck in notOwnedTrucks)
        {
            Console.WriteLine(truck);
        }
        int input = int.Parse(Console.ReadLine());

        var selectedTruck = trucks.FirstOrDefault(t => t.Id == input);

        shop.BuyTruck(selectedTruck.Id);
    }
}
