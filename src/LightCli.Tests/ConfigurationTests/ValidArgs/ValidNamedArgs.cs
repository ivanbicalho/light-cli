using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.ValidArgs
{
    public class ValidNamedArgs : IArgs
    {
        [NamedArg("-a", fullName: "--arg")]
        public int Arg1 { get; set; }

        [NamedArg(shortName: "-a2")]
        public string Arg2 { get; set; }

        [NamedArg(fullName: "--arg3")]
        public string Arg3 { get; set; }
    }
}