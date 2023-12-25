using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Simples_Data.Models.ViewModel;
public class ViewActionResponse<T> {

    [JsonConverter(typeof(StringEnumConverter))]
    public ViewActionResponseStatusEnum status { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public ViewActionResponseActionEnum action { get; set; }

    public string message { get; set; }
    public T content { get; set; }

}

public enum ViewActionResponseStatusEnum {
    OK,
    NOK
}

public enum ViewActionResponseActionEnum {
    UPDATE,
    DELETE,
    GET,
    COUNT,
    CREATE
}

