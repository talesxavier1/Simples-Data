using Microsoft.AspNetCore.Mvc;
using Simples_Data.APIs.Models;

namespace Simples_Data.APIs.Controllers;

[ApiController]
[Route("api/v1/data")]
public class DataController : Controller {

    [HttpPost]
    [Route("addData")]
    public async Task<ActionResult<ResponseModel>> addData([FromBody] AddDataRequestModel request) {
        Console.WriteLine("-----------------  [Route(\"addData\")] ----------------------");
        var response = new ResponseModel() {
            requestID = request.requestID,
            strRequestDateTime = request.strRequestDateTime,
        };

        if (request.dataJson == "INVALID") {
            response.responseText = "dataJson inválido";
            response.status = "NOK";
        }




        return Ok(response);
    }

    [HttpPost]
    [Route("countData")]
    public async Task<ActionResult<ResponseModel>> countData([FromBody] CountDataRequestModel request) {
        var response = new ResponseModel() {
            requestID = request.requestID,
            strRequestDateTime = request.strRequestDateTime,
        };

        if (request.mongoFilter == "INVALID") {
            response.responseText = "Filtro inválido";
            response.status = "NOK";
            return BadRequest(response);
        }


        return Ok("ok");
    }

    [HttpPost]
    [Route("getData")]
    public async Task<ActionResult<ResponseModel>> getData([FromBody] GetDataRequestModel request) {
        var response = new ResponseModel() {
            requestID = request.requestID,
            strRequestDateTime = request.strRequestDateTime,
        };

        return Ok(response);
    }

    [HttpPost]
    [Route("deleteData")]
    public async Task<ActionResult<ResponseModel>> deleteData([FromBody] DeleteDataRequestModel request) {
        var response = new ResponseModel() {
            requestID = request.requestID,
            strRequestDateTime = request.strRequestDateTime,
        };


        return Ok(response);
    }

    [HttpPost]
    [Route("updateData")]
    public async Task<ActionResult<ResponseModel>> updateData([FromBody] UpdateDataRequest request) {
        var response = new ResponseModel() {
            requestID = request.requestID,
            strRequestDateTime = request.strRequestDateTime,
        };

        return Ok(response);
    }
}