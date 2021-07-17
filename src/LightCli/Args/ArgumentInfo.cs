using LightCli.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LightCli.Args
{
    public class ArgumentInfo
    {
        internal ArgumentInfo(PropertyInfo property, Attribute attribute)
        {
            PropertyName = property.Name;

            if (attribute is NamedArgAttribute namedArg)
            {
                Type = typeof(NamedArgAttribute);
                Required = namedArg.Required;
                DefaultValue = namedArg.DefaultValue;
                Description = namedArg.Description ?? string.Empty;
                ShortName = namedArg.ShortName;
                FullName = namedArg.FullName;
                Names = namedArg.Names;
                NamesAsString = namedArg.NamesAsString;
            }
            else if (attribute is IndexArgAttribute indexArg)
            {
                Type = typeof(IndexArgAttribute);
                Index = indexArg.Index;
                Required = indexArg.Required;
                DefaultValue = indexArg.DefaultValue;
                Description = indexArg.Description ?? string.Empty;
            }
        }

        public Type Type { get; }
        public string PropertyName { get; }
        public string Description { get; }
        public bool Required { get; }
        public bool Optional => !Required;
        public string DefaultValue { get; }
        public string ShortName { get; }
        public string FullName { get; }
        public IEnumerable<string> Names { get; }
        public string NamesAsString { get; }
        public int? Index { get; }

        public string GetDefaultHelpMessage()
        {
            var message = new StringBuilder();

            if (Optional)
                message.Append("[");

            if (Type == typeof(IndexArgAttribute))
            {
                message.Append("Index ");
                message.Append(Index);
            }
            else
            {
                message.Append(NamesAsString);
            }

            if (Optional)
                message.Append("]");

            message.Append(": ");
            message.Append(Description);

            if (!Description.EndsWith("."))
                message.Append(".");

            if (DefaultValue != null)
            {
                message.Append(" Default value: ");
                message.Append(DefaultValue);
            }

            return message.ToString();
        }
    }
}