﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace PureConfig;

public class JsonConfigDeserializer
{
    /// <summary>
    /// Deserializes json string to a dictionary of property names and its values.
    /// </summary>
    /// <typeparam name="T">Type of config.</typeparam>
    /// <param name="json">Serialized config json string.</param>
    /// <exception cref="JsonException"/>
    public Dictionary<string, object?> Deserialize<T>(string json) where T : ConfigBase
    {
        ArgumentNullException.ThrowIfNull(json);

        Dictionary<string, object?> result = new();

        PropertyInfo[] configProperties = typeof(T).GetProperties();
        if (configProperties.Length == 0)
            return result;

        using (var document = JsonDocument.Parse(json))
        {
            foreach (var jsonElement in document.RootElement.EnumerateObject())
            {
                try
                {
                    Type propertyType = configProperties.First(p => p.Name.Equals(jsonElement.Name)).PropertyType;
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