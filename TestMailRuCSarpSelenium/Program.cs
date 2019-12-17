using System;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string login = args[0];
            string password = args[1];

            AuthenticationTest test = new AuthenticationTest("Firefox", "https://mail.ru/");
            Console.WriteLine("Тест LoginWithValidCredentialsAndLoginOff - {0}", test.LoginWithValidCredentialsAndLoginOff(login, password) ? "прошел" : "не прошел");
            Console.WriteLine("Тест LoginWithoutValidCredentials - {0}", test.LoginWithoutValidCredentials("login", "password") ? "прошел" : "не прошел");

            Console.ReadKey();
        }
    }
}