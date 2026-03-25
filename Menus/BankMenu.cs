using ConsoleApp17;
using ConsoleApp17.Menus;
using ConsoleApp17.Money;

public class BankMenu : Menu
{
    MoneyManager money = Session.CurrentUserMoney;
    UserHandler user = Session.UserHandler;

    Loader TruckLoader;
    List<TruckData> trucks;

    public override void Show()
    {
        Console.WriteLine("---Bank---");
        Console.WriteLine("1) Take Out Debt");
        Console.WriteLine("2) Pay Back Debt");
        Console.WriteLine("3) Back");

        Console.WriteLine("You will pay back 10% of your owed debt every 3 mins");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.WriteLine("Enter Amount to take out. X to exit");
                string input1 = Console.ReadLine();
                if (input.ToLower() == "x")
                {
                    return;
                }
                else
                {
                    money.TakeOutDebt(int.Parse(input1));
                    break;
                }
            case "2":
                Console.WriteLine("Enter Amount to pay back. X to exit");
                string input2 = Console.ReadLine();
                if (input.ToLower() == "x")
                {
                    return;
                }
                else
                {
                    money.PayBackDebt(int.Parse(input2));
                    break;
                }
                return;
            case "3":
                return;
        }
    }

}
