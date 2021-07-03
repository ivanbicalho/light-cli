using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.InvalidArgs
{
    public class NamedIndexArgs : IArgs
    {
        [NamedArg("-a")]
        [IndexArg(1)]
        public int Arg1 { get; set; }
    }
}
