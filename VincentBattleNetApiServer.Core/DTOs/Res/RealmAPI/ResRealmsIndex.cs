using VincentBattleNetApiServer.Core.DTOs.Base;

namespace VincentBattleNetApiServer.Core.DTOs.Res.RealmAPI;

public record ResRealmsIndex : AbsApi
{
    [JsonPropertyName("realms")]
    public BaseSlugValue[]? Realms { get; set; }
}
