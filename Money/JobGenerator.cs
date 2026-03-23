namespace ConsoleApp17.Money
{
    class JobGenerator
    {
        #region Properties
        private int Pay { get; set; }
        private int Location { get; set; }
        private int Location2 { get; set; }
        private LocationData data;
        Random Rand = new Random();

        UserHandler.UserData UserData { get; set; }
        #endregion

        #region ctors
        public JobGenerator()
        {
            Loader locationLoader = new Loader("Locations");

            data = locationLoader.Load<LocationData>();
        }
        #endregion

        #region Methods
        public Job Generate(int input)
        {
            int pay = 0;
            switch (input)
            {
                case 1: pay = Rand.Next(2000, 3500); break; 
                case 2: pay = Rand.Next(3500, 5000); break;
                case 3: pay = Rand.Next(5000, 6000); break;
            }

            int loc1 = Rand.Next(0, data.locations.Length);
            int loc2 = Rand.Next(0, data.locations.Length);

            while (loc2 == loc1)
            {
                loc2 = Rand.Next(0, data.locations.Length);
            }

            var job = new Job
            {
                Pay = pay,
                From = data.locations[loc1],
                To = data.locations[loc2],
                Distance = data.distances[loc2][loc1]
            };

            Console.WriteLine($"Job: from {job.From} to {job.To} Distance: {job.Distance}, Pay: {job.Pay}. Accept?(y/n)");
            string accept = Console.ReadLine();

            if (accept.ToLower() == "y")
            {
                return job;
            }
            return null;

        }
        #endregion

        #region NestedClasses
        public class LocationData
        {
            public string[] locations { get; set; }
            public int[][] distances { get; set; }
        }
        public class Job
        {
            public string From { get; set; }
            public string To { get; set; }
            public int Distance { get; set; }
            public int Pay { get; set; }
            public bool IsAccepted { get; set; }
            public bool IsCompleted { get; set; }
        }
        #endregion
    }
}
