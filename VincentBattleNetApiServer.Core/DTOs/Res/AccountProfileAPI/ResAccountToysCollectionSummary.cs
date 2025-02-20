using VincentBattleNetApiServer.Core.DTOs.Base;

namespace VincentBattleNetApiServer.Core.DTOs.Res.AccountProfileAPI;

public record class ResAccountToysCollectionSummary : AbsApi
{
    [JsonPropertyName("toys")]
    public BaseValue[]? Toys { get; set; }
}
