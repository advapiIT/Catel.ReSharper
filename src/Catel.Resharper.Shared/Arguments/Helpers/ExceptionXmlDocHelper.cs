// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionXmlDocHelper.cs" company="Catel development team">
//   Copyright (c) 2008 - 2013 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Arguments
{
    using System;

    using Catel.ReSharper.Arguments.Patterns;

    public static class ExceptionXmlDocHelper
    {
        #region Public Methods and Operators

        public static string GetImplementsInterfaceExceptionXmlDoc(string declaredName)
        {
            return GetExceptionXmlDoc(ExceptionXmlDocPatterns.ImplementsInterface, declaredName);
        }

        public static string GetIsMatchExceptionXmlDoc(string declaredName)
        {
            return GetExceptionXmlDoc(ExceptionXmlDocPatterns.IsMatch, declaredName);
        }

        public static string GetIsMaximunExceptionXmlDoc(string declaredName)
        {
            return GetExceptionXmlDoc(ExceptionXmlDocPatterns.IsMaximum, declaredName);
        }

        public static string GetIsMinimalExceptionXmlDoc(string declaredName)
        {
            return GetExceptionXmlDoc(ExceptionXmlDocPatterns.IsMinimal, declaredName);
        }

        public static string GetIsNotMatchExceptionXmlDoc(string declaredName)
        {
            return GetExceptionXmlDoc(ExceptionXmlDocPatterns.IsNotMatch, declaredName);
        }

        public static string GetIsNotNullExceptionXmlDoc(string declaredName)
        {
            return GetExceptionXmlDoc(ExceptionXmlDocPatterns.IsNotNull, declaredName);
        }

        public static string GetIsNotNullOrEmptyArrayExceptionXmlDoc(string declaredName)
        {
            return GetExceptionXmlDoc(ExceptionXmlDocPatterns.IsNotNullOrEmptyArray, declaredName);
        }

        public static string GetIsNotNullOrEmptyExceptionXmlDoc(string declaredName)
        {
            return GetExceptionXmlDoc(ExceptionXmlDocPatterns.IsNotNullOrEmpty, declaredName);
        }

        public static string GetIsNotNullOrWhitespaceExceptionXmlDoc(string declaredName)
        {
            return GetExceptionXmlDoc(ExceptionXmlDocPatterns.IsNotNullOrWhitespace, declaredName);
        }

        public static string GetIsNotOutOfRangeExceptionXmlDoc(string declaredName)
        {
            return GetExceptionXmlDoc(ExceptionXmlDocPatterns.IsNotOutOfRange, declaredName);
        }

        public static string GetIsOfTypeExceptionXmlDoc(string declaredName)
        {
            Argument.IsNotNullOrWhitespace(() => declaredName);

            return GetExceptionXmlDoc(ExceptionXmlDocPatterns.IsOfType, declaredName);
        }

        #endregion

        #region Methods
        private static string GetExceptionXmlDoc(string pattern, string declaredName)
        {
            Argument.IsNotNullOrWhitespace(() => pattern);
            Argument.IsNotNullOrWhitespace(() => declaredName);

            return string.Format(pattern, declaredName);
        }

        #endregion
    }
}