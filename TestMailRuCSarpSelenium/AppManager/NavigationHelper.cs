using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace UnitTest
{
    public class NavigationHelper : BaseHelper
    {
        private readonly string baseURL;

        public NavigationHelper(ApplicationManager manager) : base(manager)
        {
            baseURL = manager.BaseURL;
        }

        public void OpenHomePage()
        {
            if (manager.Driver.Url == baseURL) return;

            manager.Driver.Navigate().GoToUrl(baseURL);
        }

        public bool WaitElement(By by, int second = 3)
        {
            bool res = false;
            try
            {
                res = new WebDriverWait(manager.Driver, TimeSpan.FromSeconds(second)).Until(d => d.FindElements(by).Count > 0 && d.FindElement(by).Displayed == true);
            }
            catch
            {
                manager.Log.Write($"Элемент {by.ToString()} не найден");
            }
            return res;
        }
    }
}
