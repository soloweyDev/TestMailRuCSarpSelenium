using System;
using OpenQA.Selenium;

namespace UnitTest
{
    public class LoginHelper : BaseHelper
    {
        private readonly By AuthLink = By.Id("PH_authLink");
        private readonly By LogoutLink = By.Id("PH_logoutLink");
        private readonly By MailboxLogin = By.Id("mailbox:login");
        private readonly By MailboxSubmit = By.Id("mailbox:submit");
        private readonly By MailboxPassword = By.Id("mailbox:password");
        private readonly By UserMail = By.Id("PH_user-email");
        private readonly By Iframe = By.TagName("iframe");
        private readonly By IframeLogin = By.Name("Login");
        private readonly By IframePassword = By.Name("Password");
        private readonly By IframeSubmit = By.XPath("//html//body//div[1]//div[3]//div//div[3]//div//div[2]//div//form//div[2]//div[2]//div[3]//div//div[1]//button");
        private readonly By IframeSubmit2 = By.XPath("//html//body//div[1]//div[3]//div//div[3]//div//div[2]//div//form//div[2]//div//div[3]//div//div[1]/div//button");
        private readonly By MailLink = By.LinkText("Mail.ru");

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
            return IsLogedIn() && manager.FindElement(UserMail).Text == (account.Login.ToLower() + "@mail.ru");
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
                        manager.Click(LogoutLink);
                    }
                }

                manager.InputText(MailboxLogin, account.Login);
                manager.Click(MailboxSubmit);

                manager.InputText(MailboxPassword, account.Password);
                manager.Click(MailboxSubmit);

                if (!manager.Navigation.WaitElement(By.XPath($"//i[@id='PH_user-email' and text() = '{account.Login.ToLower()}@mail.ru']"), 10))
                    result = false;

                if (!result && manager.FindElement(UserMail).Text == account.Login.ToLower() + "@mail.ru")
                    result = true;
            }
            catch (Exception ex)
            {
                manager.Log.Write($"Ошибка: {ex.Message}");
            }

            Logoff();

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
                        manager.Click(LogoutLink);
                    }
                }

                manager.Click(AuthLink);
                var frames = manager.FindElements(Iframe);
                manager.SwitchTo(frames.Count - 1);
                manager.InputText(IframeLogin, account.Login);
                manager.Click(IframeSubmit);

                manager.InputText(IframePassword, account.Password);
                manager.Click(IframeSubmit2);
                manager.Driver.SwitchTo().DefaultContent();

                if (!manager.Navigation.WaitElement(By.XPath($"//i[@id='PH_user-email' and text() = '{account.Login.ToLower()}@mail.ru']"), 10))
                    result = false;

                if (!result && manager.FindElement(UserMail).Text == account.Login.ToLower() + "@mail.ru")
                    result = true;
            }
            catch (Exception ex)
            {
                manager.Log.Write($"Ошибка: {ex.Message}");
            }

            Logoff();

            string res = result ? "прошел" : "не прошел";
            manager.Log.Write($"Тест LoginFrame завершился с результатом - {res}");

            return result;
        }

        public bool Logoff()
        {
            bool result = false;
            manager.Log.Write("Проверяем залогинены ли мы.");
            if (!IsLogedIn())
            {
                manager.Log.Write("Мы залогинены. Будем пытаться разлогинится.");
                manager.Click(LogoutLink);
                if (manager.Navigation.WaitElement(AuthLink))
                    result = true;
            }
            if (!result) manager.Log.Write("Мы не залогинены.");

            OpenPage();
            return result;
        }

        private void OpenPage()
        {
            manager.Click(MailLink);
            manager.Navigation.WaitElement(MailboxLogin);
        }
    }
}
