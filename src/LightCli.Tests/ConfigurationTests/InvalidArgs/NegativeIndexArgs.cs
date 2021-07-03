using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.InvalidArgs
{
    public class NegativeIndexArgs : IArgs
    {
        [IndexArg(-1)]
        public int Arg1 { get; set; }
    }
}
