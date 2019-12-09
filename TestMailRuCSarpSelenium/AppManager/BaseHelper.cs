using OpenQA.Selenium;

namespace UnitTest
{
    public class BaseHelper
    {
        protected ApplicationManager manager;

        public BaseHelper(ApplicationManager manager)
        {
            this.manager = manager;
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                manager.FindElement(by);
                manager.Log.Write($"Элемент {by.ToString()} найден");
                return true;
            }
            catch (NoSuchElementException ex)
            {
                manager.Log.Write($"ERROR:  {ex.Message}");
                return false;
            }
        }
    }
}