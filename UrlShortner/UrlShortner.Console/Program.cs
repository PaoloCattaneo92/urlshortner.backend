using PaoloCattaneo.UrlShortner.Core.BL;
using PaoloCattaneo.UrlShortner.Core.DAL;
using PaoloCattaneo.UrlShortner.Core.Model;
using System;

namespace UrlShortner.Console
{
    internal class Program
    {
        private static Shortner shortner;

        private static void Log(string message)
        {
            System.Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] - {message}");
        }

        public static void Main(string[] args)
        {
            shortner = new Shortner(
                new ShortnerOptionsBuilder()
                    .WithAlphabet("ABCDEFGHIJKLMNOPRQSTUVWXYZ0123456789")
                //.WithPadLeft('0', 6)
                .Build()
                );

            //TestSomeConvertions();

            
            var dal = new MySqlUrlRepository();
            var url = "https://www.google.com/";
            var urlShort = dal.Insert(url);
            dal.Update(urlShort.Id, urlShort.Key);
            var y = dal.Get(urlShort.Id);

            System.Console.ReadLine();
        }

        private static void TestSomeConvertions()
        {
            Log("0->100:");
            for (var i = 0; i < 100; i++)
            {
                Test(i);
            }

            Log("50k:");
            Test(50000);
            Log("10M:");
            Test(1000000);
            Log("2B ca:");
            Test(int.MaxValue);
        }

        private static void Test(int i)
        {
            var encoded = shortner.Encode(i);
            var decoded = shortner.Decode(encoded);
            Log($"{i}\t{encoded}\t{decoded}");
        }
    }
}
