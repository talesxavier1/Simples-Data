using MongoDB.Driver;
using Simples_Data.Models;

namespace Simples_Data.Repository.Implementations;
public class ProjetoRepository : IProjetoRepository {

    private IMongoCollection<ProjetoModel> _projetosCollection;

    public ProjetoRepository(IMongoCollection<ProjetoModel> projetosCollection) {
        _projetosCollection = projetosCollection;
    }

    public async Task<IEnumerable<ProjetoModel>> GetAll() {
        var filter = Builders<ProjetoModel>.Filter.Eq(DOC => DOC.dataInfoModel.active,true);
        var cursor = await _projetosCollection.FindAsync(filter);
        return await cursor.ToListAsync();
    }

    public async Task<ProjetoModel> GetById(string id) {

        var filter = Builders<ProjetoModel>.Filter.Eq(DOC => DOC.dataInfoModel.active,true);
        var filters = new List<FilterDefinition<ProjetoModel>>() {
            Builders<ProjetoModel>.Filter.Eq(DOC => DOC.dataInfoModel.active,true),
            Builders<ProjetoModel>.Filter.Eq(DOC => DOC._id, id)
        };
        var cursor = await _projetosCollection.FindAsync(Builders<ProjetoModel>.Filter.And(filters));
        var result = cursor.FirstOrDefault<ProjetoModel>();
        return result;
    }
}
