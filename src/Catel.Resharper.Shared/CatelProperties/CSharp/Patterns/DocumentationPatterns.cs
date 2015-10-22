// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentationPatterns.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.CSharp.Patterns
{
    internal static class DocumentationPatterns
    {
        public const string PropertyData = "<summary>Register the {0} property so it is known in the class.</summary>";

        public const string PropertyChangedNotification = "<summary>Occurs when the value of the {0} property is changed.</summary>";

        public const string PropertyChangedNotificationMethodWithEventArgument = "<summary>Occurs when the value of the {0} property is changed.</summary>\n<param name=\"e\">The event argument</param>";
    }
}