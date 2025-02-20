using VincentBattleNetApiServer.Core.DTOs.Base;

namespace VincentBattleNetApiServer.Core.DTOs.Res.RealmAPI;

public record ResRealm : AbsApi
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("region")]
    public BaseValue? Region { get; set; }

    [JsonPropertyName("connected_realm")]
    public LinkObj? ConnectedRealm { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("category")]
    public string? Category { get; set; }

    [JsonPropertyName("locale")]
    public string? Locale { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("type")]
    public InnerType? Type { get; set; }

    [JsonPropertyName("is_tournament")]
    public bool IsTournament { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    public record InnerType
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
