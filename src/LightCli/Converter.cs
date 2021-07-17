using LightCli.Args;
using LightCli.Attributes;
using LightCli.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LightCli
{
    internal static class Converter<T> where T : IArgs, new()
    {
        public static T Convert(string[] args)
        {
            var arguments = new T();
            ValidateType(arguments);

            foreach (var property in arguments.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                SetPropertyValue(args, property, arguments);
            }

            var message = arguments.Validate();

            if (!string.IsNullOrWhiteSpace(message))
                throw new CliException(message);

            return arguments;
        }

        private static void SetPropertyValue(string[] args, PropertyInfo property, T arguments)
        {
            foreach (var attribute in property.GetCustomAttributes(false))
            {
                if (attribute is IndexArgAttribute indexArg)
                    SetIndexArg(arguments, property, indexArg, args);
                else if (attribute is NamedArgAttribute namedArg)
                    SetNamedArg(arguments, property, namedArg, args);
            }
        }

        private static void SetValue(T arguments, PropertyInfo property, string value, string argName)
        {
            try
            {
                if (property.PropertyType == typeof(Uri))
                {
                    var uri = new Uri(value);
                    property.SetValue(arguments, uri);
                    return;
                }

                var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                var safeValue = (value == null) ? null : System.Convert.ChangeType(value, type);
                property.SetValue(arguments, safeValue, null);
            }
            catch
            {
                throw new CliException($"Invalid value '{value}' for {argName}");
            }
        }

        private static string GetValue(IReadOnlyList<string> args, int parameterIndex)
        {
            if (parameterIndex == -1)
                return null;

            var valueIndex = parameterIndex + 1;

            if (args.Count > valueIndex)
                return args[valueIndex];

            return null;
        }

        private static void SetIndexArg(T arguments, PropertyInfo property, IndexArgAttribute indexArg, string[] args)
        {
            var argName = $"index {indexArg.Index}";

            if (args.Length <= indexArg.Index)
            {
                if (indexArg.Required)
                    throw new CliException($"Argument {argName} is required");

                if (indexArg.HasDefaultValue)
                    SetValue(arguments, property, indexArg.DefaultValue, argName);

                return;
            }

            SetValue(arguments, property, args[indexArg.Index], argName);
        }

        private static void SetNamedArg(T arguments, PropertyInfo property, NamedArgAttribute namedArg,
            string[] args)
        {
            var argsList = args?.ToList() ?? new List<string>();
            var shortNameIndex = -1;
            var fullNameIndex = -1;
            var hasManyShortName = false;
            var hasManyFullName = false;

            if (namedArg.HasShortName)
            {
                hasManyShortName = argsList.Count(x => x == namedArg.ShortName) > 1;
                shortNameIndex = argsList.FindIndex(arg => namedArg.ShortName.Equals(arg));
            }

            if (namedArg.HasFullName)
            {
                hasManyFullName = argsList.Count(x => x == namedArg.FullName) > 1;
                fullNameIndex = argsList.FindIndex(arg => namedArg.FullName.Equals(arg));
            }

            var hasManyNames = shortNameIndex != -1 && fullNameIndex != -1;

            if (hasManyShortName || hasManyFullName || hasManyNames)
                throw new CliException($"There are more than one value for '{namedArg.NamesAsString}'");

            var value = shortNameIndex != -1 ? GetValue(argsList, shortNameIndex) : GetValue(argsList, fullNameIndex);

            if (value != null)
                SetValue(arguments, property, value, namedArg.NamesAsString);
            else if (namedArg.Required)
                throw new CliException($"Argument {namedArg.NamesAsString} is required");
            else if (namedArg.HasDefaultValue)
                SetValue(arguments, property, namedArg.DefaultValue, namedArg.NamesAsString);
        }

        private static void ValidateType(T arguments)
        {
            var attributes = new List<Attribute>();

            foreach (var property in arguments.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                ValidateProperty(property, attributes);
            }

            ValidateAttributes(attributes);

            ValidateIndexArgs(attributes);

            ValidateNamedArgs(attributes);
        }

        private static void ValidateNamedArgs(IEnumerable<Attribute> attributes)
        {
            var named = attributes.OfType<NamedArgAttribute>();
            var allNames = named.SelectMany(x => x.Names);

            if (allNames.Distinct().Count() != allNames.Count())
                throw new InvalidConfigurationException($"Invalid names. Short and full names have to be unique");
        }

        private static void ValidateIndexArgs(IEnumerable<Attribute> attributes)
        {
            var indexes = attributes.OfType<IndexArgAttribute>();
            if (indexes.Select(x => x.Index).Distinct().Count() != indexes.Count())
                throw new InvalidConfigurationException($"Invalid indexes. There are different properties with the same index");

            if (!indexes.Any())
                return;

            var haveToBeOptional = false;
            foreach (var index in indexes)
            {
                if (haveToBeOptional && index.Required)
                    throw new InvalidConfigurationException(
                        $"Invalid configuration for indexes. All indexes have to be optional or if there are any required, they must be first");

                if (!index.Required)
                    haveToBeOptional = true;
            }
        }

        private static void ValidateAttributes(IEnumerable<Attribute> attributes)
        {
            if (!attributes.Any() && typeof(T).FullName != typeof(NoArgs).FullName)
                throw new InvalidConfigurationException(
                    $"There are no attributes on the properties. Did you forget to use {nameof(IndexArgAttribute)} or {nameof(NamedArgAttribute)}? Or maybe you want to use '{typeof(NoArgs).FullName}'");
        }

        private static void ValidateProperty(PropertyInfo property, ICollection<Attribute> attributes)
        {
            bool hasIndex = false, hasNamed = false;
            foreach (var attribute in property.GetCustomAttributes(false))
            {
                if (attribute is IndexArgAttribute indexArg)
                {
                    ValidateIndexArg(indexArg, property);
                    hasIndex = true;
                    attributes.Add(indexArg);
                }
                else if (attribute is NamedArgAttribute namedArg)
                {
                    ValidateNamedArg(namedArg, property);
                    hasNamed = true;
                    attributes.Add(namedArg);
                }
            }

            if (hasIndex && hasNamed)
                throw new InvalidConfigurationException(
                    $"Invalid property '{property.Name}', use only Index or only Named args");
        }

        private static void ValidateNamedArg(NamedArgAttribute namedArg, PropertyInfo property)
        {
            try
            {
                if (namedArg.HasDefaultValue)
                    System.Convert.ChangeType(namedArg.DefaultValue, property.PropertyType);
            }
            catch
            {
                throw new InvalidConfigurationException(
                    $"Invalid default value '{namedArg.DefaultValue}' for '{property.Name}', incompatible types");
            }

            if (!namedArg.HasShortName && !namedArg.HasFullName)
                throw new InvalidConfigurationException(
                    $"The argument '{property.Name}' has to have ShortName, FullName or both");

            if (namedArg.Required && namedArg.HasDefaultValue)
                throw new InvalidConfigurationException(
                    $"'{property.Name}' cannot be required because it's have a default value. Remove default value or set required to false");
        }

        private static void ValidateIndexArg(IndexArgAttribute indexArg, PropertyInfo property)
        {
            if (indexArg.Index < 0)
                throw new InvalidConfigurationException($"Invalid index. Index has to be a non-negative number");

            try
            {
                if (indexArg.HasDefaultValue)
                    System.Convert.ChangeType(indexArg.DefaultValue, property.PropertyType);
            }
            catch
            {
                throw new InvalidConfigurationException(
                    $"Invalid default value '{indexArg.DefaultValue}' for '{property.Name}', incompatible types");
            }

            if (indexArg.Required && indexArg.HasDefaultValue)
                throw new InvalidConfigurationException(
                    $"'{property.Name}' cannot be required because it's have a default value. Remove default value or set required to false");
        }
    }
}