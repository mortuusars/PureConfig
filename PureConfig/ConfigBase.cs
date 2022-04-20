using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PureConfig;

public abstract class ConfigBase : INotifyPropertyChanged
{
    #region PropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    #region Properties

    private readonly Dictionary<string, object?> _properties = new();

    /// <summary>
    /// Gets value of the property by its name.
    /// </summary>
    /// <typeparam name="T">Property type.</typeparam>
    /// <param name="defaultValue">Default value of the property.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns>Value of the property.</returns>
    protected T GetValue<T>(T defaultValue, [CallerMemberName] string? propertyName = null)
    {
        Debug.Assert(propertyName is not null, "PropertyName is null");

        if (propertyName is null)
            return defaultValue;

        if (_properties.TryGetValue(propertyName, out object? value))
            return (T)value!;
        
        return defaultValue;

    }

    /// <summary>
    /// Sets property to a new value. If new value is different from the old one - PropertyChanged event will be raised.
    /// </summary>
    /// <typeparam name="T">Type of value.</typeparam>
    /// <param name="value">New value of the property.</param>
    /// <param name="propertyName">Name of the property.</param>
    protected void SetValue<T>(T value, [CallerMemberName] string? propertyName = null)
    {
        Debug.Assert(propertyName is not null, "PropertyName is null");

        if (propertyName is null)
            return;

        if (_properties.TryGetValue(propertyName, out object? oldValue) && oldValue?.Equals(value) is true)
            return;

        _properties[propertyName] = value;
        OnPropertyChanged(propertyName);
    }

    #endregion

    /// <summary>
    /// Serializes current instance of config using provided config serializer.
    /// </summary>
    /// <param name="serializer">Serializer implementation that would be used to serialize this config instance.</param>
    /// <returns>Serialized string or <see langword="null"/>.</returns>
    public virtual string? Serialize(IConfigSerializer serializer)
    {
        ArgumentNullException.ThrowIfNull(serializer);
        return serializer.Serialize(this);
    }

    /// <summary>
    /// Sets internal properties to the values deserialized by provided config deserializer.
    /// </summary>
    /// <param name="serializedConfig">Serialized config string.</param>
    /// <param name="deserializer">Deserializer implementation that will be used to deserialize provided serialized string.</param>
    public virtual void Load<T>(string serializedConfig, IConfigDeserializer deserializer) where T : ConfigBase
    {
        ArgumentNullException.ThrowIfNull(serializedConfig);
        ArgumentNullException.ThrowIfNull(deserializer);
        Load(deserializer.Deserialize<T>(serializedConfig));
    }

    /// <summary>
    /// Sets internal properties to the values from provided dictionary.
    /// </summary>
    /// <param name="propertyValues">Dictionary of property names and their values.</param>
    protected virtual void Load(IDictionary<string, object?> propertyValues)
    {
        ArgumentNullException.ThrowIfNull(propertyValues);

        foreach (var prop in propertyValues)
        {
            if (this.GetType().GetProperty(prop.Key) is null)
                continue; // Property is not found on this config class.

            if (propertyValues.TryGetValue(prop.Key, out object? value))
                _properties[prop.Key] = value;
        }
    }
}
