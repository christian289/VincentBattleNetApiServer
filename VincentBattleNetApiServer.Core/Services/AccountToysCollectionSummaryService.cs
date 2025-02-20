using VincentBattleNetApiServer.Core.DTOs.Res.AccountProfileAPI;
using VincentBattleNetApiServer.Core.ServiceInterfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace VincentBattleNetApiServer.Core.Services;

public class AccountToysCollectionSummaryService(
    HttpClient httpClient,
    ICacheService cacheService,
    IBattleNetAccessTokenService accessTokenService) : AbstractService(httpClient, cacheService), IAccountToysCollectionSummaryService
{
    private readonly IBattleNetAccessTokenService accessTokenService = accessTokenService;

    public async Task<ResAccountToysCollectionSummary> GetResAccountToysCollectionSummaryAsync()
    {
        var accessToken = await accessTokenService.GetTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        UriBuilder uriBuilder = new($"https://{Consts.Region}.api.blizzard.comm{Endpoints.AccountToysCollectionSummary}")
        {
            Query = new QueryBuilder
            {
                { "namespace", Consts.ProfileNamespace },
                { "locale", Consts.Locale }
            }.ToQueryString().Value
        };
        var gameDataResponse = await HttpClient.GetAsync(uriBuilder.Uri);
        gameDataResponse.EnsureSuccessStatusCode();

        var accounttoyscollectionsummary_str = await gameDataResponse.Content.ReadAsStringAsync();
        var accounttoyscollectionsummary = JsonSerializer.Deserialize(accounttoyscollectionsummary_str, SourceGenerationContext.Default.ResAccountToysCollectionSummary);

        return CacheService.SetCachingValue(Consts.AccountToysCollectionSummary, accounttoyscollectionsummary!);
    }
}
