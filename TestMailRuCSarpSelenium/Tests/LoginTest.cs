using System;
using OpenQA.Selenium;

namespace UnitTest
{
    class LoginTest : BaseTest
    {
        public LoginTest(string brauser, string url) : base(brauser, url)
        {
        }

        public bool LoginWithValidCredentials(string login, string password)
        {
            AccountData account = new AccountData(login, password);
            application.Authentication.Login(account);

            return true;
        }
    }
}
