namespace VincentBattleNetApiServer.Core.DTOs.Res;

public record BattlenetAccessToken()
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; init; }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; init; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }

    [JsonPropertyName("sub")]
    public string? Sub { get; init; }
}