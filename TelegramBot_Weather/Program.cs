using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Reflection;
using TelegramBot_Weather;

var apiKey = "de652bea5fc39e262e384811c44b47a7";
var botToken = "5835974795:AAEIeohzoPCbBWS4uJkqi8ATXIdinC0IQhc";
var offset = 0;
var client = new HttpClient();

while (true)
{
    var response = await client.GetAsync($"https://api.telegram.org/bot{botToken}/getUpdates?offset={offset}");
    
    if (response.IsSuccessStatusCode)
    {
        var modelTelegram = JsonConvert.DeserializeObject<Telegrambot>(response.Content.ReadAsStringAsync().Result);
        foreach (var model in modelTelegram.Result)
        {
            var message = model.Message.Text;
            var chatId = model.Message.Chat.Id;
            var textMessage = string.Empty;

            if (message == "/start")
            {
                textMessage = $"Приветствую {model.Message.From.FirstName}"  + "\nЧтобы узнать погоду наберите /weather.";

                var pushMessage = await client.GetAsync($"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={textMessage}");
            }

            else
            {
                if (message == "/weather")
                {
                    textMessage = "Напишите название населенного пункта.";

                    var pushMessage = await client.GetAsync($"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={textMessage}");
                }
                else
                {
                    var responseWeather = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={message}&appid={apiKey}&lang=ru&units=metric");
                    if (responseWeather.IsSuccessStatusCode)
                    {
                        var resultWeather = await responseWeather.Content.ReadAsStringAsync();
                        var modelWeather = JsonConvert.DeserializeObject<WeatherModel>(resultWeather);

                        textMessage = $"Город: {modelWeather.Name}"
                                 + $"\nТемпература: {Math.Round(modelWeather.Main.Temp)}°"
                                 + $"\nНа улице {modelWeather.Weather[0].Description}"
                                 + $"\nОщущается как: {modelWeather.Main.FeelsLike}°"
                                 + $"\nСкорость ветра: {modelWeather.Wind.Speed} м/с, {Cardinaldirections(modelWeather.Wind.Deg)}"
                                 + $"\nВлажность: {modelWeather.Main.Humidity}%"
                                 + $"\nДавление: {modelWeather.Main.Pressure} мм рт.ст.";

                        var pushMessage = await client.GetAsync($"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={textMessage}");
                    }
                    else
                    {
                        if (message == "Спасибо")
                        {
                            textMessage = $"Не за что {model.Message.From.FirstName})";

                            var pushMessage = await client.GetAsync($"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={textMessage}");
                        }
                        else
                        {
                            textMessage = "По вашему запросу ничего не нашлось(";
                            var pushMessage = await client.GetAsync($"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={textMessage}");
                        }
                    }
                }
            }
        }
        if (modelTelegram.Result.Length > 0)
        {
            offset = modelTelegram.Result[^1].Updateid + 1;
        }

    }
    string Cardinaldirections(int wet) =>
            wet switch
            {
                >= 0 and < 23 or >= 338 and <= 360 => "C",
                >= 23 and < 68 => "СВ",
                >= 68 and < 113 => "В",
                >= 113 and < 158 => "ЮВ",
                >= 158 and < 203 => "Ю",
                >= 203 and < 248 => "ЮЗ",
                >= 248 and < 292 => "З",
                >= 292 and < 338 => "CЗ",
                _ => "",
            };
}


