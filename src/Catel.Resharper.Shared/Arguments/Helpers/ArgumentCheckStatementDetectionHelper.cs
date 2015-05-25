// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsMaximumInvoked.cs" company="Catel development team">
//   Copyright (c) 2008 - 2013 Catel development team. All rights reserved.
// </copyright>
// <summary>
//   The argument invocation detection helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Arguments
{
    using System.Text.RegularExpressions;

    using Catel.ReSharper.Arguments.Patterns;

    internal static class ArgumentCheckStatementDetectionHelper
    {
        #region Public Methods and Operators
        public static bool IsMaximumInvoked(string methodBody, string argumentName)
        {
            Argument.IsNotNullOrWhitespace(() => argumentName);

            return Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsMaximum, argumentName), RegexOptions.IgnorePatternWhitespace) || Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsMaximum2, argumentName), RegexOptions.IgnorePatternWhitespace);
        }

        public static bool IsMinimalInvoked(string methodBody, string argumentName)
        {
            Argument.IsNotNullOrWhitespace(() => argumentName);

            return Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsMinimal, argumentName), RegexOptions.IgnorePatternWhitespace) || Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsMinimal2, argumentName), RegexOptions.IgnorePatternWhitespace);
        }

        public static bool IsNotMatchInvoked(string methodBody, string argumentName)
        {
            Argument.IsNotNullOrWhitespace(() => argumentName);

            return Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsNotMatch, argumentName), RegexOptions.IgnorePatternWhitespace) || Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsNotMatch2, argumentName), RegexOptions.IgnorePatternWhitespace);
        }

        public static bool IsMatchInvoked(string methodBody, string argumentName)
        {
            Argument.IsNotNullOrWhitespace(() => argumentName);

            return Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsMatch, argumentName), RegexOptions.IgnorePatternWhitespace) || Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsMatch2, argumentName), RegexOptions.IgnorePatternWhitespace);
        }
   
        public static bool IsNotNullInvoked(string methodBody, string argumentName)
        {
            Argument.IsNotNullOrWhitespace(() => argumentName);

            return Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsNotNull, argumentName), RegexOptions.IgnorePatternWhitespace) || Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsNotNull2, argumentName), RegexOptions.IgnorePatternWhitespace);
        }

        public static bool IsNotNullOrEmptyArrayInvoked(string methodBody, string argumentName)
        {
            Argument.IsNotNullOrWhitespace(() => argumentName);

            return Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsNotNullOrEmptyArray, argumentName), RegexOptions.IgnorePatternWhitespace) || Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsNotNullOrEmptyArray2, argumentName), RegexOptions.IgnorePatternWhitespace);
        }

        public static bool IsNotNullOrEmptyInvoked(string methodBody, string argumentName)
        {
            Argument.IsNotNullOrWhitespace(() => argumentName);

            return Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsNotNullOrEmpty, argumentName), RegexOptions.IgnorePatternWhitespace) || Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsNotNullOrEmpty2, argumentName), RegexOptions.IgnorePatternWhitespace);
        }

        public static bool IsNotNullOrWhitespaceInvoked(string methodBody, string argumentName)
        {
            Argument.IsNotNullOrWhitespace(() => argumentName);

            return Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsNotNullOrWhitespace, argumentName), RegexOptions.IgnorePatternWhitespace) || Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsNotNullOrWhitespace2, argumentName), RegexOptions.IgnorePatternWhitespace);
        }

        public static bool IsNotOutOfRangeInvoked(string methodBody, string argumentName)
        {
            Argument.IsNotNullOrWhitespace(() => argumentName);

            return Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsNotOutOfRange, argumentName), RegexOptions.IgnorePatternWhitespace) || Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsNotOutOfRange2, argumentName), RegexOptions.IgnorePatternWhitespace);
        }

        public static bool IsOfTypeInvoked(string methodBody, string argumentName)
        {
            Argument.IsNotNullOrWhitespace(() => argumentName);

            return Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsOfType, argumentName), RegexOptions.IgnorePatternWhitespace) || Regex.IsMatch(methodBody, string.Format(ArgumentCheckStatementDetectionPatterns.IsOfType2, argumentName), RegexOptions.IgnorePatternWhitespace);
        }

        #endregion

   }
}