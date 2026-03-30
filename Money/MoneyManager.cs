namespace ConsoleApp17.Money
{
    class MoneyManager
    {
        private UserHandler.UserData User => Session.User;
        private double TotalDebt;
        public DateTime lastTaxTime = DateTime.Now;

        private const int TaxInterval = 5;

        public override string ToString()
        {
            return $"Money In Acc: {User.Money}, Debt: {User.Debt}, NextPayment: {User.NextDebtPayment}";
        }

        public void SpendMoney(double amount)
        {
            if (User.Money >= amount)
            {
                User.Money -= amount;
                UserHandler.SaveUser(User);
            }
            else
            {
                Console.WriteLine("Not enough money to spend!");
            }
        }

        public void AddMoney(double amount)
        {
            User.Money += amount;
            UserHandler.SaveUser(User);
        }

        public void TakeOutDebt(double amount)
        {
            User.Money += amount;

            double debtAmount = amount * 1.25;
            User.Debt += debtAmount;
            TotalDebt = debtAmount;

            User.NextDebtPayment = DateTime.Now.AddMinutes(1);
            UserHandler.SaveUser(User);
        }

        public void PayBackDebt(double amount)
        {
            if (User.Debt <= 0)
            {
                Console.WriteLine("No debt");
                return;
            }
            if (User.Money >= amount)
            {
                DebtPayment(amount);
                TotalDebt -= amount;
            }
            else
            {
                Console.WriteLine("Can't pay back that much");
            }
        }

        public void DebtPayment(double amount)
        {
            if (User.Money < amount)
            {
                Console.WriteLine("Not enough money");
                return;
            }

            User.Money -= amount;
            User.Debt -= amount;

            if (User.Debt < 0)
            {
                User.Debt = 0;
                User.NextDebtPayment = DateTime.Now;
            }
            UserHandler.SaveUser(User);
        }

        public void UpdateDebt()
        {
            if (User.Debt <= 0)
            {
                User.NextDebtPayment = DateTime.Now;
                return;
            }
            if (DateTime.Now >= User.NextDebtPayment)
            {
                double payment = TotalDebt * 0.2;
                User.NextDebtPayment = User.NextDebtPayment.AddMinutes(1);
                DebtPayment(payment);
                UserHandler.SaveUser(User);
            }
        }


        public void Taxes()
        {
            var user = Session.User;
            var truckCount = user.OwnedTruckIds.Count;

            if ((DateTime.Now - lastTaxTime).TotalMinutes >= TaxInterval)
            {
                double taxAmount = truckCount * 10000;

                if (taxAmount > 0)
                {
                    if (user.Money >= taxAmount)
                    {
                        user.Money -= taxAmount;
                        Console.WriteLine($"Taxes paid for {truckCount} truck(s): ${taxAmount}");
                    }
                    else
                    {
                        Console.WriteLine($"Not enough money to pay taxes for {truckCount} truck(s)! Adding ${taxAmount} to debt.");
                        TakeOutDebt(taxAmount);
                    }

                    UserHandler.SaveUser(user);
                }

                lastTaxTime = DateTime.Now;
            }
        }
    }
}