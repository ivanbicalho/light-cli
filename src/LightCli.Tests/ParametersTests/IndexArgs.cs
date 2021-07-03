using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ParametersTests
{
    public class IndexArgs : IArgs
    {
        [IndexArg(0)]
        public int Id { get; set; }

        [IndexArg(1, required: false, defaultValue: "test")]
        public string Name { get; set; }
    }
}