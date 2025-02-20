namespace VincentBattleNetApiServer.Core.DTOs.Base;

public record LinkObj
{
    [JsonPropertyName("href")]
    public Uri? Href { get; set; }
}
