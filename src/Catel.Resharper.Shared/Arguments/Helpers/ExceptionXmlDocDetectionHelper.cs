// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionXmlDocDetectionHelper.cs" company="Catel development team">
//   Copyright (c) 2008 - 2013 Catel development team. All rights reserved.
// </copyright>
// <summary>
//   The argument documentation detection helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Arguments
{
    using System.Text.RegularExpressions;

    using Catel.ReSharper.Arguments.Patterns;

    internal static class ExceptionXmlDocDetectionHelper
    {
        #region Public Methods and Operators
        public static bool IsMatchDocumented(string xmlDoc, string argumentName)
        {
            return IsMatch(ExceptionXmlDocDectectionPatterns.IsMatch, argumentName, xmlDoc);
        }

        public static bool IsMaximumDocumented(string xmlDoc, string argumentName)
        {
            return IsMatch(ExceptionXmlDocDectectionPatterns.IsMaximum, argumentName, xmlDoc);
        }

        public static bool IsMinimalDocumented(string xmlDoc, string argumentName)
        {
            return IsMatch(ExceptionXmlDocDectectionPatterns.IsMinimal, argumentName, xmlDoc);
        }

        public static bool IsNotMatchDocumented(string xmlDoc, string argumentName)
        {
            return IsMatch(ExceptionXmlDocDectectionPatterns.IsNotMatch, argumentName, xmlDoc);
        }

        public static bool IsNotNullDocumented(string xmlDoc, string argumentName)
        {
            return IsMatch(ExceptionXmlDocDectectionPatterns.IsNotNull, argumentName, xmlDoc);
        }

        public static bool IsNotOutOfRangeDocumented(string xmlDoc, string argumentName)
        {
            return IsMatch(ExceptionXmlDocDectectionPatterns.IsNotOutOfRange, argumentName, xmlDoc);
        }

        public static bool IsOfTypeDocumented(string xmlDoc, string argumentName)
        {
            return IsMatch(ExceptionXmlDocDectectionPatterns.IsOfType, argumentName, xmlDoc);
        }

        public static bool NotNullOrEmptyArrayDocumented(string xmlDoc, string argumentName)
        {
            return IsMatch(ExceptionXmlDocDectectionPatterns.NotNullOrEmptyArray, argumentName, xmlDoc);
        }

        public static bool NotNullOrEmptyDocumented(string xmlDoc, string argumentName)
        {
            return IsMatch(ExceptionXmlDocDectectionPatterns.NotNullOrEmpty, argumentName, xmlDoc);
        }

        public static bool NotNullOrWhitespaceDocumented(string xmlDoc, string argumentName)
        {
            return IsMatch(ExceptionXmlDocDectectionPatterns.NotNullOrEmpty, argumentName, xmlDoc);
        }

        #endregion

        #region Methods
        private static bool IsMatch(string pattern, string argumentName, string xmlDoc)
        {
            Argument.IsNotNullOrWhitespace(() => pattern);
            Argument.IsNotNullOrWhitespace(() => argumentName);

            return Regex.IsMatch(xmlDoc, string.Format(pattern, argumentName), RegexOptions.IgnorePatternWhitespace);
        }

        #endregion
    }
}