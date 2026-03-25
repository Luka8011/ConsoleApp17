namespace ConsoleApp17.Money
{
    class Shop
    {
        MoneyManager money = Session.CurrentUserMoney;
        UserHandler user = Session.UserHandler;
        Loader loader = new Loader("Trucks");
        public void BuyId()
        {
            if (user.LicenseGrade < 3)
            {
                money.SpendMoney(8000);
                user.LicenseGrade++;
            }
            else
            {
                Console.WriteLine("Already max lvl license");
            }
        }

        public void BuyTruck(int id)
        {
            var trucks = loader.Load<List<TruckData>>();

            if (trucks != null)
            {
                var selectedTruck = trucks.FirstOrDefault(t => t.Id == id);
                if (user.Money! < selectedTruck.Price)
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
            }
        }
    }
}
