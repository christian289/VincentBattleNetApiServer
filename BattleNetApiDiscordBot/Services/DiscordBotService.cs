using BattleNetApiDiscordBot.Commands;
using VincentBattleNetApiServer.Core.ServiceInterfaces;

namespace BattleNetApiDiscordBot.Services;

internal class DiscordBotService : BackgroundService
{
    public DiscordBotService(
        IConfiguration configuration,
        ICacheService cache,
        GetKoreaWowTokenCommand tokenCommand)
    {
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            //GatewayIntents = GatewayIntents.GuildMessages | GatewayIntents.MessageContent | GatewayIntents.Guilds,
            LogLevel = LogSeverity.Info,
            MessageCacheSize = 100
        });
        _interaction = new InteractionService(_client);
        _configuration = configuration;
        _cache = cache;
        _tokenCommand = tokenCommand;
    }

    private readonly DiscordSocketClient _client;
    private readonly InteractionService _interaction;
    private readonly IConfiguration _configuration;
    private readonly ICacheService _cache;
    private readonly GetKoreaWowTokenCommand _tokenCommand;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _client.Ready += DiscordReadyAndStartCommandRegist;
        _client.InteractionCreated += InteractionCreated;
        _client.SlashCommandExecuted += SlashCommandHandler;
        _client.Log += LogAsync;
        await _client.LoginAsync(TokenType.Bot, _configuration["DiscordAppBotToken"]);
        await _client.StartAsync();
    }

    private async Task DiscordReadyAndStartCommandRegist()
    {
        foreach (SocketGuild? guild in _client.Guilds)
        {
            await _interaction.RegisterCommandsToGuildAsync(guild.Id, true);
            await guild.CreateApplicationCommandAsync(_tokenCommand.ReturnCommand());
        }
    }

    private async Task InteractionCreated(SocketInteraction interaction)
    {
        //var ctx = new SocketInteractionContext(_client, interaction);
        //await _interaction.ExecuteCommandAsync(ctx);
    }

    private async Task SlashCommandHandler(SocketSlashCommand command)
    {
        //var ctx = new SocketSlashCommandContext(_client, command);
        //if (ctx.CommandName == "토큰")
        //{
        //    _tokenCommand.ReturnCommandHandler();
        //}
    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString());
        return Task.CompletedTask;
    }
}
