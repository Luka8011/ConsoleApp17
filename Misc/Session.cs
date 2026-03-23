using ConsoleApp17.Money;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    class Session
    {
        public static MoneyManager CurrentUserMoney { get; set; }
        public static UserHandler UserHandler { get; set; }
    }
}
