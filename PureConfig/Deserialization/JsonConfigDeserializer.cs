﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace PureConfig;

/// <summary>
/// Provides the ability to deserializes json string to the dictionary of properties.<br></br>
/// Properties deserialized one by one - if one of the property fails to deserialize, it will be skipped.
/// </summary>
public class JsonConfigDeserializer : IConfigDeserializer
{
    /// <summary>
    /// Deserializes json string to a dictionary of property names and its values.
    /// </summary>
    /// <typeparam name="T">Type of config.</typeparam>
    /// <param name="serializedString">Serialized config json string.</param>
    /// <exception cref="JsonException"/>
    public Dictionary<string, object?> Deserialize<T>(string serializedString) where T : ConfigBase
    {
        ArgumentNullException.ThrowIfNull(serializedString);

        Dictionary<string, object?> result = new();

        PropertyInfo[] configProperties = typeof(T).GetProperties();
        if (configProperties.Length == 0)
            return result;

        using (var document = JsonDocument.Parse(serializedString))
        {
            foreach (var jsonElement in document.RootElement.EnumerateObject())
            {
                try
                {
                    Type? propertyType = configProperties.FirstOrDefault(p => p.Name.Equals(jsonElement.Name))?.PropertyType;

                    if (propertyType is null)
                        continue; // T (config) has no property that matches current json element.

                    object? deserializedProperty = JsonSerializer.Deserialize(jsonElement.Value.GetRawText(), propertyType);
                    result.Add(jsonElement.Name, deserializedProperty);
                }
                catch (ArgumentNullException) { }
                catch (JsonException) { }
                catch (NotSupportedException) { }
            }
        }

        return result;
    }
}
