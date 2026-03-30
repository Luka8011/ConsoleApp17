namespace ConsoleApp17.Menus
{
    class LoginMenu : Menu
    {
        public override void Show()
        {
            bool inMenu = true;

            while (inMenu)
            {
                Console.Clear();
                Console.WriteLine("---Register/Login---");
                Console.WriteLine("1) Register");
                Console.WriteLine("2) Login");
                Console.WriteLine("0) Exit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Password: ");
                        string password = Console.ReadLine();

                        UserHandler.CreateUser(name, email, password);
                        Console.WriteLine("User Created. Press Enter to continue.");
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.Write("Enter Name: ");
                        string loginName = Console.ReadLine();

                        Console.WriteLine("Enter Password:");

                        string passWord = Console.ReadLine();
                        var user = UserHandler.LoadUser(loginName);


                        if (user != null && user.Password == passWord)
                        {
                            Session.User = user;
                            Session.CurrentUserMoney = new Money.MoneyManager();
                            Console.WriteLine($"Welcome back, {user.Name}! Press Enter to continue.");
                            Console.ReadLine();
                            inMenu = false;
                        }
                        else
                        {
                            Console.WriteLine("User not found. Press Enter to continue.");
                            Console.ReadLine();
                        }
                        break;

                    case "0":
                        Console.WriteLine("Exiting program...");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid input. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}