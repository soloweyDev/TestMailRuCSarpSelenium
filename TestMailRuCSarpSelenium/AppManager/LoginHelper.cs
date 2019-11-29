using System;
using OpenQA.Selenium;

namespace UnitTest
{
    public class LoginHelper : BaseHelper
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        { }

        public bool IsLogedIn()
        {
            if (!manager.Navigation.WaitElementById("PH_authLink"))
                return false;

            return IsElementPresent(By.Id("PH_authLink"));
        }

        public bool IsLogedIn(AccountData account)
        {
            return IsLogedIn() && driver.FindElement(By.Id("PH_user-email")).Text == (account.Login.ToLower() + "@mail.ru");
        }

        public bool Login(AccountData account)
        {
            if (IsLogedIn())
            {
                if (IsLogedIn(account))
                {
                    driver.FindElement(By.Id("PH_logoutLink")).Click();
                }
            }

            driver.FindElement(By.Id("mailbox:login")).Clear();
            driver.FindElement(By.Id("mailbox:login")).SendKeys(account.Login);
            driver.FindElement(By.Id("mailbox:password")).Clear();
            driver.FindElement(By.Id("mailbox:password")).SendKeys(account.Password);
            driver.FindElement(By.Id("mailbox:submit")).Click();

            return true;
        }

        public bool LoginFrame(AccountData account)
        {
            if (IsLogedIn())
            {
                if (IsLogedIn(account))
                {
                    driver.FindElement(By.Id("PH_logoutLink")).Click();
                }
            }

            driver.FindElement(By.Id("PH_authLink")).Click();
            driver.SwitchTo().Frame(0);
            driver.FindElement(By.Name("Login")).Clear();
            driver.FindElement(By.Name("Login")).SendKeys(account.Login);
            driver.FindElement(By.Name("Password")).Clear();
            driver.FindElement(By.Name("Password")).SendKeys(account.Password);

            return true;
        }

        public bool Logoff() // тут нужно еще подумать
        {
            Console.WriteLine("Проверяем залогинены ли мы.");
            if (IsLogedIn())
            {
                Console.WriteLine("Мы залогинены. Будем пытаться разлогинится.");
                driver.FindElement(By.Id("PH_logoutLink")).Click();
                return manager.Navigation.WaitElementById("PH_authLink") ? true : false;
            }
            Console.WriteLine("Мы не залогинены.");
            return false;
        }
    }
}
