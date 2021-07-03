using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.ValidArgs
{
    public class ValidDefaultNamedArgs : IArgs
    {
        [NamedArg("-a", required: false, defaultValue: "1")]
        public int Arg1 { get; set; }

        [NamedArg("-b", required: false, defaultValue: "test")]
        public string Arg2 { get; set; }
    }
}
