using MongoDB.Bson.Serialization.Attributes;

namespace Simples_Data.APIs.Models {
    public class UserModel {

        [BsonId]
        public string _id { get; set; }
        public string userID { get; set; }
        public string userName { get; set; }
        public string userPass { get; set; }
        public string userEmail { get; set; }
        public string projectID { get; set; }

    }
}