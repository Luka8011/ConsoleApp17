using ConsoleApp17.Menus;

public class MainMenu : Menu
{
    public override void Show()
    {
        while (true)
        {
            Console.WriteLine("---Main Menu---");
            Console.WriteLine("1) Truck Menu");
            Console.WriteLine("2) Bank Menu");
            Console.WriteLine("3) Shop Menu");
            Console.WriteLine("4) Exit");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":

                    break;
                case "2":

                    break;
                case "3":

                    break;
                case "4":

                    break;
            }
        }
    }
}
