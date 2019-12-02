using System;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string login = args[0];
            string password = args[1];

            LoginTest test = new LoginTest("Firefox", "https://mail.ru/");
            Console.WriteLine("Тест - {0}", test.LoginWithValidCredentials(login, password) ? "прошел" : "не прошел");

            Console.ReadKey();
        }
    }
}