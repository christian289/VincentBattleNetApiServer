using VincentBattleNetApiServer.Core.Databases;
using VincentBattleNetApiServer.Core.DTOs.Base;
using VincentBattleNetApiServer.Core.DTOs.Res.ToyAPI;

namespace VincentBattleNetApiServer.Core.ServiceInterfaces;

/// <summary>
/// WOW 장난감(수집품) 정보 조회 서비스
/// </summary>
public interface IToyApiService
{
    Task<IEnumerable<LiteDbValue<BaseValue>>> GetToyIndexAsync();

    Task<ResToy> GetToyAsync(int toyId);
}