using System;

namespace ConsoleApp17.Money
{
    class MoneyManager
    {
        public double Money { get; set; }
        public double Debt { get; set; }
        public double FuelPrice { get; set; } = 1.2;

        Loader BankLoader = new Loader("Bank");

        public MoneyManager()
        {
            var data = BankLoader.Load<MoneyManager>();

            Money = data.Money;
            Debt = data.Debt;
            FuelPrice = data.FuelPrice;
        }

        public override string ToString()
        {
            return $"Money In Acc: {Money}, Debt: {Debt}";
        }

        public void SpendMoney(double amount)
        {
            if (Money >= amount)
            {
                Money -= amount;
                BankLoader.Save<MoneyManager>(); 
            }
            else
            {
                Console.WriteLine("Not enough money to spend!");
            }
        }

        public void AddMoney(double amount)
        {
            Money += amount;
            BankLoader.Save<MoneyManager>(); 
        }
    }
}