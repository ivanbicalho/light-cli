using System;

namespace LightCli.Printers.Custom
{
    /// <summary>
    /// Color customizations
    /// </summary>
    /// <typeparam name="T">Type of the class that has [Print] attributes</typeparam>
    public interface ICustomColor<in T>
    {
        ConsoleColor? CustomColor(string propertyName, T customer);
    }
}