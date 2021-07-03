using LightCli.Commands;

namespace LightCli.Results
{
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
