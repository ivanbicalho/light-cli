using LightCli.Commands;

namespace LightCli.Results
{
    /// <summary>
    /// Result of the runner execution when commands were used
    /// </summary>
    public class CliCommandResult
    {
        internal CliCommandResult(bool success, ICommand command, string message)
        {
            Success = success;
            Command = command;
            Message = message;
        }

        public bool Success { get; }
        public ICommand Command { get; }
        public string Message { get; }
    }
}