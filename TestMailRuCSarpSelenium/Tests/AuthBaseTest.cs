namespace UnitTest
{
    public class AuthBaseTest : BaseTest
    {
        public AuthBaseTest(string brauser, string url) : base(brauser, url)
        {
        }

        public void SetupLogin()
        {
            application.Authentication.Login(new AccountData("login", "password"));
        }
    }
}
