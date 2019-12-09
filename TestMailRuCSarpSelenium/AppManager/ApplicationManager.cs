using System;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using TestMailRuCSarpSelenium.AppManager;

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
        public Loger Log { get; }

        private ApplicationManager(string brauser, string url)
        {
            Log = new Loger($"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.log");

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
                Log.Write("Выход из теста");
                Driver.Quit();
                Log.Write();
            }
            catch (Exception e)
            {
                Log.Write($"Error: {e.Message}");
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

        public IWebElement FindElement(By by)
        {
            IWebElement element = null;
            Log.Write($"Ищем элемента {by.ToString()}");
            try
            {
                element = Driver.FindElement(by);
                Log.Write($"Элемент {by.ToString()} найден");
            }
            catch (Exception ex)
            {
                Log.Write($"ERROR:  {ex.Message}");
            }

            return element;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            ReadOnlyCollection<IWebElement> elements = null;
            Log.Write($"Ищем элемента {by.ToString()}");
            try
            {
                elements = Driver.FindElements(by);
            }
            catch (Exception ex)
            {
                Log.Write($"ERROR:  {ex.Message}");
            }

            return elements;
        }

        public void Click(By by)
        {
            Log.Write($"Клик элемента {by.ToString()}");
            try
            {
                FindElement(by).Click();
            }
            catch (Exception ex)
            {
                Log.Write($"ERROR:  {ex.Message}");
            }
        }

        public void InputText(By by, string text)
        {
            if (text != null)
            {
                Log.Write($"Очистка элемента {by.ToString()}");
                try
                {
                    FindElement(by).Clear();
                }
                catch (Exception ex)
                {
                    Log.Write($"ERROR:  {ex.Message}");
                }
                if (by.ToString().Contains("password") || by.ToString().Contains("Password")) Log.Write($"Ввод в {by.ToString()} строки *******");
                else Log.Write($"Ввод в {by.ToString()} строки {text}");
                try
                {
                    FindElement(by).SendKeys(text);
                }
                catch (Exception ex)
                {
                    Log.Write($"ERROR:  {ex.Message}");
                }
            }
        }

        public void SwitchTo(int i)
        {
            Driver.SwitchTo().Frame(i);
        }
    }
}
