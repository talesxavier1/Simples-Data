using Microsoft.AspNetCore.Mvc;

namespace Simples_Data.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ProjetoController : Controller {

    [Route("Projeto")]
    public IActionResult Index(int skip,int take) {
        return View("ProjetoIndex");
    }

}
