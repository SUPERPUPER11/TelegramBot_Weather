using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot_Weather
{
    public class Telegrambot
    {
        public bool ok { get; set; }
        public Result[] Result { get; set; }
    }

    public class Result
    {
        [JsonProperty("update_id")]
        public int Updateid { get; set; }
        public Message Message { get; set; }
    }

    public class Message
    {
        public int message_id { get; set; }
        public From From { get; set; }
        public Chat Chat { get; set; }
        public int date { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        public Entity[] entities { get; set; }
    }

    public class From
    {
        public int id { get; set; }
        public bool is_bot { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string language_code { get; set; }
    }

    public class Chat
    {
        public int Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string type { get; set; }
    }

    public class Entity
    {
        public int offset { get; set; }
        public int length { get; set; }
        public string type { get; set; }
    }

}
