namespace ConsoleApp17.Money
{
    class Shop
    {
        Loader loader = new Loader("Trucks");
        public void BuyId()
        {
            var user = Session.User;
            var money = Session.CurrentUserMoney;

            if (user.LicenseGrade < 3 && user.Money >= 8000)
            {
                money.SpendMoney(8000);
                user.LicenseGrade++;
            }
            else if (user.Money < 8000)
            {
                Console.WriteLine("Not Enough money");
            }
            else
            {
                Console.WriteLine("Already max lvl license");
            }
        }

        public void BuyTruck(int id)
        {
            var user = Session.User;
            var money = Session.CurrentUserMoney;
            var trucks = loader.Load<List<Truck.TruckData>>();

            if (trucks != null)
            {
                var selectedTruck = trucks.FirstOrDefault(t => t.Id == id);
                if (user.Money >= selectedTruck.Price)
                {
                    Console.WriteLine($"Buy truck {selectedTruck.ModelName} for {selectedTruck.Price}? (y/n)");
                    string input = Console.ReadLine();
                    if (input != null)
                    {
                        if (input.ToLower() == "y")
                        {
                            money.SpendMoney(selectedTruck.Price);
                            user.OwnedTruckIds.Add(selectedTruck.Id);
                            Console.WriteLine("Truck purchased");
                            UserHandler.SaveUser(user);
                        }
                        else if (input.ToLower() == "n")
                        {
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Enter again");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Not enough money!");
                    Console.WriteLine("Press Enter to return");
                    Console.ReadLine();
                    return;
                }
            }
        }
    }
}
