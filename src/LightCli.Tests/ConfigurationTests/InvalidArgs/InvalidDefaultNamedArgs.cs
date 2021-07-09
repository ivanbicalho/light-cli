using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.InvalidArgs
{
    public class InvalidDefaultNamedArgs : IArgs
    {
        [NamedArg("-a", required: false, defaultValue: "test")]
        public int Arg1 { get; set; }
    }
}