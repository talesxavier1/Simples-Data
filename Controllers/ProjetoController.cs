using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Simples_Data.Models;
using Simples_Data.Models.ViewModel;
using SingularChatAPIs.BD;

namespace Simples_Data.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ProjetoController : Controller {

    private IMongoCollection<ProjetoModel> projetoCollection;
    private IMongoDatabase mongoDataBase;

    [Route("Projeto/{skip}/{take}")]
    public async Task<IActionResult> Index(int skip,int take) {
        mongoDataBase = MongoDBConnection.getMongoDatabase();
        projetoCollection = mongoDataBase.GetCollection<ProjetoModel>("Projetos");


        var filter = Builders<ProjetoModel>.Filter.Eq(DOC => DOC.dataInfoModel.active,true);
        var findOptions = new FindOptions<ProjetoModel>() {
            Skip = skip,
            Limit = take
        };
        var cursor = await projetoCollection.FindAsync(filter,findOptions);
        var count = await projetoCollection.CountDocumentsAsync(filter);

        var result = new ProjetoViewModel() {
            projetoModels = await cursor.ToListAsync(),
            count = count
        };

        return View("ProjetoIndex",result);
    }

}
