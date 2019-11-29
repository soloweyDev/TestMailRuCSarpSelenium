using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace UnitTest
{
    public class ApplicationManager
    {
        private readonly Setup setup;
        private static ThreadLocal<ApplicationManager> instance = new ThreadLocal<ApplicationManager>();

        public string BaseURL { get; }
        public IWebDriver Driver { get; }
        public LoginHelper Authentication { get; }
        public NavigationHelper Navigation { get; }
        internal AdminHelper Admin { get; private set; }

        private ApplicationManager(string brauser, string url)
        {
            setup = new Setup(url);
            BaseURL = setup.GetBaseURL();
            switch (brauser)
            {
                case "Chrome":
                    Driver = setup.InitSetupChrome();
                    break;
                case "Firefox":
                    Driver = setup.InitSetupFirefox();
                    break;
                case "Opera":
                    Driver = setup.InitSetupOpera();
                    break;
                // не проверенные
                case "IE":
                    Driver = setup.InitSetupIE();
                    break;
                case "Yandex":
                    Driver = setup.InitSetupYandex();
                    break;
                case "Edge":
                    Driver = setup.InitSetupEdge();
                    break;
                case "Safari":
                    Driver = setup.InitSetupSafari();
                    break;
                default:
                    Driver = null;
                    break;
            }

            Authentication = new LoginHelper(this);
            Navigation = new NavigationHelper(this);
            Admin = new AdminHelper(this, BaseURL);
        }

        ~ApplicationManager()
        {
            try
            {
                Driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public static ApplicationManager GetInstance(string brauser, string url)
        {
            if (!instance.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager(brauser, url);
                newInstance.Navigation.OpenHomePage();
                instance.Value = newInstance;
            }

            return instance.Value;
        }

        public void InputText(string locator, string text)
        {
            if (text != null)
            {
                IWebElement element = Driver.FindElement(By.Name(locator));
                element.Clear();
                element.SendKeys(text);
            }
        }

        public void InputTextClick(string locator, string text)
        {
            if (text != null)
            {
                IWebElement element = Driver.FindElement(By.Name(locator));
                element.Clear();

                foreach (var c in text)
                {
                    element.Click();
                    element.SendKeys(c.ToString());
                }
            }
        }

        public void ClickByName(string text)
        {
            Driver.FindElement(By.Name(text)).Click();
        }

        public void ClickByXpath(string text)
        {
            Driver.FindElement(By.XPath(text)).Click();
        }

        public void TextDown(int x, int y, int steps)
        {
            Actions actions = new Actions(Driver);
            actions.MoveByOffset(x, y).Click();

            for (int i = 0; i < steps; ++i)
                actions.SendKeys(Keys.PageDown).Build().Perform();

            Thread.Sleep(300);
        }
    }
}
