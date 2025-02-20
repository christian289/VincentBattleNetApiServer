using VincentBattleNetApiServer.Core.DTOs.Res.AccountProfileAPI;

namespace VincentBattleNetApiServer.Core.ServiceInterfaces;

public interface IAccountToysCollectionSummaryService
{
    Task<ResAccountToysCollectionSummary> GetResAccountToysCollectionSummaryAsync();
}