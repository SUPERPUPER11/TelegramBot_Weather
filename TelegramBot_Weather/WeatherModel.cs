using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot_Weather
{
    public class WeatherModel
    {
        public Coord coord { get; set; }
        public Weather[] Weather { get; set; }
        public string _base { get; set; }
        [JsonProperty("main")]
        public Main Main { get; set; }
        public int visibility { get; set; }
        public Wind Wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        public int cod { get; set; }

    }
    public class Coord
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }
        [JsonProperty("feels_like")]
        public float FeelsLike { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
        [JsonProperty("pressure")]
        public int Pressure { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
        public int sea_level { get; set; }
        public int grnd_level { get; set; }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public float Speed { get; set; }
        [JsonProperty("deg")]
        public int Deg { get; set; }
        public float gust { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        public string icon { get; set; }
    }
}
