// See https://aka.ms/new-console-template for more information

using PureConfig;
using PureConfig.Demo;

Config config = new Config((s) => Console.WriteLine(s));
config.MagicNumber = 42;

config.Serializer = new JsonConfigSerializer(new System.Text.Json.JsonSerializerOptions() { WriteIndented = true });
string? json = config.Serialize();
Console.WriteLine("Serialized config:\n" + json);

//Config newConfig = 

Console.ReadLine();