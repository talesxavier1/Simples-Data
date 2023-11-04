using MongoDB.Driver;
using Simples_Data.APIs.Models;
using SingularChatAPIs.BD;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using System.Text.Json;


namespace Simples_Data.APIs.Pipelines;

public static class PipelineValidacaoRequisicao {


    public static IApplicationBuilder UsePipelineValidacaoRequisicao(this IApplicationBuilder mainApp) {

        mainApp.UseMiddleware<MValidacaoBodyData>();

        mainApp.UseWhen(context => {
            List<string> rotas = new List<string> { "/countData","/getData","/deleteData","/updateData" };
            return rotas.Any(VALUE => context.Request.Path.Value?.IndexOf(VALUE) > -1);
        }
        ,branch => {
            branch.UseMiddleware<MValidacaoMongoFilter>();
        });

        mainApp.UseWhen(context => {
            List<string> rotas = new List<string> { "/updateData","/addData" };
            return rotas.Any(VALUE => context.Request.Path.Value?.IndexOf(VALUE) > -1);
        },branch => {
            branch.UseMiddleware<MValidacaoJsonData>();
        });

        mainApp.UseMiddleware<MValidacaoAssinaturas>();

        return mainApp;
    }



}

public class MValidacaoBodyData {

    private RequestDelegate _next;

    public MValidacaoBodyData(RequestDelegate next) {
        this._next = next;
    }

    public async Task Invoke(HttpContext context) {
        Console.WriteLine("-----------------  MValidacaoBodyData ----------------------");

        var utils = new PipelineValidacaoRequisicaoUtils(context);
        var requestBodyObject = await utils.getRequestObject();

        if (requestBodyObject == null) {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Não foi possível desserializar payload enviado.");
            return;
        }

        var validationContext = new ValidationContext(requestBodyObject);
        var validationResults = new List<ValidationResult>();
        bool valido = Validator.TryValidateObject(requestBodyObject,validationContext,validationResults,true);
        if (!valido) {
            List<string> errosList = validationResults.Select(VALUE => $"Campo '{VALUE.MemberNames.FirstOrDefault()}' é obrigatório.").ToList();
            context.Response.StatusCode = 400;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync(string.Join("\n",errosList));
            return;
        }

        await _next.Invoke(context);
    }
}

public class MValidacaoMongoFilter {

    private RequestDelegate _next;

    public MValidacaoMongoFilter(RequestDelegate next) {
        this._next = next;
    }

    public async Task Invoke(HttpContext context) {
        Console.WriteLine("-----------------  MValidacaoMongoFilter ----------------------");

        var utils = new PipelineValidacaoRequisicaoUtils(context);
        dynamic? requestBodyObject = await utils.getRequestObject();
        if (requestBodyObject == null) {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Não foi possível desserializar payload enviado.");
            return;
        }
        if (requestBodyObject.mongoFilter == "{}") {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("mongoFilter vazio.");
            return;
        }
        try {
            JsonSerializer.Deserialize<object>(requestBodyObject.mongoFilter);
        } catch (Exception) {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Não foi possível desserializar mongoFilter enviado.");
            return;
        }

        await _next.Invoke(context);
    }
}

public class MValidacaoJsonData {

    private RequestDelegate _next;

    public MValidacaoJsonData(RequestDelegate next) {
        this._next = next;
    }

    public async Task Invoke(HttpContext context) {
        Console.WriteLine("-----------------  MValidacaoJsonData ----------------------");

        var utils = new PipelineValidacaoRequisicaoUtils(context);
        dynamic? requestBodyObject = await utils.getRequestObject();
        if (requestBodyObject == null) {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Não foi possível desserializar payload enviado.");
            return;
        }
        if (requestBodyObject.dataJson == "{}") {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("dataJson vazio.");
            return;
        }
        try {
            JsonSerializer.Deserialize<object>(requestBodyObject.dataJson);
        } catch (Exception) {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Não foi possível desserializar dataJson enviado.");
            return;
        }

        await _next.Invoke(context);
    }
}

public class MValidacaoAssinaturas {

    private IMongoCollection<ProjetoMasterDataModel> projetosCollection;
    private IMongoCollection<SchemaDataModel> schemaDataCollection;
    private IMongoCollection<UserModel> userCollection;


    private RequestDelegate _next;

