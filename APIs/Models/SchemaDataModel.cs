using MongoDB.Bson.Serialization.Attributes;

namespace Simples_Data.APIs.Models;
public class SchemaDataModel {

    [BsonId]
    public string _id { get; set; }

    [BsonElement("schemaDataID")]
    public string schemaDataID { get; set; }

    [BsonElement("title")]
    public string title { get; set; }

    [BsonElement("projectID")]
    public string projectID { get; set; }
}
