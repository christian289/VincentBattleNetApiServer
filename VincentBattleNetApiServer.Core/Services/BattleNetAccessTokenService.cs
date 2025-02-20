using VincentBattleNetApiServer.Core.Options;
using VincentBattleNetApiServer.Core.ServiceInterfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace VincentBattleNetApiServer.Core.Services;

public class BattleNetAccessTokenService(
    HttpClient httpClient,
    ICacheService cacheService,
    IOptionsMonitor<BattleNetAuth> option) : AbstractService(httpClient, cacheService), IBattleNetAccessTokenService
{
    public BattleNetAuth BattleNetAuth => option.CurrentValue;

    public async Task<string> GetTokenAsync(bool sub_token_use = false)
    {
        string? token = sub_token_use ? CacheService.GetCachedValue(Consts.BattleNetSubTokenKey) : CacheService.GetCachedValue(Consts.BattleNetAccessTokenKey);

        if (!string.IsNullOrWhiteSpace(token))
            return token;

        var clientId = BattleNetAuth.ClientId;
        var clientSecret = BattleNetAuth.ClientSecret;

        var tokenResponse = await HttpClient.PostAsync("https://oauth.battle.net/token",
            new FormUrlEncodedContent([
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
            ]));

        // ASP.NET Core NativeAOT 에서는 Json객체로 바로 변환할 수 없고, 문자열로 받은 후 Source Generator를 이용해서 아래 형태로 변환할 수 있음.
        // https://learn.microsoft.com/ko-kr/dotnet/standard/serialization/system-text-json/source-generation
        var tokenData_str = await tokenResponse.Content.ReadAsStringAsync();
        var tokenData = JsonSerializer.Deserialize(tokenData_str, SourceGenerationContext.Default.BattlenetAccessToken);

        var access_token = CacheService.SetCachingValue(
            key: Consts.BattleNetAccessTokenKey,
            value: tokenData!.AccessToken!,
            expiration: TimeSpan.FromHours(24)); // Battle API의 Access Token은 24시간동안 유효함

        var sub_access_token = CacheService.SetCachingValue(
            key: Consts.BattleNetSubTokenKey,
            value: tokenData!.Sub!,
            expiration: TimeSpan.FromHours(24)); // Battle API의 Access Token은 24시간동안 유효함

        return sub_token_use ? sub_access_token : access_token;
    }
}