    public MValidacaoAssinaturas(RequestDelegate next) {
        _next = next;
        IMongoDatabase mongoDatabasea = MongoDBConnection.getMongoDatabase();
        projetosCollection = mongoDatabasea.GetCollection<ProjetoMasterDataModel>("Projetos");
        schemaDataCollection = mongoDatabasea.GetCollection<SchemaDataModel>("Schemas");
        userCollection = mongoDatabasea.GetCollection<UserModel>("User");
    }

    public async Task Invoke(HttpContext context) {
        Console.WriteLine("-----------------  MValidacaoAssinaturas ----------------------");

        var utils = new PipelineValidacaoRequisicaoUtils(context);
        dynamic? requestBodyObject = await utils.getRequestObject();
        ResponseModel response = new() {
            responseID = "",
            status = "NOK"
        };

        if (requestBodyObject == null) {
            context.Response.StatusCode = 400;
            response.responseText = "Não foi possível desserializar payload enviado.";
            await context.Response.WriteAsJsonAsync(response);
            return;
        }

        string projectID = requestBodyObject.projectID;
        var conutProjeto = await projetosCollection.CountDocumentsAsync(VALUE => VALUE.projectID.Equals(projectID));

        if (conutProjeto == 0) {
            context.Response.StatusCode = 400;
            response.responseText = $"Projeto com id '{projectID}' não encontrado.";
            await context.Response.WriteAsJsonAsync(response);
            return;
        }

        string schemaDataID = requestBodyObject.schemaDataID;
        var countSchemaData = await schemaDataCollection.CountDocumentsAsync(VALUE => VALUE.projectID.Equals(projectID) && VALUE.schemaDataID.Equals(schemaDataID));

        if (countSchemaData == 0) {
            context.Response.StatusCode = 400;
            response.responseText = $"schemaData com id '{schemaDataID}' e projectID '{projectID}' não encontrado.";
            await context.Response.WriteAsJsonAsync(response);
            return;
        }

        string userID = requestBodyObject.userID;
        var countUser = await userCollection.CountDocumentsAsync(VALUE => VALUE.projectID.Equals(projectID) && VALUE.userID.Equals(userID));

        if (countUser == 0) {
            context.Response.StatusCode = 400;
            response.responseText = $"User com id '{userID}' e projectID '{projectID}' não encontrado.";
            await context.Response.WriteAsJsonAsync(response);
            return;
        }
        await _next.Invoke(context);
    }
}

public class PipelineValidacaoRequisicaoUtils {

    private HttpContext context;

    public PipelineValidacaoRequisicaoUtils(HttpContext context) {
        this.context = context;
    }

    private async Task<string> readRequest() {
        string strRequest;
        using (StreamReader reader = new StreamReader(this.context.Request.Body)) {
            strRequest = await reader.ReadToEndAsync();
        }

        MemoryStream bodyStream = new MemoryStream(Encoding.UTF8.GetBytes(strRequest));
        this.context.Request.Body = bodyStream;

        return strRequest;
    }

    public async Task<object?> getRequestObject() {

        IDictionary<string,string> requestModels = new Dictionary<string,string>() {
            { "/addData", "Simples_Data.APIs.Models.AddDataRequestModel"},
            { "/countData", "Simples_Data.APIs.Models.CountDataRequestModel"},
            { "/getData", "Simples_Data.APIs.Models.GetDataRequestModel"},
            { "/deleteData", "Simples_Data.APIs.Models.DeleteDataRequestModel"},
            { "/updateData", "Simples_Data.APIs.Models.UpdateDataRequest"},
        };

        var strRequest = await readRequest();

        string? classeRequestMapeada = requestModels.Keys.FirstOrDefault(VALUE => context.Request.Path.Value?.IndexOf(VALUE) > -1);
        if (classeRequestMapeada is null) { Trace.Write($"AVISO \n ORIGEM: PipelineValidacaoRequisicaoUtils:getRequestObject \n MENSAGEM: Classe para '{context.Request.Path.Value}' não mapeada."); return null; }

        try {
            string strType = requestModels[classeRequestMapeada];
            Type? type = Type.GetType(strType);
            if (type is null) { Trace.Write($"AVISO \n ORIGEM: PipelineValidacaoRequisicaoUtils:getRequestObject \n MENSAGEM: Não foi possível criar Type para {classeRequestMapeada}"); return null; }

            return JsonSerializer.Deserialize(strRequest,type);
        } catch (Exception ex) {
            Trace.Write($"ERRO \n ORIGEM: PipelineValidacaoRequisicaoUtils:getRequestObject \n MENSAGEM: {ex}");
            return null;
        }
    }
}

