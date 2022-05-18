using System.Text.Json;

namespace PureConfig.Demo;

internal class Config : ConfigBase
{
    public int MagicNumber { get => GetValue(default(int)); set => SetValue(value); }
    public string MagicString { get => GetValue(string.Empty); set => SetValue(value); }

    public Config(Action<string> propertyChangedPrinter)
    {
        PropertyChanged += (s, e) => propertyChangedPrinter.Invoke(
            $"{e.PropertyName} is changed to {this.GetType().GetProperty(e.PropertyName ?? string.Empty)?.GetValue(this)}.");
    }

    public Config()
    {

    }

    public string? Serialize()
    {
        return base.Serialize(new JsonConfigSerializer(new JsonSerializerOptions() { WriteIndented = true }));
    }

    public void Load(string json)
    {
        var properties = new JsonConfigDeserializer().Deserialize<Config>(json);
        SetProperties(properties);
    }
}
