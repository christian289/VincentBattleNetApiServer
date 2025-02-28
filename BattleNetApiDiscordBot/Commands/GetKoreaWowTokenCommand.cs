using VincentBattleNetApiServer.Core.ServiceInterfaces;

namespace BattleNetApiDiscordBot.Commands;

internal class GetKoreaWowTokenCommand(ICacheService cache, IBattleNetAccessTokenService serv)
{
    private readonly ICacheService _cache = cache;
    private readonly IBattleNetAccessTokenService _serv = serv;

    public SlashCommandProperties ReturnCommand()
    {
        var command = new SlashCommandBuilder()
            .WithName("토큰")
            .WithDescription("현재 한국 와우의 토큰 골드가격을 확인합니다.")
            .Build();

        return command;
    }

    public void ReturnCommandHandler()
    {

    }
}
