namespace VincentBattleNetApiServer.Core.ServiceInterfaces;

/// <summary>
/// 로그인한 계정의 액세스 토큰을 가져올 수 있는 서비스
/// </summary>
public interface IBattleNetAccessTokenService
{
    Task<string> GetTokenAsync(bool sub_token_use = false);
}
