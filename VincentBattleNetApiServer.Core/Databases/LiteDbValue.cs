namespace VincentBattleNetApiServer.Core.Databases;

public class LiteDbValue<T>(T value) : ILiteDbValue
{
    public T Value { get; set; } = value;
    public DateTime CreatedTime { get; set; }
    public DateTime LastUpdatedTime { get; set; }
}
