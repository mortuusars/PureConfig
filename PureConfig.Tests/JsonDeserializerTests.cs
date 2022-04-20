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
        config.Serializer = new JsonConfigSerializer();
        string json = config.Serialize()!;
        var dictionary = new JsonConfigDeserializer().Deserialize<TestConfig>(json);

        // Do not count properties with JsonIgnore attribute:
        var properties = config.GetType().GetProperties()
            .Where(p => !p.CustomAttributes.Any(a => a.AttributeType == typeof(JsonIgnoreAttribute)));

        Assert.Equal(properties.Count(), dictionary.Count);

        foreach (var item in dictionary)
        {
            var value = config.GetType().GetProperty(item.Key)?.GetValue(config);
            Assert.Equal(item.Value, value);
        }
    }
}
