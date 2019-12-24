using System;
using System.IO;
using System.Xml.Serialization;

namespace UnitTest
{
    [Serializable]
    public class XmlSetup
    {
        public bool Maximize { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Path { get; set; }

        public XmlSetup()
        {
            Maximize = false;
            X = 0;
            Y = 0;
            Height = 0;
            Width = 0;
            Path = "";
        }

        public static XmlSetup ReadXml()
        {
            XmlSetup xmlSetup = null;

            try
            {
                using (StreamReader sr = new StreamReader("setup.xml"))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(XmlSetup));
                    xmlSetup = xml.Deserialize(sr) as XmlSetup;
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Файл setup.xml не найден. Ошибка: {ex}");
            }

            return xmlSetup;
        }

        public static void WriteXml()
        {
            XmlSetup xmlSetup = new XmlSetup();
            using (StreamWriter sw = new StreamWriter("setup.xml"))
            {
                XmlSerializer xml = new XmlSerializer(typeof(XmlSetup));
                xml.Serialize(sw, xmlSetup);
            }
        }
    }
}
