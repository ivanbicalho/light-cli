﻿using LightCli.Attributes;
using System.Collections.Generic;
using System.Reflection;

namespace LightCli.Args
{
    /// <summary>
    /// Interface for argument class
    /// </summary>
    public interface IArgs
    {
        /// <summary>
        /// Implement this method if you want to do an extra validation in the argument class
        /// </summary>
        /// <returns>true if arguments are valid, otherwise false</returns>
        bool Validate() => true;

        /// <summary>
        /// Gets the information of all arguments
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ArgumentInfo> GetArgumentsInfo()
        {
            foreach (var property in GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                foreach (var attribute in property.GetCustomAttributes())
                {
                    if (attribute is IndexArgAttribute || attribute is NamedArgAttribute)
                        yield return new ArgumentInfo(property, attribute);
                }
            }
        }
    }
}