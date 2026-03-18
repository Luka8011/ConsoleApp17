using System.Text.Json;

namespace ConsoleApp17
{
    class Loader
    {
        public string Name { get; set; }
        private string path;
        public Loader(string name)
        {
            Name = name;
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "GameData", $"{Name}.json");
        }
        public T Load<T>()
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
        public void Save<T>()
        {
            string json = JsonSerializer.Serialize(path);
            File.WriteAllText(path , json);
        }
    }
}
