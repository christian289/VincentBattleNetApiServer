using VincentBattleNetApiServer.Core.DTOs.Res.RealmAPI;

namespace VincentBattleNetApiServer.Core.ServiceInterfaces;

public interface IRealmApiService
{
    Task<ResRealmsIndex> GetRealmsIndexAsync();

    Task<ResRealm> GetRealmAsync(string realmSlug);
}
