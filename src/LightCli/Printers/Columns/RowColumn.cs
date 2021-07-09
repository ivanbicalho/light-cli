using LightCli.Printers.Custom;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightCli.Printers.Columns
{
    internal class RowColumn<T> : Column
    {
        private string _propertyName;
        private T _item;
        private ICustomColor<T> _customColor;
        private ICustomFormat<T> _customFormat;

        public RowColumn(T item, string propertyName, string text, int maxSize = -1, string postTextWhenBreak = null, ConsoleColor color = ConsoleColor.White)
        {
            OriginalText = text;
            MaxSize = maxSize;
            PostTextWhenBreak = postTextWhenBreak ?? string.Empty;
            Color = color;

            _propertyName = propertyName;
            _item = item;
            _customColor = item as ICustomColor<T>;
            _customFormat = item as ICustomFormat<T>;
        }

        public override void Print()
        {
            Console.ForegroundColor = _customColor?.CustomColor(_propertyName, _item) ?? Color;
            Console.Write(_customFormat?.CustomFormat(_propertyName, _item) ?? Text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
