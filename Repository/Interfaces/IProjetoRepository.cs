using Simples_Data.Models;

public interface IProjetoRepository {
    public Task<IEnumerable<ProjetoModel>> GetAll();
    public Task<ProjetoModel> GetById(string id);
}
