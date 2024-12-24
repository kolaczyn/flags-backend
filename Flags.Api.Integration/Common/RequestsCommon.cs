using System.Text;
using System.Text.Json;

namespace Flags.Tests.Common;

public class RequestsCommon
{
    private readonly JsonSerializerOptions _jsonSettings = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public async Task<T> GetRequestContent<T>(HttpResponseMessage httpResponseMessage)
    {
        var result =
            JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), _jsonSettings);

        if (result is null)
        {
            throw new ApplicationException("GetRequestContent failed");
        }

        return result;
    }

    public static StringContent BuildRequestContent<T>(T request)
    {
        var serialized = JsonSerializer.Serialize(request);
        return new StringContent(serialized, Encoding.UTF8, "application/json");
    }
}