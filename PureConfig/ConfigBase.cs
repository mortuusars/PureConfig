using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

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

    #region Serialization

    [JsonIgnore]
    public virtual IConfigSerializer? Serializer { get; set; }

    public virtual string? Serialize()
    {
        return Serializer?.Serialize(this);
    }

    #endregion

    [JsonIgnore]
    public virtual IConfigDeserializer? Deserializer { get; set; }



    protected virtual void Load(Dictionary<string, object?> propertyValues)
    {
        foreach (var prop in _properties)
        {
            if (propertyValues.TryGetValue(prop.Key, out object? value))
                _properties[prop.Key] = value;
        }
    }
}
