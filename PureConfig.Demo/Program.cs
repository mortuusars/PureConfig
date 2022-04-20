// See https://aka.ms/new-console-template for more information

using PureConfig;
using PureConfig.Demo;

Config config = new Config((s) => Console.WriteLine(s));
config.MagicNumber = 42;

string? json = config.Serialize();
Console.WriteLine("Serialized config:\n" + json);

Config newCfg = new Config((s) => { });

Console.WriteLine("New config before loading: \n" + newCfg.Serialize());
newCfg.Load(json!);
Console.WriteLine("New config after loading:\n" + newCfg.Serialize());

Console.ReadLine();