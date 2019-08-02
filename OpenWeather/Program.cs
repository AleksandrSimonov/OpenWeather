using System;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace OpenWeather
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string url = "http://api.openweathermap.org/data/2.5/weather";
            string appiKey = "f3fd8ad4e923868dde7e77657ce28b19";

            Console.Write("Enter city name: ");
            string cityName = Console.ReadLine();
            Console.WriteLine();

            WebRequest req = WebRequest.Create(url + "?q=" + cityName+"&appid="+appiKey);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sreader = new StreamReader(stream);
            string Out = sreader.ReadToEnd();
            sreader.Close();

            string temperature = "\"temp\":(.{6})";
            Regex regex = new Regex(temperature);
            Match match = regex.Match(Out);
            string t ="Temperature: "+(Convert.ToDouble(match.Groups[1].Value)-273)+ "°C";

            string humidity = "\"humidity\":(.{2})";
            regex = new Regex(humidity);
            match = regex.Match(Out);
            string h = "Humidity: "+ Convert.ToDouble(match.Groups[1].Value)+"%";

            string sunset = "\"sunset\":(.{10})";
            regex = new Regex(sunset);
            match = regex.Match(Out);
            string ss = "Sunset: "+ new DateTime(1970, 1, 1).AddSeconds(Convert.ToDouble(match.Groups[1].Value)).TimeOfDay;

            string sunrise = "\"sunrise\":(.{10})";
            regex = new Regex(sunrise);
            match = regex.Match(Out);
            string sr = "Sunrise: " + new DateTime(1970, 1, 1).AddSeconds(Convert.ToDouble(match.Groups[1].Value)).TimeOfDay;

            Console.WriteLine(t);
            Console.WriteLine(h);
            Console.WriteLine(ss);
            Console.WriteLine(sr);

            string fileName = DateTime.Now.Day+"."+ DateTime.Now.Month+"."+ DateTime.Now.Year+".txt";
                using (StreamWriter file = new StreamWriter(fileName, false, Encoding.Default))
                {
                    file.WriteLine(t);
                    file.WriteLine(h);
                    file.WriteLine(ss);
                    file.WriteLine(sr);
                }
            
            Console.ReadKey();
        }
        
    }
}
