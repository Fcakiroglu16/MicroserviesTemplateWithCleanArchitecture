namespace Core.Data;

public interface IGenericRepository<T> where T : IEntity
{
    Task<List<T>> GetAll();
    Task<T> GetById(string id);
    Task Insert(T entity);
    Task Update(T entity);
    Task Delete(string id);
}