using System.Linq;
using System.Text.Json.Serialization;
using Xunit;

namespace PureConfig.Tests;

public class JsonDeserializerTests
{
    [Fact]
    public void DeserializeShouldLoadAllProperties()
    {
        TestConfig config = new();
        string json = config.Serialize(new JsonConfigSerializer())!;
        var dictionary = new JsonConfigDeserializer().Deserialize<TestConfig>(json);

        Assert.Equal(config.GetType().GetProperties().Length, dictionary.Count);

        foreach (var item in dictionary)
        {
            var value = config.GetType().GetProperty(item.Key)?.GetValue(config);
            Assert.Equal(item.Value, value);
        }
    }
}
