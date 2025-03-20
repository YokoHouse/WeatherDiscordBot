using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace DiscordWeatherBot
{
    public class WeatherBot
    {
        private readonly DiscordSocketClient _client;
        private readonly WeatherService _weatherService;

        public WeatherBot(WeatherService weatherService)
        {
            
            _client = new DiscordSocketClient();
            _weatherService = weatherService;
            
            _client.Log += LogAsync;
            _client.Ready += OnReady;
        }

        public async Task RunAsync()
        {
            
            await _client.LoginAsync(TokenType.Bot, "DISCORD BOT TOKEN");
            
            await _client.StartAsync();
            
            await Task.Delay(-1);
        }
        
        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log);
            return Task.CompletedTask;
        }
        
        private async Task OnReady()
        {
            Console.WriteLine("Ботът е онлайн!");
            
            var weatherData = await _weatherService.GetWeatherAsync();

            if (weatherData != null)
            {
                Console.WriteLine($"Температура: {weatherData.Current.TempC}°C");
                Console.WriteLine($"Усеща се като: {weatherData.Current.FeelsLikeC}°C");
                Console.WriteLine($"Условия: {weatherData.Current.Condition.Text}");
            }
            else
            {
                Console.WriteLine("Не можа да се извлече информация за времето.");
            }

            
            var channel = _client.GetChannel(ulong.Parse("CHANNEL ID")) as IMessageChannel;
            if (channel != null)
            {
                await channel.SendMessageAsync($"Температура в София: {weatherData.Current.TempC}°C. Усеща се като: {weatherData.Current.FeelsLikeC}°C. {weatherData.Current.Condition.Text}");
            }
        }
    }
}
