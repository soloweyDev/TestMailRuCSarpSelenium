using System;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //XmlSetup.WriteXml();

            string login = args[0];
            string password = args[1];

            AuthenticationTest test = new AuthenticationTest("Firefox", "https://mail.ru/");
            Console.WriteLine("Тесты:");
            Console.Write("  LoginWithValidCredentialsAndLoginOff - ");
            ViewTestResult(test.LoginWithValidCredentialsAndLoginOff(login, password));
            Console.Write("  LoginWithoutValidCredentials - ");
            ViewTestResult(test.LoginWithoutValidCredentials("login", "password"));

            Console.ReadKey();
        }

        static private void ViewTestResult(bool res)
        {
            ConsoleColor color = Console.ForegroundColor;

            if (res)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("прошел");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("не прошел");
            }

            Console.ForegroundColor = color;
        }
    }
}