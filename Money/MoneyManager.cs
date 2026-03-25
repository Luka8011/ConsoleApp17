namespace ConsoleApp17.Money
{
    class MoneyManager
    {
        private UserHandler.UserData userData;

        public MoneyManager(string name)
        {
            userData = UserHandler.LoadUser(name);
        }
        public override string ToString()
        {
            return $"Money In Acc: {userData.Money}, Debt: {userData.Debt}";
        }

        public void SpendMoney(double amount)
        {
            if (userData.Money >= amount)
            {
                userData.Money -= amount;
                UserHandler.SaveUser(userData);
            }
            else
            {
                Console.WriteLine("Not enough money to spend!");
            }
        }

        public void AddMoney(double amount)
        {
            userData.Money += amount;
            UserHandler.SaveUser(userData);
        }

        public void TakeOutDebt(double amount)
        {
            AddMoney(amount);
            userData.Debt += amount;
        }

        public void PayBackDebt(double amount)
        {
            if (userData.Money >= amount)
            {
                SpendMoney(amount);
                userData.Debt -= amount;
            }
            else
            {
                Console.WriteLine("can't pay back that much");
                return;
            }
        }

        public void PayPercentage(double percent)
        {
            double payment = userData.Debt * percent;
            userData.Debt -= payment;
            if (userData.Debt < 0)
            {
                userData.Debt = 0;
            }

            Console.WriteLine($"Debt Payment made:{payment}, Remaining Debt {userData.Debt}");
        }
    }
}