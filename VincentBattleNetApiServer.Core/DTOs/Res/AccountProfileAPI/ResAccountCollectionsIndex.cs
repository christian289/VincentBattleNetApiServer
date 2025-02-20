using VincentBattleNetApiServer.Core.DTOs.Base;

namespace VincentBattleNetApiServer.Core.DTOs.Res.AccountProfileAPI;

public record ResAccountCollectionsIndex : AbsApi
{
    [JsonPropertyName("pets")]
    public LinkObj? Pets { get; set; }

    [JsonPropertyName("mounts")]
    public LinkObj? Mounts { get; set; }

    [JsonPropertyName("heirlooms")]
    public LinkObj? Heirlooms { get; set; }

    [JsonPropertyName("toys")]
    public LinkObj? Toys { get; set; }

    [JsonPropertyName("transmogs")]
    public LinkObj? Transmogs { get; set; }
}