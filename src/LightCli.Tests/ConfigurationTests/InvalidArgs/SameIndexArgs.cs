using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.InvalidArgs
{
    public class SameIndexArgs : IArgs
    {
        [IndexArg(1)]
        public int Arg1 { get; set; }

        [IndexArg(1)]
        public int Arg2 { get; set; }
    }
}