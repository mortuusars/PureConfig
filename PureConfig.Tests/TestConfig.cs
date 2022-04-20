namespace PureConfig.Tests;

public class TestConfig : ConfigBase
{
    public int Number { get => GetValue(default(int)); set => SetValue(value); }
    public string String { get => GetValue(string.Empty); set => SetValue(value); }
    public string? NullableString { get => GetValue(string.Empty); set => SetValue(value); }
}
