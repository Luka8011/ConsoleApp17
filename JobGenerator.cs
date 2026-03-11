using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace ConsoleApp17
{
    class JobGenerator
    {
        private int Pay { get; set; }
        private int Location { get; set; }
        private int Location2 { get; set; }
        public Random Rand;

        private LocationData data;

        public JobGenerator()
        {
            Rand = new Random();

            string LocationPath = @"C:\Users\PC\source\repos\ConsoleApp17\ConsoleApp17\Jsons\Locations.json";

            string Json = File.ReadAllText(LocationPath);

            data = JsonSerializer.Deserialize<LocationData>(Json);
        }

        public void Generate()
        {
            Pay = Rand.Next(3500, 8001);

            Location = Rand.Next(0, data.locations.Length);
            Location2 = Rand.Next(0, data.locations.Length);
            if (Location2 == Location)
            {
                while (Location2 == Location)
                {
                    Location2 = Rand.Next(0, data.locations.Length);
                }
            }

            Console.WriteLine($"Job: from {data.locations[Location]} to {data.locations[Location2]} Distance: {data.distances[Location][Location2]}, Pay: {Pay}. Accept?(y/n)");
        }


        public class LocationData
        {
            public string[] locations { get; set; }
            public int[][] distances { get; set; }
        }
    }
}
