using System;
using System.Collections.Generic;

namespace LightCli.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NamedArgAttribute : Attribute
    {
        public NamedArgAttribute(string shortName = null, string fullName = null, string description = null, bool required = true, string defaultValue = null)
        {
            ShortName = shortName;
            FullName = fullName;
            Description = description;
            Required = required;
            DefaultValue = defaultValue;
        }

        public string ShortName { get; }
        public string FullName { get; }
        public string Description { get; }
        public bool Required { get; }
        public string DefaultValue { get; }
        public bool HasShortName => ShortName != null;
        public bool HasFullName => FullName != null;
        public bool HasDefaultValue => DefaultValue != null;

        public IEnumerable<string> Names
        {
            get
            {
                if (HasShortName)
                    yield return ShortName;

                if (HasFullName)
                    yield return FullName;
            }
        }

        public string NamesAsString
        {
            get
            {
                if (HasShortName && HasFullName)
                    return $"{ShortName} | {FullName}";

                if (HasShortName)
                    return ShortName;

                if (HasFullName)
                    return FullName;

                return string.Empty;
            }
        }
    }
}