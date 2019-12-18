using OpenQA.Selenium;
using System;
using System.Threading;

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

        public bool WaitElementByName(string name)
        {
            //return new WebDriverWait(driver, TimeSpan.FromSeconds(second)).Until(d => d.FindElements(By.Name(name)).Count > 0);
            int time = 300;
            while (manager.FindElements(By.Name(name)).Count == 0)
            {
                Thread.Sleep(time);
                time += 300;
                Console.WriteLine(time);
                if (time >= 3000)
                {
                    return false;
                }
            }

            return true;
        }

        public bool WaitElementById(string name)
        {
            //return new WebDriverWait(driver, TimeSpan.FromSeconds(second)).Until(d => d.FindElements(By.Name(name)).Count > 0);
            int time = 300;
            while (manager.FindElements(By.Id(name)).Count == 0)
            {
                Thread.Sleep(time);
                time += 300;
                Console.WriteLine(time);
                if (time >= 3000)
                {
                    return false;
                }
            }

            return true;
        }

        public bool WaitElementByXpath(string name)
        {
            int time = 300;
            while (manager.FindElements(By.XPath(name)).Count == 0)
            {
                Thread.Sleep(time);
                time += 300;
                Console.WriteLine(time);
                if (time >= 3000)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
