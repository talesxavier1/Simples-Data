namespace Simples_Data.APIs.Models;

public class ResponseModel {

    public string responseID { get; set; } = "";
    public string requestID { get; set; } = "";
    public string strResponseDateTime { get; private set; } = "";
    public string strRequestDateTime { get; set; } = "";
    public string responseText { get; set; } = "";
    private string _status = "NOK";

    public string status {
        get {
            return this._status;
        }
        set {
            var statusValues = new List<string>() { "OK","NOK" };
            if (!statusValues.Contains(value)) {
                throw new ArgumentException(
                    "\nErro: [Valor não permitido.] \n" +
                    "Origem: ResponseModel -> status\n" +
                    $"Valor: {value}\n" +
                    $"Valores aceitos: {string.Join(", ",statusValues)}");
            }
            this._status = value;
        }
    }


    public object[] responseData { get; set; } = Array.Empty<object>();

    public ResponseModel() {
        responseID = "RESPONSE_" + Guid.NewGuid().ToString("N");
        strResponseDateTime = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
    }
}

