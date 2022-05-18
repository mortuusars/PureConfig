# PureConfig

Easy runtime config.

Usage:

Start by inheriting from ConfigBase:
```csharp
public class Config : ConfigBase {}
```

Defining properties and their default values:
```csharp
public int MagicNumber { get => GetValue(default(int)); set => SetValue(value); }
public string MagicString { get => GetValue(string.Empty); set => SetValue(value); }
```

Serializing:
```csharp
string? serializedConfig = config.Serialize(new JsonConfigSerializer());
```

Deserializing:
```csharp
public void Load(string json)
{
    Dictionary<string, object?> properties = new JsonConfigDeserializer().Deserialize<Config>(json);
    SetProperties(properties);
}
```
