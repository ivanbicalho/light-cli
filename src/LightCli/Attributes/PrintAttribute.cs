using System;

namespace LightCli.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PrintAttribute : Attribute
    {
        public PrintAttribute(int order, string title = null, int maxSize = -1, string postTextWhenBreak = null, string toStringFormat = null, ConsoleColor color = ConsoleColor.Gray)
        {
            Order = order;
            Title = title;
            MaxSize = maxSize;
            PostTextWhenBreak = postTextWhenBreak;
            ToStringFormat = toStringFormat;
            Color = color;
        }

        public int Order { get; }
        public string Title { get; internal set; }
        public int MaxSize { get; }
        public string PostTextWhenBreak { get; }
        public string ToStringFormat { get; }
        public ConsoleColor Color { get; }
    }
}