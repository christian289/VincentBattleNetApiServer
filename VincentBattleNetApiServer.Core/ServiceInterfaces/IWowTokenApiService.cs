using VincentBattleNetApiServer.Core.DTOs.Res.WowTokenAPI;

namespace VincentBattleNetApiServer.Core.ServiceInterfaces;

/// <summary>
/// WOW 인게임 토큰 골드 시세를 가져올 수 있는 서비스
/// </summary>
public interface IWowTokenApiService
{
    Task<ResWowTokenIndex> GetWowTokenIndexAsync();
}
