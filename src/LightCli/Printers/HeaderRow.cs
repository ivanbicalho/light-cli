using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightCli.Printers
{
    internal class HeaderRow : Row
    {
        public HeaderRow()
        {
            IsHeaderRow = true;
        }
    }
}
