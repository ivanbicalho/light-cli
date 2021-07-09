using LightCli.Printers.Columns;
using System;
using System.Collections.Generic;

namespace LightCli.Printers.Rows
{
    internal class Row
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

        public List<Column> Columns { get; } = new List<Column>();
    }
}