using MongoDB.Bson.Serialization.Attributes;

namespace Simples_Data.Models;
public class ProjetoModel {

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
    public ProjetoModelStatusEnum status { get; set; }

    [BsonElement("dataInfoModel")]
    public DataInfoModel dataInfoModel;

    public ProjetoModel() {
        this._id = Guid.NewGuid().ToString();
    }
}

public enum ProjetoModelStatusEnum {
    ATIVO,
    INATIVO
}
