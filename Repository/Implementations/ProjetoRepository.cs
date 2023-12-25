using MongoDB.Driver;
using Simples_Data.Models;
using Simples_Data.Repository.Interfaces;
using System.Reflection;

namespace Simples_Data.Repository.Implementations;
public class ProjetoRepository : IGenericRepository<ProjetoModel> {

    private IMongoCollection<ProjetoModel> _projetosCollection;

    public ProjetoRepository(IMongoCollection<ProjetoModel> projetosCollection) {
        _projetosCollection = projetosCollection;
    }

    public async Task<bool> Existe(string id) {
        var filter = Builders<ProjetoModel>.Filter.Eq(DOC => DOC._id, id);
        var result = await _projetosCollection.CountDocumentsAsync(filter);
        if (result > 0) { return true; }
        return false;
    }

    public async Task<IEnumerable<ProjetoModel>> GetAll() {
        var filter = Builders<ProjetoModel>.Filter.Eq(DOC => DOC.dataInfoModel.active, true);
        var cursor = await _projetosCollection.FindAsync(filter);
        return await cursor.ToListAsync();
    }

    public async Task<ProjetoModel> GetById(string id) {

        var filter = Builders<ProjetoModel>.Filter.Eq(DOC => DOC.dataInfoModel.active, true);
        var filters = new List<FilterDefinition<ProjetoModel>>() {
            Builders<ProjetoModel>.Filter.Eq(DOC => DOC.dataInfoModel.active,true),
            Builders<ProjetoModel>.Filter.Eq(DOC => DOC._id, id)
        };
        var cursor = await _projetosCollection.FindAsync(Builders<ProjetoModel>.Filter.And(filters));
        var result = cursor.FirstOrDefault<ProjetoModel>();
        return result;
    }

    public async Task<bool> tryAdd(ProjetoModel entity) {
        if ((await Existe(entity._id))) {
            return false;
        }

        try {
            entity.dataInfoModel = new DataInfoModel() {
                active = true,
                updateDetails = new List<UpdateDetail>() {
                    new UpdateDetail() {
                        actionTrackID = "SEM TrackID FALTA IMPLEMENTAR",
                        updateDetailsActionEnum = UpdateDetailsActionEnum.CREATE,
                        userID = "SEM ID FALTA IMPLAMENTAR",
                        userName = "SEM NOME FALTA IMPLAMENTAR",
                        dataTimeAction = DateTime.UtcNow.AddHours(-3)
                    }
                }
            };
            await _projetosCollection.InsertOneAsync(entity);
            return true;
        } catch {
            return false;
        }

    }

    public async Task<bool> tryDelete(string id) {
        var filter = Builders<ProjetoModel>.Filter.Eq(DOC => DOC._id, id);
        var updateDefinition = Builders<ProjetoModel>.Update.Set("dataInfoModel.active", false);

        var result = await _projetosCollection.UpdateOneAsync(filter, updateDefinition);

        if (result.MatchedCount > 0) {
            return true;
        }
        return false;
    }

    public async Task<bool> tryUpdate(ProjetoModel entity) {
        var filterCurrentDoc = Builders<ProjetoModel>.Filter.Eq(DOC => DOC._id, entity._id);
        var currentDoc = await (await _projetosCollection.FindAsync(filterCurrentDoc)).FirstOrDefaultAsync();

        if (currentDoc == null) {
            return false;
        }

        PropertyInfo[] propertiesInfo = entity.GetType().GetProperties();

        List<UpdateDefinition<ProjetoModel>> listUpdateDefinitions = propertiesInfo.Select((PropertyInfo VALUE) => {
            return Builders<ProjetoModel>.Update.Set(VALUE.Name, VALUE.GetValue(entity));
        }).ToList();

        currentDoc.dataInfoModel.updateDetails.Add(new UpdateDetail() {
            actionTrackID = "SEM TrackID FALTA IMPLEMENTAR",
            updateDetailsActionEnum = UpdateDetailsActionEnum.UPDATE,
            userID = "SEM ID FALTA IMPLAMENTAR",
            userName = "SEM NOME FALTA IMPLAMENTAR",
            dataTimeAction = DateTime.UtcNow.AddHours(-3)
        });
        listUpdateDefinitions.Add(Builders<ProjetoModel>.Update.Set("dataInfoModel", currentDoc.dataInfoModel));


        var result = await _projetosCollection.UpdateOneAsync(filterCurrentDoc, Builders<ProjetoModel>.Update.Combine(listUpdateDefinitions));

        if (result.MatchedCount > 0) {
            return true;
        }
        return false;
    }
}
