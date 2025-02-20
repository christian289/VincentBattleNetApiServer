using VincentBattleNetApiServer.Core.DTOs.Res.AccountProfileAPI;

namespace VincentBattleNetApiServer.Core.ServiceInterfaces
{
    public interface IAccountCollectionsIndexService
    {
        Task<ResAccountCollectionsIndex> GetResAccountCollectionsIndexAsync();
    }
}