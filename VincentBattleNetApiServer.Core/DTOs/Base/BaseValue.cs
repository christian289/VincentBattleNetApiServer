namespace VincentBattleNetApiServer.Core.DTOs.Base;

public record BaseValue
{
    [JsonPropertyName("key")]
    public LinkObj? Key { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }
}
