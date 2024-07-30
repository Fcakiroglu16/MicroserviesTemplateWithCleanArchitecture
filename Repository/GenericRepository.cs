

using Core.Data;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : IEntity
{
    protected readonly IMongoCollection<T> Collection;

    public GenericRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var dbName = configuration.GetConnectionString("MongoDbName");
        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(dbName);
        Collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }

    public Task Delete(string id)
    {
        return Collection.FindOneAndDeleteAsync(x => x.Id == id);
    }

    public Task<T> GetById(string id)
    {
        return Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task<List<T>> GetAll()
    {
        return Collection.AsQueryable().ToListAsync();
    }

    public Task Insert(T entity)
    {
        return Collection.InsertOneAsync(entity);
    }

    public Task Update(T entity)
    {
        return Collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
    }
}