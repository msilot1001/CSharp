using System;
using System.Configuration;
using System.Collections.Specialized;
using Discord;
using Discord.WebSocket;

namespace DiscordBot2
{
	class Program
	{
        private readonly DiscordSocketClient _client;
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public Program()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;
            _client.Ready += Ready;
            _client.MessageReceived += MessageReceivedAsync;
        }

        public async Task MainAsync()
        {
            await _client.LoginAsync(TokenType.Bot, "토큰");
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task Ready()
        {
            Console.WriteLine($"{_client.CurrentUser} 연결됨!");

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(SocketMessage message)
        {
            if (message.Author.Id == _client.CurrentUser.Id)
                return;

            if (message.Content == "!ping")
                await message.Channel.SendMessageAsync("pong!");
        }
    }
}