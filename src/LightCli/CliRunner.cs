using LightCli.Args;
using LightCli.Commands;
using LightCli.Exceptions;
using LightCli.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightCli
{
    /// <summary>
    /// Runner configuration class
    /// </summary>
    public class CliRunner
    {
        private readonly List<ICommand> _commands = new List<ICommand>();

        public IEnumerable<ICommand> Commands => _commands;

        /// <summary>
        /// Register a new command on the Runner
        /// </summary>
        /// <param name="command">Command to register</param>
        /// <returns></returns>
        public CliRunner AddCommand(ICommand command)
        {
            if (_commands.Exists(c => c.CommandName == command.CommandName))
                throw new InvalidOperationException($"Command '{command.CommandName}' already registered");

            _commands.Add(command);

            return this;
        }


        /// <summary>
        /// Entry point to run the program considering the registered commands
        /// </summary>
        /// <param name="args">string args[] from Main class</param>
        /// <returns>Result of the execution</returns>
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

        /// <summary>
        /// Entry point to run the program considering no commands
        /// </summary>
        /// <typeparam name="T">Arguments type</typeparam>
        /// <param name="args">string args[] from Main class</param>
        /// <param name="action"></param>
        /// <returns>Result of the execution</returns>
        public async Task<CliNoCommandResult> RunWithoutCommand<T>(string[] args, Func<T, Task> action) where T : IArgs, new()
        {
            var arguments = Activator.CreateInstance<T>().GetArgumentsInfo();

            if ((args == null || args.Length == 0) && typeof(T) != typeof(NoArgs))
                return new CliNoCommandResult(false, arguments, $"Arguments cannot be null or empty");

            try
            {
                var newArgs = Converter<T>.Convert(args);
                await action(newArgs);
                return new CliNoCommandResult(true, arguments);
            }
            catch (CliException ex)
            {
                return new CliNoCommandResult(false, arguments, ex.Message);
            }
        }

        /// <summary>
        /// Shows default message with available registered commands
        /// </summary>
        public void ShowDefaultAvailableCommandsMessage()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine();

            foreach (var command in Commands)
            {
                Console.Write(command.CommandName);
                Console.Write(": ");
                Console.Write(command.Description);
                Console.WriteLine();
            }
        }
    }
}