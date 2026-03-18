using System.Text.Json;

namespace ConsoleApp17
{
    class UserHandler
    {
        private string _name;
        private string _email;
        private string _password;
        public int Money;
        public int Debt;

        public UserHandler(string name, string email, string password)
        {
            _name = name;
            _email = email;
            _password = password;
        }

        public void CreateOrSaveUser()
        {
            string projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
                                          .Parent
                                          .Parent
                                          .Parent
                                          .FullName;

            string directoryPath = Path.Combine(projectPath, "Jsons", "Users");
            string userFolder = Path.Combine(directoryPath, $"{_name}Folder");

            if (!Directory.Exists(userFolder))
            {
                Directory.CreateDirectory(userFolder);
            }
            
            string userJson = Path.Combine(userFolder, $"User{_name}.json");

            var userData = new
            {
                Name = _name,
                Email = _email,
                Password = _password,
                Money = Money,
                Debt = Debt
            };

            string json = JsonSerializer.Serialize(userData, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(userJson, json);
        }

        public void LoadUser(string name)
        {
            string DirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsons", "Users");
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            string UserJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsons", "Users", $"User{_name}.json");

            if (!File.Exists(UserJsonPath))
            {
                Console.WriteLine("This person doesn't exist.");
                return;
            }

            string json = File.ReadAllText(UserJsonPath);
            var text = JsonSerializer.Deserialize<object>(json);

            Console.WriteLine(text);
        }
    }
}