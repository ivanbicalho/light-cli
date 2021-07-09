using LightCli.Args;
using LightCli.Commands;
using System.Threading.Tasks;

namespace LightCli.Tests
{
    public class GenericCommand<T> : Command<T> where T : IArgs, new()
    {
        public override string CommandName => "generic";
        public override string Description => "generic command for tests";
        public override string ExampleUsage => CommandName;
        public T Args { get; private set; }

        protected override async Task Run(T args)
        {
            Args = args;
            await Task.FromResult(0);
        }
    }
}