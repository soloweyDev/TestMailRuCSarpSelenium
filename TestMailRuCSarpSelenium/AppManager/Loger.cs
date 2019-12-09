using System;
using System.IO;

namespace TestMailRuCSarpSelenium.AppManager
{
    public class Loger
    {
        private readonly string fileName;

        public Loger(string fileName)
        {
            this.fileName = fileName;
        }

        public void Write(string text = null)
        {
            using (var sw = new StreamWriter(fileName, true))
            {
                if (text != null)
                    sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {text}");
                else
                    sw.WriteLine();
            }
        }
    }
}
