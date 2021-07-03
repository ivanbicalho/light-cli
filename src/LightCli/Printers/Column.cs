using System;
using System.Collections.Generic;
using System.Text;

namespace LightCli.Printers
{
    internal class Column
    {
        public static string Divisor => " | ";
        public static int FullSize => -1;

        public Column(string text, int maxSize = -1, string postTextWhenBreak = null, ConsoleColor color = ConsoleColor.White)
        {
            OriginalText = text;
            MaxSize = maxSize;
            PostTextWhenBreak = postTextWhenBreak ?? string.Empty;
            Color = color;
        }

        public string OriginalText { get; }
        public string Text => BreakOriginalText(OriginalText, MaxSize, PostTextWhenBreak);
        public int MaxSize { get; internal set; }
        public string PostTextWhenBreak { get; }
        public ConsoleColor Color { get; }

        public void Print(bool isHeaderRow = false)
        {
            if (isHeaderRow)
            {
                Console.Write(Text);
                return;
            }

            Console.ForegroundColor = Color;
            Console.Write(Text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static string BreakOriginalText(string text, int maxSize, string postTextWhenBreak)
        {
            if (text.Length > maxSize)
            {
                if (text.Length > postTextWhenBreak.Length)
                    return string.Concat(text[..(maxSize - postTextWhenBreak.Length)], postTextWhenBreak);

                return postTextWhenBreak;
            }

            while (text.Length < maxSize)
            {
                text = string.Concat(text, " ");
            }

            return text;
        }
    }
}
