using System;

namespace LightCli.Exceptions
{
    internal class CliException : Exception
    {
        public CliException(string message)
            : base(message)
        {
        }
    }
}
