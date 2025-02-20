using VincentBattleNetApiServer.Core.DTOs.Base;

namespace VincentBattleNetApiServer.Core.DTOs.Res.ToyAPI;

public record ResToy : AbsApi
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("item")]
    public BaseValue? Item { get; set; }

    [JsonPropertyName("source")]
    public InnerSource? Source { get; set; }

    [JsonPropertyName("source_description")]
    public string? SourceDescription { get; set; }

    [JsonPropertyName("media")]
    public BaseValue? Media { get; set; }

    public record InnerSource
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}