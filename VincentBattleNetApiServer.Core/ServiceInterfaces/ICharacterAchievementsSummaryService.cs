using VincentBattleNetApiServer.Core.DTOs.Res.CharacterAchievementsAPI;

namespace VincentBattleNetApiServer.Core.ServiceInterfaces
{
    public interface ICharacterAchievementsSummaryService
    {
        Task<ResCharacterAchievementsSummary> GetResCharacterAchievementsSummaryAsync(string realmSlug, string characterName);
    }
}