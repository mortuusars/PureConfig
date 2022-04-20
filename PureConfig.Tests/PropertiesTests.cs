using Xunit;

namespace PureConfig.Tests;

public class PropertiesTests
{
    public TestConfig _config = new();

    [Fact]
    public void PropertyShouldHaveADefaultValueInitially()
    {
        Assert.Equal(0, _config.Number);
        Assert.Equal(string.Empty, _config.String);
    }

    [Fact]
    public void SettingPropertyShouldSetIt()
    {
        _config.Number = 0;
        _config.Number = 42;

        Assert.Equal(42, _config.Number);
    }

    [Fact]
    public void SettingPropertyShouldRaisePropertyChanged()
    {
        string? eventPropertyName = null;
        _config.PropertyChanged += (s, e) => eventPropertyName = e.PropertyName;

        _config.String = "Test";

        Assert.Equal(nameof(TestConfig.String), eventPropertyName);
    }

    [Fact]
    public void NullablePropertiesCanHaveNullValue()
    {
        Assert.NotNull(_config.NullableString);

        _config.NullableString = null;

        Assert.Null(_config.NullableString);
    }

    [Fact]
    public void NonNullablePropertiesCanStillHaveNullValue()
    {
        Assert.NotNull(_config.String);

        _config.String = null;

        Assert.Null(_config.String);
    }
}
