using Newtonsoft.Json;

namespace CarAuctionsSystem.Application.Models;

public class ResultDto<T>
{
    public int StatusCode { get; set; }
    public object? Error { get; set; }
    public T? Content { get; set; }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}
