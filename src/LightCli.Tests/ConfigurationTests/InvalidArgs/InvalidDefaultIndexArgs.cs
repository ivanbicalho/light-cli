using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.InvalidArgs
{
    public class InvalidDefaultIndexArgs : IArgs
    {
        [IndexArg(1, required: false, defaultValue: "test")]
        public int Arg1 { get; set; }
    }
}
