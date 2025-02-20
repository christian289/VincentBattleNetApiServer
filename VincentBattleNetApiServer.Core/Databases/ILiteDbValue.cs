namespace VincentBattleNetApiServer.Core.Databases;

public interface ILiteDbValue
{
    DateTime CreatedTime { get; protected set; }
    DateTime LastUpdatedTime { get; protected set; }

    void SetCreatedTime() => CreatedTime = DateTime.Now;
    void UpdateLastUpdatedTime() => LastUpdatedTime = DateTime.Now;
}
