using System.Text.Json;

namespace ConsoleApp17
{
    class UserHandler
    {
        #region Properties
        private string _name;
        private string _email;
        private string _password;
        #endregion

        #region Ctors
        public UserHandler(string name)
        {
            _name = name;
        }

        public UserHandler(string name, string email, string password)
        {
            _name = name;
            _email = email;
            _password = password;
        }
        #endregion

        #region Methods
        public static void CreateUser(string name, string password)
        {
            string projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
                              .Parent
                              .Parent
                              .Parent
                              .FullName;

            string directoryPath = Path.Combine(projectPath, "Data", "Users");
            string userFolder = Path.Combine(directoryPath, $"{name}Folder");

            if (!Directory.Exists(userFolder))
            {
                Directory.CreateDirectory(userFolder);
            }

            string userJson = Path.Combine(userFolder, $"User{name}.json");

            var userData = new UserData
            {
                Name = name,
                Email = name,
                Password = password,
                Money = 5000,
                Debt = 0,
                LicenseGrade = 1,
                OwnedTruckIds = new List<int> { 1, 2 }
            };



            string json = JsonSerializer.Serialize(userData, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(userJson, json);
        }

        public static UserData LoadUser(string name)
        {
            string projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
                                          .Parent
                                          .Parent
                                          .Parent
                                          .FullName;

            string userFolder = Path.Combine(projectPath, "Data", "Users", $"{name}Folder");
            string userJsonPath = Path.Combine(userFolder, $"User{name}.json");

            if (!File.Exists(userJsonPath))
            {
                Console.WriteLine("This person doesn't exist.");
                return null;
            }

            string json = File.ReadAllText(userJsonPath);
            var user = JsonSerializer.Deserialize<UserData>(json);
            user.OwnedTruckIds ??= new List<int>();

            return user;
        }

        public static void SaveUser(UserData data)
        {
            string projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
                                          .Parent
                                          .Parent
                                          .Parent
                                          .FullName;

            string userFolder = Path.Combine(projectPath, "Data", "Users", $"{data.Name}Folder");

            if (!Directory.Exists(userFolder))
                Directory.CreateDirectory(userFolder);

            string userJson = Path.Combine(userFolder, $"User{data.Name}.json");

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(userJson, json);
        }
        #endregion

        #region NestedClasses
        public class UserData
        {
            public UserData()
            {
                NextDebtPayment = DateTime.Now.AddMinutes(3);
            }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public double Money { get; set; }
            public double Debt { get; set; }
            public int LicenseGrade { get; set; }
            public List<int> OwnedTruckIds { get; set; }
            public DateTime NextDebtPayment { get; set; }
        }
        #endregion
    }
}