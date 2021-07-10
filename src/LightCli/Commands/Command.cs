using LightCli.Args;
using LightCli.Attributes;
using LightCli.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LightCli.Commands
{
    /// <summary>
    /// Represents the command that can be executed
    /// </summary>
    /// <typeparam name="T">Arguments type</typeparam>
    public abstract class Command<T> : ICommand where T : IArgs, new()
    {
        protected Command(int commandIndex = 0)
        {
            if (commandIndex < 0)
                throw new InvalidConfigurationException($"The parameter '{commandIndex}' cannot be less than zero");

            CommandIndex = commandIndex;
        }

        public int CommandIndex { get; }
        public abstract string CommandName { get; }
        public abstract string Description { get; }
        public abstract string ExampleUsage { get; }

        protected abstract Task Run(T args);

        public async Task Run(string[] args)
        {
            var newArgs = Converter<T>.Convert(args);
            await Run(newArgs);
        }

        /// <summary>
        /// Shows a default help message
        /// </summary>
        public void ShowDefaultHelp()
        {
            Console.WriteLine("Available arguments:");
            Console.WriteLine();

            var arguments = Activator.CreateInstance<T>().GetArgumentsInfo();

            if (!arguments.Any())
            {
                Console.WriteLine("No args");
                return;
            }

            foreach (var argument in arguments)
            {
                ShowDefaultHelp(argument);
            }

            if (ExampleUsage != null)
            {
                Console.WriteLine();
                Console.WriteLine($"Example: {ExampleUsage}");
            }

            if (arguments.Any(x => x.Optional))
            {
                Console.WriteLine();
                Console.WriteLine("[] = optional arguments");
            }
        }

        private void ShowDefaultHelp(ArgumentInfo argument)
        {
            if (argument.Optional)
                Console.Write("[");

            if (argument.Type == typeof(IndexArgAttribute))
            {
                Console.Write("Index ");
                Console.Write(argument.Index);
            }
            else
            {
                Console.Write(argument.NamesAsString);
            }

            if (argument.Optional)
                Console.Write("]");

            Console.Write(": ");
            Console.Write(argument.Description);

            if (!argument.Description.EndsWith("."))
                Console.Write(".");

            if (argument.DefaultValue != null)
            {
                Console.Write(" Default value: ");
                Console.Write(argument.DefaultValue);
            }

            Console.WriteLine();
        }
    }
}