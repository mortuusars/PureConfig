using System.Collections.Generic;

namespace PureConfig;

/// <summary>
/// Deserializes serialized string to config instance.
/// </summary>
public interface IConfigDeserializer
{
    /// <summary>
    /// Deserializes string to the instance of specified config type.
    /// </summary>
    /// <typeparam name="T">Type of config.</typeparam>
    /// <param name="serializedString">String to deserialize.</param>
    /// <returns>Instance of deserialized config.</returns>
    Dictionary<string, object?> Deserialize<T>(string serializedString) where T : ConfigBase;
}