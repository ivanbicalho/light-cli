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
    public static class TablePrinter
    {
        public static void Print<T>(IEnumerable<T> items) where T : class
        {
            if (items == null || !items.Any())
                return;

            var table = new Table();
            var properties = new List<Property>();

            SetHeadersAndGetProperties<T>(table, properties);

            ValidateProperties(properties);

            SetRows(table, properties, items);

            table.Print();
        }

        private static void ValidateProperties(IReadOnlyCollection<Property> properties)
        {
            var countDistinctOrders = properties.Select(x => x.Attribute.Order).Distinct().Count();

            if (properties.Count != countDistinctOrders)
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

        private static void SetHeadersAndGetProperties<T>(Table table, ICollection<Property> properties)
            where T : class
        {
            foreach (var property in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var attribute = (PrintAttribute)property.GetCustomAttributes(typeof(PrintAttribute)).FirstOrDefault();

                if (attribute == null)
                    continue;

                var text = attribute.Title ??= property.Name;
                table.Header.Columns.Add(new HeaderColumn(text, attribute.MaxSize, attribute.PostTextWhenBreak, attribute.Color));

                properties.Add(new Property { Attribute = attribute, PropertyInfo = property });
            }
        }

        private static void SetRows<T>(Table table, IReadOnlyCollection<Property> properties, IEnumerable<T> items)
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