using OpenQA.Selenium;

namespace UnitTest
{
    public class BaseHelper
    {
        protected ApplicationManager manager;
        protected IWebDriver driver;

        public BaseHelper(ApplicationManager manager)
        {
            this.manager = manager;
            driver = manager.Driver;
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

    }
}