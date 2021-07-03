using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.ValidArgs
{
    public class ValidRequiredIndexArgsA : IArgs
    {
        [IndexArg(1)]
        public int Arg1 { get; set; }

        [IndexArg(2, required: false)]
        public int Arg2 { get; set; }
    }

    public class ValidRequiredIndexArgsB : IArgs
    {
        [IndexArg(1, required: false)]
        public int Arg1 { get; set; }

        [IndexArg(2, required: false)]
        public int Arg2 { get; set; }

        [IndexArg(3, required: false)]
        public int Arg3 { get; set; }
    }
}
