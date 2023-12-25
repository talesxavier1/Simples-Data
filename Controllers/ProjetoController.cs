using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using Simples_Data.Models;
using Simples_Data.Models.ViewModel;
using Simples_Data.Repository.Implementations;

namespace Simples_Data.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ProjetoController : Controller {

    private IMongoCollection<ProjetoModel> _projetoCollection;

    public ProjetoController(IMongoDatabase database) {
        this._projetoCollection = database.GetCollection<ProjetoModel>("Projetos");
    }

    [Route("Projeto")]
    public IActionResult Index() {
        return View("ProjetoIndex");
    }

    [Route("Projeto/Page/{id?}")]
    [HttpGet]
    async public Task<IActionResult> Page(string? id) {
        var responseBag = new ViewActionResponse<ProjetoModel>();

        if (id == null) {
            responseBag.action = ViewActionResponseActionEnum.GET;
            responseBag.status = ViewActionResponseStatusEnum.OK;
            responseBag.content = new ProjetoModel();

            ViewBag.jsonResponseBag = JsonConvert.SerializeObject(responseBag);
            ViewBag.responseBag = responseBag;

            return View("ProjetoForm", responseBag.content);
        }

        var result = await new ProjetoRepository(_projetoCollection).GetById(id);

        responseBag.action = ViewActionResponseActionEnum.GET;

        if (result != null) {
            responseBag.status = ViewActionResponseStatusEnum.OK;
            responseBag.message = string.Empty;
            responseBag.content = result;
        } else {
            responseBag.status = ViewActionResponseStatusEnum.NOK;
            responseBag.message = "NÃO ENCONTRADO.";
            responseBag.content = new ProjetoModel();
        }

        ViewBag.jsonResponseBag = JsonConvert.SerializeObject(responseBag);
        ViewBag.responseBag = JsonConvert.SerializeObject(responseBag);

        return View("ProjetoForm", responseBag.content);
    }

    [Route("Projeto/Page")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PagePost(ProjetoModel projeto) {
        ProjetoRepository projetoRepository = new(_projetoCollection);
        var responseBag = new ViewActionResponse<ProjetoModel>();

        var resultExist = await projetoRepository.Exist(projeto._id);
        if (resultExist) {
            var resultUpdate = await projetoRepository.tryUpdate(projeto);
            if (resultUpdate == true) {
                return View("ProjetoIndex");
            } else {
                responseBag.status = ViewActionResponseStatusEnum.NOK;
                responseBag.action = ViewActionResponseActionEnum.UPDATE;
                responseBag.message = "Não foi possível atualizar registro";
                responseBag.content = projeto;
                return View("ProjetoForm", responseBag);
            }
        }

        var resultAdd = await projetoRepository.tryAdd(projeto);
        if (resultAdd == true) {
            return View("ProjetoIndex");
        }

        responseBag.status = ViewActionResponseStatusEnum.NOK;
        responseBag.action = ViewActionResponseActionEnum.CREATE;
        responseBag.message = "Não foi possível criar registro";
        responseBag.content = projeto;

        ViewBag.jsonResponseBag = JsonConvert.SerializeObject(responseBag);
        ViewBag.responseBag = responseBag;

        return View("ProjetoForm", responseBag.content);
    }

    [Route("Projeto/Delete/{id}")]
    [HttpGet]
    public async Task<ActionResult> Delete(string id) {
        ProjetoRepository projetoRepository = new(_projetoCollection);
        var responseBag = new ViewActionResponse<ProjetoModel>();

        var result = await projetoRepository.GetById(id);
        if (result == null) {
            responseBag.status = ViewActionResponseStatusEnum.NOK;
            responseBag.action = ViewActionResponseActionEnum.GET;
            responseBag.message = "Não foi possível encontrar conteúdo";

            ViewBag.jsonResponseBag = JsonConvert.SerializeObject(responseBag);
            return View("ProjetoIndex");
        }
        responseBag.status = ViewActionResponseStatusEnum.OK;
        responseBag.action = ViewActionResponseActionEnum.GET;
        responseBag.message = string.Empty;
        responseBag.content = result;

        ViewBag.jsonResponseBag = JsonConvert.SerializeObject(responseBag);
        return View("ProjetoConfirmDelete", result);
    }

    [Route("Projeto/Delete/{id}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteAction(string id) {
        ProjetoRepository projetoRepository = new(_projetoCollection);
        var responseBag = new ViewActionResponse<ProjetoModel>();

        var result = await projetoRepository.tryDelete(id);
        if (!result) {
            responseBag.status = ViewActionResponseStatusEnum.NOK;
            responseBag.action = ViewActionResponseActionEnum.DELETE;
            responseBag.message = "Não foi possível deletar o conteúdo.";
        }

        responseBag.status = ViewActionResponseStatusEnum.OK;
        responseBag.action = ViewActionResponseActionEnum.DELETE;
        responseBag.message = "Registro deletado";

        ViewBag.jsonResponseBag = JsonConvert.SerializeObject(responseBag);
        return View("ProjetoIndex");
    }
}