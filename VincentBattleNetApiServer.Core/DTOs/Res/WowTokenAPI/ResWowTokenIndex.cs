using VincentBattleNetApiServer.Core.DTOs.Base;

namespace VincentBattleNetApiServer.Core.DTOs.Res.WowTokenAPI;

public record ResWowTokenIndex : AbsApi
{
    [JsonPropertyName("last_updated_timestamp")]
    public long LastUpdatedTimestamp { get; set; }

    [JsonPropertyName("price")]
    public int Price { get; set; }
}
