using ConsoleApp17.Money;

namespace ConsoleApp17
{
    class Session
    {
        public static MoneyManager CurrentUserMoney { get; set; }
        public static UserHandler.UserData User { get; set; }
    }
}
