using System;

namespace LightCli.Printers.Columns
{
    internal class HeaderColumn : Column
    {
        public HeaderColumn(string text, int maxSize = -1, string postTextWhenBreak = null, ConsoleColor color = ConsoleColor.Gray)
        {
            OriginalText = text;
            MaxSize = maxSize;
            PostTextWhenBreak = postTextWhenBreak ?? string.Empty;
            Color = color;
        }

        public override void Print()
        {
            Console.Write(Text);
        }
    }
}