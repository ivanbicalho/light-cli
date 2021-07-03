using System;

namespace LightCli.Exceptions
{
    public class InvalidConfigurationException : InvalidOperationException
    {
        public InvalidConfigurationException(string message)
            : base(message)
        {
        }
    }
}
