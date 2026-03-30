using ConsoleApp17.Menus;

namespace ConsoleApp17
{
    internal class App
    {
        private LoginMenu loginMenu = new LoginMenu();
        private MainMenu mainMenu = new MainMenu();
        private bool isRunning = true;

        public void Run()
        {

            loginMenu.Show();

            if (Session.User == null)
            {
                Console.WriteLine("Exiting program");
                return;
            }
            while (isRunning)
            {
                mainMenu.Show();
                if (Session.User.Debt > 50000)
                {
                    Console.Clear();
                    Console.WriteLine("You went bankrupt");
                    return;
                }
            }
        }
    }
}
