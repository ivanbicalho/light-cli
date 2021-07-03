namespace LightCli.Results
{
    public class CliNoCommandResult
    {
        internal CliNoCommandResult(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}
