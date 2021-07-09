using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.InvalidArgs
{
    public class NamelessNamedArgs : IArgs
    {
        [NamedArg]
        public int Arg1 { get; set; }
    }
}