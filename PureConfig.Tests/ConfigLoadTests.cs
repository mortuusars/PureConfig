using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PureConfig.Tests;

public class ConfigLoadTests
{
    private class TestDeserializer : IConfigDeserializer
    {
        private readonly Dictionary<string, object?> _props;

        public TestDeserializer(Dictionary<string, object?> props)
        {
            _props = props;
        }
        
        public Dictionary<string, object?> Deserialize<T>(string serializedString) where T : ConfigBase
        {
            return _props;
        }
    }

    [Fact]
    public void LoadShouldLoadAllProperties()
    {
        Dictionary<string, object?> properties = new()
        {
            { "Number", 99 },
            { "String", "Testing" }
        };

        TestConfig config = new();
        
        Assert.Equal(0, config.Number);
        Assert.Equal("", config.String);

        config.Load<TestConfig>("", new TestDeserializer(properties));

        Assert.Equal(99, config.Number);
        Assert.Equal("Testing", config.String);
    }
}
