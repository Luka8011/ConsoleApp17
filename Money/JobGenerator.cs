namespace ConsoleApp17.Money
{
    class JobGenerator
    {
        private int Pay { get; set; }
        private int Location { get; set; }
        private int Location2 { get; set; }
        private LocationData data;
        Random Rand = new Random();

        public JobGenerator()
        {
            Loader locationLoader = new Loader("Locations");

            data = locationLoader.Load<LocationData>();
        }

        public void Generate(int input)
        {
            switch (input)
            {
                case 1:
                    Pay = Rand.Next(2000, 3500);

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
                    break;
                case 2:
                    Pay = Rand.Next(3500, 5000);

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
                    break;
                case 3:
                    Pay = Rand.Next(5000, 6000);

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
                    break;
            }
        }

        public class LocationData
        {
            public string[] locations { get; set; }
            public int[][] distances { get; set; }
        }

    }
}
