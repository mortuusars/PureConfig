using System;
using System.Text.Json;

namespace PureConfig;

/// <summary>
/// Simple config serializer that uses <see cref="JsonSerializer"/> to serialize specified config instance to a json string.
/// </summary>
public class JsonConfigSerializer : IConfigSerializer
{
    private readonly JsonSerializerOptions _options;

    /// <summary>
    /// Creates new instance of <see cref="JsonConfigSerializer"/>.
    /// </summary>
    public JsonConfigSerializer()
    {
        _options = new JsonSerializerOptions();
    }

    /// <summary>
    /// Creates new instance of <see cref="JsonConfigSerializer"/> with specified <see cref="JsonSerializerOptions"/>.
    /// </summary>
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
