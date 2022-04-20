using Xunit;

namespace PureConfig.Tests;

public class SerializedTests
{
    public TestConfig _config = new();

    [Fact]
    public void ConfigShouldSerializeToJsonString()
    {
        _config.Number = 42;
        string? json = _config.Serialize(new JsonConfigSerializer());

        Assert.NotNull(json);
        Assert.Contains("\"Number\":42", json);
    }
}
