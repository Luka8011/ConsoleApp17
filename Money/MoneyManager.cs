using System;

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
    }
}