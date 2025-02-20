using VincentBattleNetApiServer.Core.DTOs.Base;

namespace VincentBattleNetApiServer.Core.DTOs.Res.ToyAPI;

public record ResToyIndex : AbsApi
{
    [JsonPropertyName("toys")]
    public BaseValue[]? Toys { get; set; }
}
