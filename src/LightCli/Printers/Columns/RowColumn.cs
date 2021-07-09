using LightCli.Attributes;
using LightCli.Printers.Custom;
using System;
using System.Reflection;

namespace LightCli.Printers.Columns
{
    internal class RowColumn<T> : Column
    {
        private T _item;
        private ICustomColor<T> _customColor;
        private ICustomFormat<T> _customFormat;
        private PropertyInfo _propertyInfo;

        public RowColumn(T item, PropertyInfo propertyInfo, PrintAttribute attribute)
        {
            _item = item;
            _customColor = item as ICustomColor<T>;
            _customFormat = item as ICustomFormat<T>;
            _propertyInfo = propertyInfo;

            OriginalText = _customFormat?.CustomFormat(propertyInfo.Name, _item) ?? propertyInfo.GetValue(item).ToString();
            MaxSize = attribute.MaxSize;
            PostTextWhenBreak = attribute.PostTextWhenBreak ?? string.Empty;
            Color = attribute.Color;
        }

        public override void Print()
        {
            Console.ForegroundColor = _customColor?.CustomColor(_propertyInfo.Name, _item) ?? Color;
            Console.Write(Text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}