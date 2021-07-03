using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.ValidArgs
{
    public class ValidIndexArgs : IArgs
    {
        [IndexArg(1)]
        public int Arg1 { get; set; }

        [IndexArg(2)]
        public string Arg2 { get; set; }
    }
}
