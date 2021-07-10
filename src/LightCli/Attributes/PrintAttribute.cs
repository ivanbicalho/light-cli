using System;

namespace LightCli.Attributes
{
    /// <summary>
    /// Indicate a field as printable on the table
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrintAttribute : Attribute
    {
        /// <summary>
        /// Print constructor
        /// </summary>
        /// <param name="order">Order in which the field will be printed on the table</param>
        /// <param name="title">Title of this column on the table, default is the name of the property</param>
        /// <param name="maxSize">Maximum size of this column. If the column value is greater than this value, it will be truncated, default is infinite</param>
        /// <param name="postTextWhenBreak">If the value was truncated, this value will be appended at the end, ex: "..."</param>
        /// <param name="color">Color of the all values of this column, default is gray</param>
        public PrintAttribute(int order, string title = null, int maxSize = -1, string postTextWhenBreak = null, ConsoleColor color = ConsoleColor.Gray)
        {
            Order = order;
            Title = title;
            MaxSize = maxSize;
            PostTextWhenBreak = postTextWhenBreak;
            Color = color;
        }

        public int Order { get; }
        public string Title { get; internal set; }
        public int MaxSize { get; }
        public string PostTextWhenBreak { get; }
        public ConsoleColor Color { get; }
    }
}