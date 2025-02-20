using VincentBattleNetApiServer.Core.DTOs.Base;
using VincentBattleNetApiServer.Core.DTOs.Res;
using VincentBattleNetApiServer.Core.DTOs.Res.AccountProfileAPI;
using VincentBattleNetApiServer.Core.DTOs.Res.CharacterAchievementsAPI;
using VincentBattleNetApiServer.Core.DTOs.Res.RealmAPI;
using VincentBattleNetApiServer.Core.DTOs.Res.ToyAPI;
using VincentBattleNetApiServer.Core.DTOs.Res.WowTokenAPI;

namespace VincentBattleNetApiServer.Core;

[JsonSourceGenerationOptions(WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(ProblemDetails))] // 추가 AspNetCore MVC 관련하여 추가해야 함.
[JsonSerializable(typeof(ValidationProblemDetails))] // 추가 AspNetCore MVC 관련하여 추가해야 함.
[JsonSerializable(typeof(AbsApi))]
[JsonSerializable(typeof(BaseValue))]
[JsonSerializable(typeof(BaseSlugValue))]
[JsonSerializable(typeof(LinkObj))]
[JsonSerializable(typeof(BattlenetAccessToken))]
[JsonSerializable(typeof(ResRealmsIndex))]
[JsonSerializable(typeof(ResRealm))]
[JsonSerializable(typeof(ResToyIndex))]
[JsonSerializable(typeof(ResToy))]
[JsonSerializable(typeof(ResWowTokenIndex))]
[JsonSerializable(typeof(ResAccountProfileSummary))]
[JsonSerializable(typeof(ResAccountCollectionsIndex))]
[JsonSerializable(typeof(ResAccountToysCollectionSummary))]
[JsonSerializable(typeof(ResCharacterAchievementsSummary))]
public partial class SourceGenerationContext : JsonSerializerContext
{
}
