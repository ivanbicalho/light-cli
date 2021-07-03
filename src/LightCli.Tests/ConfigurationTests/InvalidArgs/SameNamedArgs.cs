using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.InvalidArgs
{
    public class SameNamedArgsA : IArgs
    {
        [NamedArg("-a", "--arg")]
        public int Arg1 { get; set; }

        [NamedArg("-a", "--arg")]
        public int Arg2 { get; set; }
    }

    public class SameNamedArgsB : IArgs
    {
        [NamedArg("-a", "--arg")]
        public int Arg1 { get; set; }

        [NamedArg("-a", "--otherArg")]
        public int Arg2 { get; set; }
    }

    public class SameNamedArgsC : IArgs
    {
        [NamedArg("-a", "--arg")]
        public int Arg1 { get; set; }

        [NamedArg("-b", "--arg")]
        public int Arg2 { get; set; }
    }
}
