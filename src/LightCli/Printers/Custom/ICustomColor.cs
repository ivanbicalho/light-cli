using System;
using System.Collections.Generic;
using System.Text;

namespace LightCli.Printers.Custom
{
    public interface ICustomColor<in T>
    {
        ConsoleColor? CustomColor(string propertyName, T customer);
    }
}
