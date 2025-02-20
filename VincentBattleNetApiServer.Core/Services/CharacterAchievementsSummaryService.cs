using VincentBattleNetApiServer.Core.DTOs.Res.CharacterAchievementsAPI;
using VincentBattleNetApiServer.Core.ServiceInterfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace VincentBattleNetApiServer.Core.Services;

public class CharacterAchievementsSummaryService(
    HttpClient httpClient,
    ICacheService cacheService,
    IBattleNetAccessTokenService accessTokenService) : AbstractService(httpClient, cacheService), ICharacterAchievementsSummaryService
{
    private readonly IBattleNetAccessTokenService accessTokenService = accessTokenService;

    public async Task<ResCharacterAchievementsSummary> GetResCharacterAchievementsSummaryAsync(string realmSlug, string characterName)
    {
        var accessToken = await accessTokenService.GetTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        UriBuilder uriBuilder = new($"https://{Consts.Region}.api.blizzard.com{string.Format(Endpoints.CharacterAchievementsSummary, realmSlug, characterName)}")
        {
            Query = new QueryBuilder
            {
                { "namespace", Consts.ProfileNamespace },
                { "locale", Consts.Locale }
            }.ToQueryString().Value
        };
        var gameDataResponse = await HttpClient.GetAsync(uriBuilder.Uri);
        gameDataResponse.EnsureSuccessStatusCode();
        var characterachievementssummary_str = await gameDataResponse.Content.ReadAsStringAsync();
        var characterachievementssummary = JsonSerializer.Deserialize(characterachievementssummary_str, SourceGenerationContext.Default.ResCharacterAchievementsSummary);

        return characterachievementssummary!; // Index 파일들이나 Cache에 저장하지, 특정데이터는 저장하지 않는다.
    }
}
