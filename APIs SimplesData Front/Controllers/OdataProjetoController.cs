
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MongoDB.Driver;
using Simples_Data.Models;
using Simples_Data.Repository.Implementations;

namespace Simples_Data.APIs_SimplesData_Front.Controllers;
[ApiController]
[Route("odata/v1")]
public class OdataProjetoController : ODataController {
    private IMongoCollection<ProjetoModel> _projetoCollection;

    public OdataProjetoController(IMongoDatabase mongoDatabase) {
        this._projetoCollection = mongoDatabase.GetCollection<ProjetoModel>("Projetos");
    }

    [EnableQuery]
    [HttpGet("ProjetoModels")]
    async public Task<IEnumerable<ProjetoModel>> Get() {
        var result = await new ProjetoRepository(_projetoCollection).GetAll();
        return result;
    }

}
