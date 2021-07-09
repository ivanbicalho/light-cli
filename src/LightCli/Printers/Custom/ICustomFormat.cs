using System;
using System.Collections.Generic;
using System.Text;

namespace LightCli.Printers.Custom
{
    public interface ICustomFormat<in T>
    {
        string CustomFormat(string propertyName, T customer);
    }
}
