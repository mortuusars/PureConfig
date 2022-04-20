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
    /// <param name="serializedConfig">String to deserialize.</param>
    /// <returns>Instance of deserialized config.</returns>
    T Deserialize<T>(string serializedConfig) where T : ConfigBase;
}