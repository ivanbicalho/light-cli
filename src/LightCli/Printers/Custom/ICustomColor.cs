using System;

namespace LightCli.Printers.Custom
{
    public interface ICustomColor<in T>
    {
        ConsoleColor? CustomColor(string propertyName, T customer);
    }
}