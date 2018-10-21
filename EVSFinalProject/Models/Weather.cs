using System.Net;
using System.Web.Script.Serialization;

namespace EVSFinalProject.Models
{
    public class Weather
    {
        public object GetWeatherForcast()
        {


            string url = "http://api.openweathermap.org/data/2.5/weather?q=Lahore&APPID=1f630ef81159132ae0101b24851e5f6e&units=imperial";
            var client = new WebClient();
            var content = client.DownloadString(url);
            var serilizer = new JavaScriptSerializer();
            var jsoncontent = serilizer.Deserialize<object>(content);
            return jsoncontent;
        }
    }
}
