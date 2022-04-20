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

    public string? Serialize()
    {
        return new JsonConfigSerializer(new System.Text.Json.JsonSerializerOptions() { WriteIndented = true }).Serialize(this);
    }

    public void Load(string json)
    {
        var values = new JsonConfigDeserializer().Deserialize<Config>(json);
        Load(values);
    }
}
