using System.Text.Json;

namespace Flags.Tests.Common;

public static class GetRequestContentExtension
{
    public static async Task<T?> GetRequestContent<T>(this HttpResponseMessage httpResponseMessage)
    {
        // TODO maybe reuse to make it more performant. That would probably mean changing it from extension to e.g. base class
        var jsonSettings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        // Maybe I should throw error? This should be fine in tests
        return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), jsonSettings);
    }
}