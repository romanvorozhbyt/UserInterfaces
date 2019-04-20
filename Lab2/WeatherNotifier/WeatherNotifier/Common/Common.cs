using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherNotifier.Common
{
    
    public class Common
    {
        public static string API_LINK = "http://api.openweathermap.org/data/2.5/";
        public static string API_KEY = "167045056eb54ffd39f9f48d8e47d02c";

        public static string APIWeatherRequest(string cityName, string counryCode, string units)
        {
            var requst = new StringBuilder(API_LINK);
            requst.Append($"weather?q={cityName},{counryCode}&APPID={API_KEY}&units={units}");
            return requst.ToString();
        }

        public static string APIWeatherRequest(double lat, double lon, string units)
        {
            var requst = new StringBuilder(API_LINK);
            requst.Append($"weather?at={lat}&lon={lon}&APPID={API_KEY}&units={units}");
            return requst.ToString();
        }

        public static string APIForecastRequest(double lat, double lon, string units)
        {
            var requst = new StringBuilder(API_LINK);
            requst.Append($"forecast?lat={lat}&lon={lon}&APPID={API_KEY}&units={units}");
            return requst.ToString();
        }

        public static string APIForecastRequest(string cityName, string counryCode, string units)
        {
            var requst = new StringBuilder(API_LINK);
            requst.Append($"forecast?q={cityName},{counryCode}&APPID={API_KEY}&units={units}");
            return requst.ToString();
        }

    }
}
