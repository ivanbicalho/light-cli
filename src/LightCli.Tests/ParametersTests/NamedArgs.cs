using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Tests.ParametersTests
{
    public class NamedArgs : IArgs
    {
        [NamedArg(shortName: "-id", fullName: "--identifier")]
        public int Id { get; set; }

        [NamedArg(shortName: "-n", fullName: "--name", required: false, defaultValue: "test")]
        public string Name { get; set; }
    }
}