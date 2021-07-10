using System;
using System.Collections.Generic;

namespace LightCli.Attributes
{
    /// <summary>
    /// Argument obtained with name
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NamedArgAttribute : Attribute
    {
        /// <summary>
        /// NamedArg constructor
        /// </summary>
        /// <param name="shortName">Short name for the argument, ex: -n</param>
        /// <param name="fullName">Full name for the argument, ex: --name</param>
        /// <param name="description">Argument description. Useful when showing help</param>
        /// <param name="required">Set the argument as required or not</param>
        /// <param name="defaultValue">Set the default value for the argument. Can only be used if required is false</param>
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

        /// <summary>
        /// List of names (shortName and fullName)
        /// </summary>
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

        /// <summary>
        /// ShortName and fullName as string, joined with the character "|"
        /// </summary>
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