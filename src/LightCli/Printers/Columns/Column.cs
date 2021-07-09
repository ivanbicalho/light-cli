using System;
using System.Collections.Generic;
using System.Text;

namespace LightCli.Printers.Columns
{
    internal abstract class Column
    {
        public static int FullSize => -1;

        public string OriginalText { get; protected set; }
        public string Text => BreakOriginalText(OriginalText, MaxSize, PostTextWhenBreak);
        public int MaxSize { get; set; }
        public string PostTextWhenBreak { get; protected set; }
        public ConsoleColor Color { get; protected set; }
        public abstract void Print();

        protected static string BreakOriginalText(string text, int maxSize, string postTextWhenBreak)
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
