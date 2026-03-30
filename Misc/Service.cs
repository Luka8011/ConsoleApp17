using ConsoleApp17;
using ConsoleApp17.Money;

class Service
{
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

        var user = UserHandler.LoadUser(name);

        CurrentUserData = user;
        Session.User = user;
        Session.CurrentUserMoney = new MoneyManager();

        return user;
    }

    public UserHandler.UserData Login(string name, string password)
    {
        var user = UserHandler.LoadUser(name);

        if (user == null)
        {
            Console.WriteLine("User doesn't exist");
            return null;
        }

        if (user.Password != password)
        {
            Console.WriteLine("Wrong password");
            return null;
        }

        CurrentUserData = user;
        Session.User = user;
        Session.CurrentUserMoney = new MoneyManager();

        return user;
    }
}