using Newtonsoft.Json;

namespace TrainManagementSystem.Extensions;

public static class SerializerExtensions
{
    public static string Serialize<TObject>(this TObject obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
    }
}
