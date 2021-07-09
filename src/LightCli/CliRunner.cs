using LightCli.Args;
using LightCli.Commands;
using LightCli.Exceptions;
using LightCli.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightCli
{
    public class CliRunner
    {
        private readonly List<ICommand> _commands = new List<ICommand>();

        public IEnumerable<ICommand> Commands => _commands;

        public CliRunner AddCommand(ICommand command)
        {
            if (_commands.Exists(c => c.CommandName == command.CommandName))
                throw new InvalidOperationException($"Command '{command.CommandName}' already registered");

            _commands.Add(command);

            return this;
        }

        public async Task<CliCommandResult> Run(string[] args)
        {
            var resultNotFound = new CliCommandResult(false, null, "Command not found");

            if (args == null || args.Length == 0)
                return resultNotFound;

            foreach (var command in _commands)
            {
                if (args.Length <= command.CommandIndex)
                    continue;

                if (command.CommandName == args[command.CommandIndex])
                {
                    try
                    {
                        await command.Run(args);
                        return new CliCommandResult(true, command, null);
                    }
                    catch (CliException ex)
                    {
                        return new CliCommandResult(false, command, ex.Message);
                    }
                }
            }

            return resultNotFound;
        }

        public async Task<CliNoCommandResult> RunWithoutCommand<T>(string[] args, Func<T, Task> action) where T : IArgs, new()
        {
            if (args == null || args.Length == 0)
                return new CliNoCommandResult(false, $"Arguments cannot be null or empty");

            try
            {
                var newArgs = Converter<T>.Convert(args);
                await action(newArgs);
                return new CliNoCommandResult(true, null);
            }
            catch (CliException ex)
            {
                return new CliNoCommandResult(false, ex.Message);
            }
        }

        public void ShowDefaultAvailableCommandsMessage()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine();

            foreach (var command in Commands)
            {
                Console.Write(command.CommandName);
                Console.Write(": ");
                Console.Write(command.Description);
            }

            Console.WriteLine();
        }
    }
}