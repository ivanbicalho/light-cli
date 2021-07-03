using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ConfigurationTests.ValidArgs
{
    public class ValidDefaultIndexArgs : IArgs
    {
        [IndexArg(1, required: false, defaultValue: "1")]
        public int Arg1 { get; set; }

        [IndexArg(2, required:false, defaultValue:"test")]
        public string Arg2 { get; set; }
    }
}
