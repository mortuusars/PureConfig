namespace PureConfig;

/// <summary>
/// Serializes config to the string.
/// </summary>
public interface IConfigSerializer
{
    /// <summary>
    /// Serializes config instance to the string.
    /// </summary>
    /// <typeparam name="T">Type of config to serialize.</typeparam>
    /// <param name="config">Object to serialize.</param>
    /// <returns>Serialized string or <see langword="null"/> if failed.</returns>
    string? Serialize<T>(T config) where T : ConfigBase;
}
