using System;

namespace LightCli.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IndexArgAttribute : Attribute
    {
        public IndexArgAttribute(int index, string description = null, bool required = true, string defaultValue = null)
        {
            Index = index;
            Description = description;
            Required = required;
            DefaultValue = defaultValue;
        }

        public bool Required { get; }
        public string DefaultValue { get; }
        public int Index { get; }
        public string Description { get; }
        public bool HasDefaultValue => DefaultValue != null;
    }
}
