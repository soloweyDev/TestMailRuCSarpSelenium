namespace UnitTest
{
    public class AccountData
    {
        public string Login { get; internal set; }
        public string Password { get; internal set; }

        public AccountData(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
