namespace Simples_Data.Repository.Interfaces;
public interface IGenericRepository<T> {
    public Task<IEnumerable<T>> GetAll();
    public Task<T> GetById(string id);
    public Task<bool> Exist(string id);
    public Task<bool> tryAdd(T entity);
    public Task<bool> tryUpdate(T entity);
    public Task<bool> tryDelete(string id);
}
