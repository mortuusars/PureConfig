using System;
using System.Text.Json;

namespace PureConfig;

public class JsonConfigSerializer : IConfigSerializer
{
    private readonly JsonSerializerOptions _options;

    public JsonConfigSerializer()
    {
        _options = new JsonSerializerOptions();
    }

    public JsonConfigSerializer(JsonSerializerOptions options)
    {
        _options = options;
    }

    public string? Serialize<T>(T config) where T : ConfigBase
    {
        try
        {
            Type configType = config.GetType();
            return JsonSerializer.Serialize(config, configType, _options);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
