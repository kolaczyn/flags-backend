using System.Text;
using System.Text.Json;

namespace Flags.Tests.Common;

public static class RequestsCommon
{
    public static async Task<T?> GetRequestContent<T>(HttpResponseMessage httpResponseMessage)
    {
        // TODO maybe reuse to make it more performant. That would probably mean changing it from static to e.g. base class
        var jsonSettings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        // Maybe I should throw error? This should be fine in tests
        return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), jsonSettings);
    }

    public static StringContent BuildRequestContent<T>(T request)
    {
        var serialized = JsonSerializer.Serialize(request);
        return new StringContent(serialized, Encoding.UTF8, "application/json");
    }
}