using System;

namespace LightCli.Exceptions
{
    /// <summary>
    /// Represents an invalid Light CLI configuration. Look at the message for more details
    /// </summary>
    public class InvalidConfigurationException : InvalidOperationException
    {
        public InvalidConfigurationException(string message)
            : base(message)
        {
        }
    }
}