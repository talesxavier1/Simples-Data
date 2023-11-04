using System.ComponentModel.DataAnnotations;

namespace Simples_Data.APIs.Models;

public abstract class RequestModel {
    public string requestID { get; private set; } = "";
    public string strRequestDateTime { get; private set; }
    [Required]
    public string projectID { get; set; } = "";
    [Required]
    public string userID { get; set; } = "";
    [Required]
    public string schemaDataID { get; set; } = "";

    public RequestModel() {
        requestID = "REQUEST_" + Guid.NewGuid().ToString("N");
        strRequestDateTime = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
    }
}

public class AddDataRequestModel : RequestModel {

    [Required]
    public string dataJson { get; set; }

    public AddDataRequestModel() { }
}

public class CountDataRequestModel : RequestModel {

    [Required]
    public string mongoFilter { get; set; }

    public CountDataRequestModel() { }
}

public class GetDataRequestModel : RequestModel {

    [Required]
    public string mongoFilter { get; set; }
    [Required]
    public int skip { get; set; }
    [Required]
    private int _take;
    public int take {
        get {
            return _take;
        }
        set {
            if (value == 0) {
                _take = 30;
            } else if (value > 100) {
                _take = 100;
            } else {
                _take = value;
            }
        }
    }

    public GetDataRequestModel() { }

}

public class DeleteDataRequestModel : RequestModel {

    [Required]
    public string mongoFilter { get; set; }

    public DeleteDataRequestModel() { }

}

public class UpdateDataRequest : RequestModel {

    [Required]
    public string mongoFilter { get; set; }

    [Required]
    public string dataJson { get; set; }

    public UpdateDataRequest() { }
}