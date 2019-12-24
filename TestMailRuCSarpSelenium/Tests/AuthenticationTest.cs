namespace UnitTest
{
    public class AuthenticationTest : BaseTest
    {
        public AuthenticationTest(string brauser, string url) : base(brauser, url)
        {
            application.Log.Write("Старт набора тестов AuthenticationTest");
        }

        public bool LoginWithValidCredentialsAndLoginOff(string login, string password)
        {
            AccountData account = new AccountData(login, password);
            bool result = application.Authentication.Login(account) && application.Authentication.LoginFrame(account);
            application.Log.Write("Результат набора тестов LoginWithValidCredentialsAndLoginOff - " + (result ? "прошел" : "не прошел"));

            return result;
        }

        public bool LoginWithoutValidCredentials(string login, string password)
        {
            AccountData account = new AccountData(login, password);
            bool result = !application.Authentication.Login(account) && !application.Authentication.LoginFrame(account);
            application.Log.Write("Результат набора тестов LoginWithoutValidCredentials - " + (result ? "прошел" : "не прошел"));

            return result;
        }
    }
}
