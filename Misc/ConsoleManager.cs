using ConsoleApp17.Money;

namespace ConsoleApp17.Misc
{
    class ConsoleManager
    {
        MoneyManager moneyManager = new MoneyManager();
        public void ShowCommands(int input)
        {
            switch (input) 
            {
                case 1:
                    Console.WriteLine(
                    "---Command List---",
                    "1) See all trucks",
                    "2) Manage a truck",
                    "3) Check your bank account",
                    "4) Generate a job",
                    "5) Order a new truck"
                    );
                    break;
                case 2:
                    Console.WriteLine(
                    "---Truck Manager---",
                    "1) Refuel",
                    "2) Move to a new city",
                    "3) Repair",
                    "4) Generate a job",
                    "5) Exit"
                    );
                    break;
                case 3:
                    Console.WriteLine(
                    "---Bank Manager---",
                    "1) Take out debt",
                    "2) Check out debt",
                    "3) Pay back debt",
                    "4) Purchase new liscense",
                    "5) Exit"
                    );
                    break;
                case 4:
                    Console.WriteLine(
                    "---Job Generator---",
                    "1) Generate job",
                    "2) Accept job",
                    "3) Decline job",
                    "5) Exit"
                    );
                    break;
            }
        }
        public void ShowBank()
        {
            moneyManager.ToString();
        }
        public void ShowAllTrucks(List<TruckData> trucks)
        {
            foreach (var truck in trucks)
            {
                truck.ToString();
            }
        }

        public void SeeTruck(List<TruckData> trucks, int id)
        {
            var truck = trucks.FirstOrDefault(x => x.Id == id);
            if (truck != null)
            {
                truck.ToString();
            }
        }
    }
}
