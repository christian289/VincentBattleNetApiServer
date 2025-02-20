using VincentBattleNetApiServer.Core.Options;
using VincentBattleNetApiServer.Core.ServiceInterfaces;

namespace VincentBattleNetApiServer.Core.Databases;

public class LiteDbService : ILiteDbService
{
    public LiteDbService(IOptionsMonitor<BattleNetApiLiteDB> dbFileName)
    {
        this.dbFileName = dbFileName;
        dbFilePath = Path.Combine(AppContext.BaseDirectory, this.dbFileName.CurrentValue.FileName);
    }

    private readonly IOptionsMonitor<BattleNetApiLiteDB> dbFileName;
    private readonly string dbFilePath;

    public int Insert<T>(string collectionName, T data) where T : ILiteDbValue
    {
        data.SetCreatedTime();
        data.UpdateLastUpdatedTime();
        using var db = new LiteDatabase(dbFilePath);
        var col = db.GetCollection<T>(collectionName);

        return col.Insert(data);
    }

    public int InsertBulk<T>(string collectionName, T[] data, int batchsize = 5000) where T : ILiteDbValue
    {
        foreach (var item in data)
            item.SetCreatedTime();

        using var db = new LiteDatabase(dbFilePath);
        var col = db.GetCollection<T>(collectionName);

        return col.InsertBulk(data, batchsize);
    }

    public bool Update<T>(T data) where T : ILiteDbValue
    {
        using var db = new LiteDatabase(dbFilePath);
        var col = db.GetCollection<T>();
        data.UpdateLastUpdatedTime();

        return col.Update(data);
    }

    public bool Delete<T>(int id)
    {
        using var db = new LiteDatabase(dbFilePath);
        var col = db.GetCollection<T>();

        return col.Delete(id);
    }

    public int DeleteAll<T>(string key) where T : ILiteDbValue
    {
        using var db = new LiteDatabase(dbFilePath);
        var col = db.GetCollection<T>(key);

        return col.DeleteAll();
    }

    public IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate)
    {
        using var db = new LiteDatabase(dbFilePath);
        var col = db.GetCollection<T>();

        return col.Find(predicate);
    }

    public IEnumerable<T> FindAll<T>(string key)
    {
        using var db = new LiteDatabase(dbFilePath);
        var col = db.GetCollection<T>(key);

        return col.FindAll();
    }
}
