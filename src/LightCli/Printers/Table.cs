using LightCli.Printers.Columns;
using LightCli.Printers.Rows;
using System.Collections.Generic;
using System.Linq;

namespace LightCli.Printers
{
    internal class Table
    {
        internal static readonly string Divisor = " | ";

        public void Print()
        {
            SetMaxSizeColumns();

            Header.Print();
            Header.PrintDivisorLine();

            foreach (var row in Rows)
            {
                row.Print();
            }
        }

        private void SetMaxSizeColumns()
        {
            for (var i = 0; i < Header.Columns.Count; i++)
            {
                if (Header.Columns[i].MaxSize != Column.FullSize)
                    continue;

                var maxSize = Header.Columns[i].OriginalText.Length;
                maxSize = Rows.Select(row => row.Columns[i].OriginalText.Length).Prepend(maxSize).Max();

                Header.Columns[i].MaxSize = maxSize;

                foreach (var row in Rows)
                {
                    row.Columns[i].MaxSize = maxSize;
                }
            }
        }

        public List<Row> Rows { get; } = new List<Row>();
        public HeaderRow Header { get; } = new HeaderRow();
    }
}