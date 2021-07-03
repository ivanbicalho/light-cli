using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.InvalidArgs
{
    public class InvalidRequiredNamedArgs : IArgs
    {
        [NamedArg("-a", required: true, defaultValue: "1")]
        public int Arg1 { get; set; }
    }
}
