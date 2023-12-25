using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Simples_Data.Models;
public class ProjetoModel {

    [Key]
    [BsonId]
    public string _id { get; set; }

    [BsonElement("nomeProjeto")]
    public string nomeProjeto { get; set; }

    [BsonElement("dataBaseDesenvolvimento")]
    public string dataBaseDesenvolvimento { get; set; }

    [BsonElement("dataBaseHomologacao")]
    public string dataBaseHomologacao { get; set; }

    [BsonElement("dataBaseProducao")]
    public string dataBaseProducao { get; set; }

    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    [JsonConverter(typeof(StringEnumConverter))]
    public ProjetoModelStatusEnum status { get; set; }

    [BsonElement("dataInfoModel")]
    public DataInfoModel dataInfoModel { get; set; }

    public ProjetoModel() {
        this._id = Guid.NewGuid().ToString();
    }
}


public enum ProjetoModelStatusEnum {
    ATIVO,
    INATIVO
}
