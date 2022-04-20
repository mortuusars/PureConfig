using Xunit;

namespace PureConfig.Tests;

public class JsonSerializerTests
{
    [Fact]
    public void SerializeShouldSerializeProperly()
    {
        TestConfig config = new TestConfig();
        config.Number = -1;
        config.String = "asd";
        config.NullableString = null;

        var serializer = new JsonConfigSerializer();
        string? result = serializer.Serialize(config);

        Assert.NotNull(result);
        Assert.Equal("{\"Number\":-1,\"String\":\"asd\",\"NullableString\":null}", result);
    }
}
