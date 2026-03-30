using ConsoleApp17;
using ConsoleApp17.Menus;

public class MainMenu : Menu
{
    private TruckMenu truckMenu = new TruckMenu();
    private BankMenu bankMenu = new BankMenu();
    private ShopMenu shopMenu = new ShopMenu();

    public override void Show()
    {
        bool inMainMenu = true;

        while (inMainMenu)
        {
            Console.Clear();
            Console.WriteLine("---Main Menu---");
            Console.WriteLine($"User: {Session.User.Name} | Money: ${Session.User.Money} | Debt: ${Session.User.Debt} | Licsense lvl: {Session.User.LicenseGrade}");
            Console.WriteLine("1) Truck Menu");
            Console.WriteLine("2) Bank Menu");
            Console.WriteLine("3) Shop Menu");
            Console.WriteLine("4) Exit");


            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    truckMenu.Show();
                    break;

                case "2":
                    bankMenu.Show();
                    break;

                case "3":
                    shopMenu.Show();
                    break;

                case "4":
                    Console.WriteLine("Exiting...");
                    inMainMenu = false;
                    break;

                default:
                    Console.WriteLine("Invalid input. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}