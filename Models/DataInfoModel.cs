using MongoDB.Bson.Serialization.Attributes;

namespace Simples_Data.Models;
public class DataInfoModel {

    public DataInfoModel() { }

    [BsonElement("active")]
    public Boolean active { get; set; }

    [BsonElement("updateDetails")]
    public List<UpdateDetail> updateDetails { get; set; }

}

public class UpdateDetail {
    public UpdateDetail() { }

    [BsonElement("userID")]
    public string userID { get; set; }

    [BsonElement("userName")]
    public string userName { get; set; }

    [BsonElement("updateDetailsActionEnum")]
    public UpdateDetailsActionEnum updateDetailsActionEnum { get; set; }

    [BsonElement("actionTrackID")]
    public string actionTrackID { get; set; }

    [BsonElement("dataTimeAction")]
    public DateTime dataTimeAction { get; set; }

}

public enum UpdateDetailsActionEnum {
    CREATE = 1,
    UPDATE = 2,
    DELETE = 3
}