using System;

namespace LightCli.Attributes
{
    /// <summary>
    /// Argument obtained with index
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IndexArgAttribute : Attribute
    {
        /// <summary>
        /// IndexArg constructor 
        /// </summary>
        /// <param name="index">Index to bind the argument</param>
        /// <param name="description">Argument description. Useful when showing help</param>
        /// <param name="required">Set the argument as required or not</param>
        /// <param name="defaultValue">Set the default value for the argument. Can only be used if required is false</param>
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