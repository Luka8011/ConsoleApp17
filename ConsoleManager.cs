using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    class ConsoleManager
    {
        public void ShowCommands()
        {

        }

        public void ShowBank()
        {

        }
        public void ShowAllTrucks(List<TruckData> trucks)
        {

        }

        public void SeeTruck(List<TruckData> trucks, int id)
        {
            var truck = trucks.FirstOrDefault(x => x.Id == id);
        }
    }
}
