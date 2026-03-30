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

        #endregion

        #region ctors
        public JobGenerator()
        {
            Loader locationLoader = new Loader("Locations");

            data = locationLoader.Load<LocationData>();
        }
        #endregion

        #region Methods
        public Job Generate()
        {
            var user = Session.User;

            int pay = 0;
            switch (user.LicenseGrade)
            {
                case 1: pay = Rand.Next(2000, 3500); break;
                case 2: pay = Rand.Next(3500, 5000); break;
                case 3: pay = Rand.Next(5000, 6500); break;
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

            return job;

        }

        public int GetDistance(string from, string to)
        {
            int fromIndex = Array.IndexOf(data.locations, from);
            int toIndex = Array.IndexOf(data.locations, to);

            if (fromIndex == -1 || toIndex == -1)
            {
                return 0;
            }

            return data.distances[toIndex][fromIndex];
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
