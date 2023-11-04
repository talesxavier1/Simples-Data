using MongoDB.Bson.Serialization.Attributes;

namespace Simples_Data.APIs.Models;
public class ProjetoMasterDataModel {

    [BsonId]
    public string _id { get; set; }

    [BsonElement("nomeProjeto")]
    public string nomeProjeto { get; set; }

    [BsonElement("projectID")]
    public string projectID { get; set; }

    [BsonElement("dataBaseDesenvolvimento")]
    public string dataBaseDesenvolvimento { get; set; }

    [BsonElement("dataBaseHomologacao")]
    public string dataBaseHomologacao { get; set; }

    [BsonElement("dataBaseProducao")]
    public string dataBaseProducao { get; set; }

    [BsonElement("status")]
    public string status { get; set; }
}
