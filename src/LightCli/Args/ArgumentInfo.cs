using LightCli.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

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
    }
}