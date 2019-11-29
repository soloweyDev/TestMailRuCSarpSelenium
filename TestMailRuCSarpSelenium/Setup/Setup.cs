using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;

namespace UnitTest
{
    class Setup
    {
        #region Настройки окружения
        private readonly string baseURL;
        //private readonly string pathFirefox = @"C:\Program Files\Mozilla Firefox\firefox.exe";
        private readonly string pathIE = @"C:\Program Files\Internet Explorer\iexplore.exe";
        private readonly string pathEdge = @"C:\Windows\SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdge.exe";
        private readonly string pathYandex = @"C:\Users\user\AppData\Local\Yandex\YandexBrowser\Application\browser.exe";
        //private readonly string pathChrome = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
        private readonly string pathOpera = @"C:\Users\user\AppData\Local\Programs\Opera\launcher.exe";
        private readonly string pathSafari = @"C:\Program Files\Safari\Safari.exe";
        #endregion

        public IWebDriver driver;
        //private static ChromeDriverService ChromeService;
        private static InternetExplorerDriverService IEService;
        private static EdgeDriverService EdgeService;

        public Setup(string url)
        {
            baseURL = url;
        }

        public string GetBaseURL()
        {
            return baseURL;
        }

        public IWebDriver InitSetupFirefox()
        {
            return new FirefoxDriver();
        }

        public IWebDriver InitSetupIE()
        {
            return new InternetExplorerDriver();
        }

        public IWebDriver InitSetupEdge()
        {
            EdgeService = EdgeDriverService.CreateDefaultService(pathEdge);
            driver = new EdgeDriver(EdgeService);

            return driver;
        }

        // своего нет, но движок от CHROME
        public IWebDriver InitSetupYandex()
        {
            ChromeOptions co = new ChromeOptions();
            co.BinaryLocation = pathYandex;

            return new ChromeDriver(co);
        }

        public IWebDriver InitSetupChrome()
        {
            return new ChromeDriver();
        }

        public IWebDriver InitSetupOpera()
        {
            OperaOptions oo = new OperaOptions();
            oo.BinaryLocation = pathOpera;

            return new OperaDriver(oo);
        }

        public IWebDriver InitSetupSafari()
        {
            return new SafariDriver(pathSafari);
        }
    }
}
