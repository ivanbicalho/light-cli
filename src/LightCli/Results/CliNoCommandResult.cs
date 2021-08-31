using LightCli.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using LightCli.Extensions;

namespace LightCli.Results
{
    /// <summary>
    /// Result of the runner execution when no commands were used
    /// </summary>
    public class CliNoCommandResult
    {
        private readonly IEnumerable<ArgumentInfo> _arguments;

        internal CliNoCommandResult(bool success, IEnumerable<ArgumentInfo> arguments, string message = null)
        {
            Success = success;
            Message = message;
            _arguments = arguments;
        }

        public bool Success { get; }
        public string Message { get; }

        public void ShowDefaultArgsHelp()
        {
            Console.WriteLine("Available arguments:");

            if (!_arguments.Any())
            {
                Console.WriteLine("No args");
                return;
            }

            foreach (var argument in _arguments)
            {
                ConsoleExtensions.WriteTab();
                Console.WriteLine(argument.GetDefaultHelpMessage());
            }

            if (_arguments.Any(x => x.Optional))
            {
                Console.WriteLine();
                Console.WriteLine("[] = optional arguments");
            }
        }
    }
}