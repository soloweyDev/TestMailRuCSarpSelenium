using System;
using System.Threading;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            try
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
                driver.FindElement(By.Id("mailbox:submit")).Click();

                driver.FindElement(By.Id("mailbox:password")).Clear();
                driver.FindElement(By.Id("mailbox:password")).SendKeys(account.Password);
                driver.FindElement(By.Id("mailbox:submit")).Click();
                Thread.Sleep(3000);

                Assert.AreEqual(driver.FindElement(By.Id("PH_user-email")).Text, "csharpselenium@mail.ru");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return false;
        }

        public bool LoginFrame(AccountData account)
        {
            try
            {
                if (IsLogedIn())
                {
                    if (IsLogedIn(account))
                    {
                        driver.FindElement(By.Id("PH_logoutLink")).Click();
                    }
                }

                driver.FindElement(By.Id("PH_authLink")).Click();
                Thread.Sleep(2000);
                var frames = driver.FindElements(By.TagName("iframe"));
                driver.SwitchTo().Frame(frames.Count - 1);
                driver.FindElement(By.Name("Login")).Clear();
                driver.FindElement(By.Name("Login")).SendKeys(account.Login);
                driver.FindElement(By.XPath("//html//body//div[1]//div[3]//div//div[3]//div//div[2]//div//form//div[2]//div[2]//div[3]//div//div[1]//button")).Click();

                driver.FindElement(By.Name("Password")).Clear();
                driver.FindElement(By.Name("Password")).SendKeys(account.Password);
                driver.FindElement(By.XPath("//html//body//div[1]//div[3]//div//div[3]//div//div[2]//div//form//div[2]//div//div[3]//div//div[1]/div//button")).Click();
                Thread.Sleep(3000);

                driver.SwitchTo().Frame(0);
                string s = driver.FindElement(By.Id("PH_user-email")).Text;
                Assert.AreEqual(s, "csharpselenium@mail.ru");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return false;
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
