using System.Text.Json;

class MoneyManager
{
    public double Money { get; set; }
    public double Debt { get; set; }
    public double FuelPrice { get; set; } = 1.2;

    string BankPath = @"C:\Users\PC\source\repos\ConsoleApp17\ConsoleApp17\Jsons\Bank.json";

    public MoneyManager()
    {
        LoadBank(BankPath);
    }
    private void LoadBank(string path)
    {
        string json = File.ReadAllText(path);
        var data = JsonSerializer.Deserialize<MoneyManager>(json);

        Money = data.Money;
        Debt = data.Debt;
        FuelPrice = data.FuelPrice;
    }
    public void SpendMoney(double amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            SaveBank();
        }
        else
        {
            Console.WriteLine("Not enough money to spend!");
        }
    }

    public void AddMoney(double amount)
    {
        Money += amount;
        SaveBank();
    }

    private void SaveBank()
    {
        string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(BankPath, json);
    }
}