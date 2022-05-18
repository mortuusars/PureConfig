using System.Collections.Generic;

namespace PureConfig;

/// <summary>
/// Deserializes serialized string to config instance.
/// </summary>
public interface IConfigDeserializer
{
    /// <summary>
    /// Deserializes string to the dictionary of property names and their values.
    /// </summary>
    /// <typeparam name="T">Type of config.</typeparam>
    /// <param name="serializedString">String to deserialize.</param>
    /// <returns>Dictionary PropertyNames and its deserialized Values.</returns>
    Dictionary<string, object?> Deserialize<T>(string serializedString) where T : ConfigBase;
}