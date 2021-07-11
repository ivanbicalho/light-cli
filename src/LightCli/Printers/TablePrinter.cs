using LightCli.Attributes;
using LightCli.Exceptions;
using LightCli.Printers.Columns;
using LightCli.Printers.Rows;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace LightCli.Printers
{
    /// <summary>
    /// Table Printer
    /// </summary>
    public static class TablePrinter
    {
        /// <summary>
        /// Prints a list of items. Make sure you set the Print attribute on the properties of this type T
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="items">List of items</param>
        public static void Print<T>(IEnumerable<T> items) where T : class
        {
            if (items == null || !items.Any())
                return;

            var properties = GetProperties<T>();
            ValidateProperties(properties);

            var table = new Table();
            SetHeaders(table, properties);
            SetRows(table, properties, items);

            table.Print();
        }

        private static void ValidateProperties(IEnumerable<Property> properties)
        {
            var countDistinctOrders = properties.Select(x => x.Attribute.Order).Distinct().Count();

            if (properties.Count() != countDistinctOrders)
                throw new InvalidConfigurationException($"Invalid orders. There are different properties with the same order");

            foreach (var property in properties)
            {
                if (property.Attribute.MaxSize != -1 && property.Attribute.MaxSize <= 0)
                    throw new InvalidConfigurationException($"Invalid 'MaxSize' for the property '{property.PropertyInfo.Name}'. Value has to be greater than zero");

                if (!string.IsNullOrEmpty(property.Attribute.PostTextWhenBreak))
                {
                    if (property.Attribute.PostTextWhenBreak.Length > property.Attribute.MaxSize)
                        throw new InvalidConfigurationException(
                            $"The length of 'PostTextWhenBreak' is bigger than 'MaxSize' for the Property '{property.PropertyInfo.Name}'");
                }
            }
        }

        private static IEnumerable<Property> GetProperties<T>()
            where T : class
        {
            var properties = new List<Property>();

            foreach (var property in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var attribute = (PrintAttribute)property.GetCustomAttributes(typeof(PrintAttribute)).FirstOrDefault();

                if (attribute == null)
                    continue;

                properties.Add(new Property { Attribute = attribute, PropertyInfo = property });
            }

            return properties;
        }

        private static void SetHeaders(Table table, IEnumerable<Property> properties)
        {
            foreach (var property in properties.OrderBy(x => x.Attribute.Order))
            {
                var text = property.Attribute.Title ??= property.PropertyInfo.Name;
                table.Header.Columns.Add(new HeaderColumn(text, property.Attribute.MaxSize, property.Attribute.PostTextWhenBreak, property.Attribute.Color));
            }
        }

        private static void SetRows<T>(Table table, IEnumerable<Property> properties, IEnumerable<T> items)
            where T : class
        {
            foreach (var item in items)
            {
                var row = new Row();

                foreach (var property in properties.OrderBy(x => x.Attribute.Order))
                {
                    var text = property.PropertyInfo.GetValue(item).ToString();
                    var column = new RowColumn<T>(item, property.PropertyInfo, property.Attribute);
                    row.Columns.Add(column);
                }

                table.Rows.Add(row);
            }
        }

        private class Property
        {
            public PrintAttribute Attribute { get; set; }
            public PropertyInfo PropertyInfo { get; set; }
        }
    }
}