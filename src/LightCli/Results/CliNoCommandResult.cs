namespace LightCli.Results
{
    /// <summary>
    /// Result of the runner execution when no commands were used
    /// </summary>
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