namespace UnitTest
{
    class LoginTest : BaseTest
    {
        public LoginTest(string brauser, string url) : base(brauser, url)
        {
            application.Log.Write("Старт набора тестов LoginTest");
        }

        public bool LoginWithValidCredentials(string login, string password)
        {
            AccountData account = new AccountData(login, password);
            bool result = application.Authentication.Login(account) && application.Authentication.LoginFrame(account);
            application.Log.Write("Результат набора тестов LoginTest - " + (result ? "прошел" : "не прошел"));

            return result;
        }
    }
}
