using ConsoleApp17;
using ConsoleApp17.Menus;

public class BankMenu : Menu
{
    public override void Show()
    {
        bool inMenu = true;
        var money = Session.CurrentUserMoney;
        var user = Session.User;

        while (inMenu)
        {
            Console.Clear();

            money.UpdateDebt();

            var remaining = user.NextDebtPayment - DateTime.Now;
            if (remaining.TotalSeconds < 0) remaining = TimeSpan.Zero;
            TimeSpan timeUntilNextTax = (money.lastTaxTime.AddMinutes(5) - DateTime.Now);

            Console.WriteLine("---Bank---");
            Console.WriteLine($"Money: {Math.Ceiling(user.Money)}, Debt: {user.Debt}, Next Debt Payment: {Math.Ceiling(remaining.TotalSeconds)}s");
            Console.WriteLine($"Next Tax payment {Math.Ceiling(timeUntilNextTax.TotalSeconds)}");
            Console.WriteLine("1) Take Out Debt");
            Console.WriteLine("2) Pay Back Debt");
            Console.WriteLine("3) Back");
            Console.WriteLine("25% of total debt is paid automatically every minute.");
            Console.WriteLine("For Every owned truck you pay $10000 every 5 mins");

            if (Console.KeyAvailable)
            {
                var input = Console.ReadKey(true).Key;
                money.Taxes();
                switch (input)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Enter amount to take out (X to cancel):");
                        string input1 = Console.ReadLine();
                        if (input1.ToLower() != "x" && double.TryParse(input1, out double amt1))
                        {
                            money.TakeOutDebt(amt1);
                        }
                        break;

                    case ConsoleKey.D2:
                        Console.WriteLine("Enter amount to pay back (X to cancel):");
                        string input2 = Console.ReadLine();
                        if (input2.ToLower() != "x" && double.TryParse(input2, out double amt2))
                        {
                            money.PayBackDebt(amt2);
                        }
                        break;

                    case ConsoleKey.D3:
                        inMenu = false;
                        break;
                }
            }
            System.Threading.Thread.Sleep(1000);
        }
    }
}