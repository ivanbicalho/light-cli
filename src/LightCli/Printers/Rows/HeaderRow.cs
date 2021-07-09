using LightCli.Printers.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightCli.Printers.Rows
{
    internal class HeaderRow
    {
        public void Print()
        {
            foreach (var column in Columns)
            {
                column.Print();
                Console.Write(Table.Divisor);
            }

            Console.WriteLine();
        }

        public void PrintDivisorLine()
        {
            var lineSize = Columns.Sum(x => x.Text.Length) + Columns.Count * Table.Divisor.Length - 1;

            for (var i = 0; i < lineSize; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine();
        }

        public List<HeaderColumn> Columns { get; } = new List<HeaderColumn>();
    }
}
