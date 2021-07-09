using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.InvalidArgs
{
    public class InvalidRequiredIndexArgsA : IArgs
    {
        [IndexArg(1, required: false)]
        public int Arg1 { get; set; }

        [IndexArg(2)]
        public int Arg2 { get; set; }
    }

    public class InvalidRequiredIndexArgsB : IArgs
    {
        [IndexArg(1)]
        public int Arg1 { get; set; }

        [IndexArg(2, required: false)]
        public int Arg2 { get; set; }

        [IndexArg(3)]
        public int Arg3 { get; set; }
    }

    public class InvalidRequiredIndexArgsC : IArgs
    {
        [IndexArg(1, required: true, defaultValue: "1")]
        public int Arg1 { get; set; }
    }
}