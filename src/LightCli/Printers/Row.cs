using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightCli.Printers
{
    internal class Row
    {
        protected internal bool IsHeaderRow = false;

        public void Print()
        {
            foreach (var column in Columns)
            {
                column.Print(IsHeaderRow);
                Console.Write(Column.Divisor);
            }

            Console.WriteLine();
        }

        public void PrintDivisorLine()
        {
            var lineSize = Columns.Sum(x => x.Text.Length) + Columns.Count * Column.Divisor.Length - 1;

            for (var i = 0; i < lineSize; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine();
        }

        public List<Column> Columns { get; } = new List<Column>();
    }
}
