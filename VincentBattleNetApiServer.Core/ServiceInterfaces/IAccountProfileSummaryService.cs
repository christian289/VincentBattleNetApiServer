using VincentBattleNetApiServer.Core.DTOs.Res.AccountProfileAPI;

namespace VincentBattleNetApiServer.Core.ServiceInterfaces;

/// <summary>
/// 로그인한 본인 계정에 대한 캐릭터 정보를 가져올 수 있는 서비스
/// </summary>
public interface IAccountProfileSummaryService
{
    Task<ResAccountProfileSummary> GetResAccountProfileSummaryAsync();
}
