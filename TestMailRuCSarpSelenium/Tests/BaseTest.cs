namespace UnitTest
{
    public class BaseTest
    {
        protected ApplicationManager application;

        public BaseTest(string brauser, string url)
        {
            application = ApplicationManager.GetInstance(brauser, url);
        }
    }
}