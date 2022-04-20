using Xunit;

namespace PureConfig.Tests;

public class SerializedTests
{
    public TestConfig _config = new();

    [Fact]
    public void ConfigShouldSerializeToJsonString()
    {
        _config.Number = 42;
        _config.Serializer = new JsonConfigSerializer();

        string? json = _config.Serialize();

        Assert.NotNull(json);
        Assert.Contains("\"Number\":42", json);
    }

}
