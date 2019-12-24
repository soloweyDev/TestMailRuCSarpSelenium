using System;
using System.Threading;
using OpenQA.Selenium;

namespace UnitTest
{
    public class LoginHelper : BaseHelper
    {
        By AuthLink = By.Id("PH_authLink");

        public LoginHelper(ApplicationManager manager) : base(manager)
        { }

        public bool IsLogedIn()
        {
            if (!manager.Navigation.WaitElement(AuthLink))
                return false;

            return IsElementPresent(AuthLink);
        }

        public bool IsLogedIn(AccountData account)
        {
            return IsLogedIn() && manager.FindElement(By.Id("PH_user-email")).Text == (account.Login.ToLower() + "@mail.ru");
        }

        public bool Login(AccountData account)
        {
            bool result = false;
            manager.Log.Write("Старт теста Login");

            try
            {
                if (IsLogedIn())
                {
                    if (IsLogedIn(account))
                    {
                        manager.Click(By.Id("PH_logoutLink"));
                    }
                }

                manager.InputText(By.Id("mailbox:login"), account.Login);
                manager.Click(By.Id("mailbox:submit"));

                manager.InputText(By.Id("mailbox:password"), account.Password);
                manager.Click(By.Id("mailbox:submit"));

                if (!manager.Navigation.WaitElement(By.XPath($"//i[@id='PH_user-email' and text() = '{account.Login.ToLower()}@mail.ru']"), 10))
                    return false;

                if (manager.FindElement(By.Id("PH_user-email")).Text == account.Login.ToLower() + "@mail.ru")
                    result = true;
            }
            catch (Exception ex)
            {
                manager.Log.Write($"Ошибка: {ex.Message}");
            }

            if (result)
            {
                result = Logoff();
            }

            string res = result ? "прошел" : "не прошел";
            manager.Log.Write($"Тест Login завершился с результатом - {res}");

            return result;
        }

        public bool LoginFrame(AccountData account)
        {
            bool result = false;
            manager.Log.Write("Старт теста LoginFrame");

            try
            {
                if (IsLogedIn())
                {
                    if (IsLogedIn(account))
                    {
                        manager.Click(By.Id("PH_logoutLink"));
                    }
                }

                manager.Click(AuthLink);
                var frames = manager.FindElements(By.TagName("iframe"));
                manager.SwitchTo(frames.Count - 1);
                manager.InputText(By.Name("Login"), account.Login);
                manager.Click(By.XPath("//html//body//div[1]//div[3]//div//div[3]//div//div[2]//div//form//div[2]//div[2]//div[3]//div//div[1]//button"));

                manager.InputText(By.Name("Password"), account.Password);
                manager.Click(By.XPath("//html//body//div[1]//div[3]//div//div[3]//div//div[2]//div//form//div[2]//div//div[3]//div//div[1]/div//button"));
                manager.Driver.SwitchTo().DefaultContent();

                if (!manager.Navigation.WaitElement(By.XPath($"//i[@id='PH_user-email' and text() = '{account.Login.ToLower()}@mail.ru']"), 10))
                    return false;

                if (manager.FindElement(By.Id("PH_user-email")).Text == account.Login.ToLower() + "@mail.ru")
                    result = true;
            }
            catch (Exception ex)
            {
                manager.Log.Write($"Ошибка: {ex.Message}");
            }

            if (result)
            {
                result = Logoff();
            }

            string res = result ? "прошел" : "не прошел";
            manager.Log.Write($"Тест LoginFrame завершился с результатом - {res}");

            return result;
        }

        public bool Logoff()
        {
            manager.Log.Write("Проверяем залогинены ли мы.");
            if (!IsLogedIn())
            {
                manager.Log.Write("Мы залогинены. Будем пытаться разлогинится.");
                manager.Click(By.Id("PH_logoutLink"));
                if (manager.Navigation.WaitElement(AuthLink))
                {
                    manager.Click(By.LinkText("Mail.ru"));
                    return true;
                }
                else
                    return false;
            }
            manager.Log.Write("Мы не залогинены.");
            return false;
        }
    }
}
