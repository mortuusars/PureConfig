namespace PureConfig.Demo;

internal class Config : ConfigBase
{
    public int MagicNumber { get => GetValue(default(int)); set => SetValue(value); }
    public string MagicString { get => GetValue(string.Empty); set => SetValue(value); }

    public Config(Action<string> propertyChangedPrinter)
    {
        PropertyChanged += (s, e) => propertyChangedPrinter.Invoke(
            $"{e.PropertyName} is changed to {this.GetType().GetProperty(e.PropertyName ?? String.Empty)?.GetValue(this)}.");
    }

    public void Load()
    {
        var values = new JsonConfigDeserializer().Deserialize<Config>(Serialize()!);
        Load(values);
    }
}
