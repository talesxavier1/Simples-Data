
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MongoDB.Driver;
using Simples_Data.Models;
using SingularChatAPIs.BD;

namespace Simples_Data.APIs_SimplesData_Front.Controllers;
[ApiController]
[Route("odata/v1")]
public class OdataProjetoController : ODataController {
    private IMongoCollection<ProjetoModel> projetoCollection;
    private IMongoDatabase mongoDataBase;

    public OdataProjetoController() {
        this.mongoDataBase = MongoDBConnection.getMongoDatabase();
        this.projetoCollection = mongoDataBase.GetCollection<ProjetoModel>("Projetos");
    }

    [EnableQuery]
    [HttpGet("ProjetoModels")]
    async public Task<IQueryable<ProjetoModel>> Get() {

        var filter = Builders<ProjetoModel>.Filter.Eq(DOC => DOC.dataInfoModel.active,true);
        var cursor = await projetoCollection.FindAsync(filter);

        return cursor.ToList().AsQueryable();
    }

}
