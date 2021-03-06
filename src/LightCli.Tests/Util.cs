using LightCli.Args;
using LightCli.Commands;
using LightCli.Results;
using System.Threading.Tasks;

namespace LightCli.Tests
{
    public static class Util
    {
        public static async Task<CliCommandResult> Run<T>(string[] args) where T : IArgs, new()
        {
            var runner = new CliRunner();

            var command = new GenericCommand<T>();
            runner.AddCommand(command);

            return await runner.Run(args);
        }

        public static async Task<CliCommandResult> Run(string[] args, ICommand command)
        {
            var runner = new CliRunner();
            runner.AddCommand(command);

            return await runner.Run(args);
        }
    }
}