using Discord;
using Discord.WebSocket;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordWeatherBot
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            // Създаване на инстанция на WeatherService с HttpClient
            var weatherService = new WeatherService(new HttpClient());

            // Създаване на инстанция на WeatherBot с WeatherService
            var bot = new WeatherBot(weatherService);

            // Стартиране на бота
            await bot.RunAsync();
        }
    }
}