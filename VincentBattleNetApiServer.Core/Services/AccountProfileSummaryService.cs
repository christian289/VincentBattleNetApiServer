using VincentBattleNetApiServer.Core.DTOs.Res.AccountProfileAPI;
using VincentBattleNetApiServer.Core.ServiceInterfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace VincentBattleNetApiServer.Core.Services;

public class AccountProfileSummaryService(
    HttpClient httpClient,
    ICacheService cacheService,
    IBattleNetAccessTokenService accessTokenService) : AbstractService(httpClient, cacheService), IAccountProfileSummaryService
{
    private readonly IBattleNetAccessTokenService accessTokenService = accessTokenService;

    public async Task<ResAccountProfileSummary> GetResAccountProfileSummaryAsync()
    {
        var accessToken = await accessTokenService.GetTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        UriBuilder uriBuilder = new($"https://{Consts.Region}.api.blizzard.com{Endpoints.AccountProfileSummary}")
        {
            Query = new QueryBuilder
            {
                { "namespace", Consts.ProfileNamespace },
                { "locale", Consts.Locale }
            }.ToQueryString().Value
        };
        var gameDataResponse = await HttpClient.GetAsync(uriBuilder.Uri);
        gameDataResponse.EnsureSuccessStatusCode();

        //if (!gameDataResponse.IsSuccessStatusCode)
        //{
        //    accessToken = await AccessTokenService.GetTokenAsync(true);
        //    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        //    gameDataResponse = await HttpClient.GetAsync(uriBuilder.Uri);
        //    gameDataResponse.EnsureSuccessStatusCode();
        //}

        var accountprofilesummary_str = await gameDataResponse.Content.ReadAsStringAsync();
        var accountprofilesummary = JsonSerializer.Deserialize(accountprofilesummary_str, SourceGenerationContext.Default.ResAccountProfileSummary);

        return CacheService.SetCachingValue(Consts.AccountProfileSummary, accountprofilesummary!);
    }
}
