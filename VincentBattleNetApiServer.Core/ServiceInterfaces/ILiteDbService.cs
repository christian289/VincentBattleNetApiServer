using VincentBattleNetApiServer.Core.Databases;

namespace VincentBattleNetApiServer.Core.ServiceInterfaces;

public interface ILiteDbService
{
    bool Delete<T>(int id);

    int DeleteAll<T>(string key) where T : ILiteDbValue;

    IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate);

    IEnumerable<T> FindAll<T>(string key);
    
    int Insert<T>(string collectionName, T data) where T : ILiteDbValue;
    
    int InsertBulk<T>(string collectionName, T[] data, int batchsize = 5000) where T : ILiteDbValue;
    
    bool Update<T>(T data) where T : ILiteDbValue;
}