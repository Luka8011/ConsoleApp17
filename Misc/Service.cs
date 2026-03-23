using ConsoleApp17.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp17
{
    class Service
    {
        public UserHandler CurrentUser {get; set;}
        public UserHandler.UserData CurrentUserData { get; set; }
        public UserHandler.UserData Register(string name, string email, string password)
        {
            var existingUser = UserHandler.LoadUser(name);
            if (existingUser != null)
            {
                Console.WriteLine("User exists already");
                return null;
            }

            UserHandler.CreateUser(name, email, password);
            CurrentUserData = UserHandler.LoadUser(name);
            CurrentUser = new UserHandler(name);
            Session.UserHandler = CurrentUser;

            return CurrentUserData;
        }

        public UserHandler.UserData Login(string name, string password)
        {
            var existingUser = UserHandler.LoadUser(name);
            if (existingUser == null)
            {
                Console.WriteLine("User Doesn't Exist");
                return null;
            }
            if (existingUser.Password != password)
            {
                Console.WriteLine("Wrong password");
            }

            CurrentUserData = UserHandler.LoadUser(name);
            CurrentUser = new UserHandler(name);
            Session.UserHandler = CurrentUser;

            return CurrentUserData;
        }
    }
}
