namespace VincentBattleNetApiServer.Core.DTOs.Base;

public record BaseSlugValue : BaseValue
{
    [JsonPropertyName("slug")]
    public string? Slug { get; set; }
}