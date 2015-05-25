// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentCheckStatementHelper.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Arguments
{
    using Catel.ReSharper.Arguments.Patterns;
    using Catel.ReSharper.Identifiers;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;

#if R90
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
#endif

#if R80 || R81 || R82 || R90
    using JetBrains.ReSharper.Psi.Tree;
#endif

    internal static class ArgumentCheckStatementHelper
    {
        public static ICSharpStatement CreateIsOfTypeArgumentCheckStatement(ICSharpContextActionDataProvider provider, IRegularParameterDeclaration parameterDeclaration)
        {
            return CreateArgumentCheckStatement(provider, ArgumentCheckStatementPatterns.IsOfType, parameterDeclaration);
        }

        public static ICSharpStatement CreateImplementsInterfaceArgumentCheckStatement(ICSharpContextActionDataProvider provider, IRegularParameterDeclaration parameterDeclaration)
        {
            return CreateArgumentCheckStatement(provider, ArgumentCheckStatementPatterns.ImplementsInterface, parameterDeclaration);
        }

        public static ICSharpStatement CreateIsMaximumArgumentCheckStatement(ICSharpContextActionDataProvider provider, IRegularParameterDeclaration parameterDeclaration)
        {
            return CreateArgumentCheckStatement(provider, ArgumentCheckStatementPatterns.IsMaximum, parameterDeclaration);
        }

        public static ICSharpStatement CreateIsMinimalArgumentCheckStatement(ICSharpContextActionDataProvider provider, IRegularParameterDeclaration parameterDeclaration)
        {
            return CreateArgumentCheckStatement(provider, ArgumentCheckStatementPatterns.IsMinimal, parameterDeclaration);
        }

        public static ICSharpStatement CreateIsNotNullArgumentCheckStatement(ICSharpContextActionDataProvider provider, IRegularParameterDeclaration parameterDeclaration)
        {
            return CreateArgumentCheckStatement(provider, ArgumentCheckStatementPatterns.IsNotNull, parameterDeclaration);
        }

        public static ICSharpStatement CreateIsNotNullOrEmptyArrayArgumentCheckStatement(ICSharpContextActionDataProvider provider, IRegularParameterDeclaration parameterDeclaration)
        {
            return CreateArgumentCheckStatement(provider, ArgumentCheckStatementPatterns.IsNotNullOrEmptyArray, parameterDeclaration);
        }

        public static ICSharpStatement CreateIsNotNullOrEmptyArgumentCheckStatement(ICSharpContextActionDataProvider provider, IRegularParameterDeclaration parameterDeclaration)
        {
            return CreateArgumentCheckStatement(provider, ArgumentCheckStatementPatterns.IsNotNullOrEmpty, parameterDeclaration);
        }

        public static ICSharpStatement CreateIsNotNullOrWhitespaceArgumentCheckStatement(ICSharpContextActionDataProvider provider, IRegularParameterDeclaration parameterDeclaration)
        {
            return CreateArgumentCheckStatement(provider, ArgumentCheckStatementPatterns.IsNotNullOrWhitespace, parameterDeclaration);
        }

        public static ICSharpStatement CreateIsNotOutOfRangeArgumentCheckStatement(ICSharpContextActionDataProvider provider, IRegularParameterDeclaration parameterDeclaration)
        {
            return CreateArgumentCheckStatement(provider, ArgumentCheckStatementPatterns.IsNotOutOfRange, parameterDeclaration);
        }

        public static ICSharpStatement CreateIsNotMatchArgumentCheckStatement(ICSharpContextActionDataProvider provider, IRegularParameterDeclaration parameterDeclaration)
        {
            return CreateArgumentCheckStatement(provider, ArgumentCheckStatementPatterns.IsNotMatch, parameterDeclaration);
        }

        public static ICSharpStatement CreateIsMatchArgumentCheckStatement(ICSharpContextActionDataProvider provider, IRegularParameterDeclaration parameterDeclaration)
        {
            return CreateArgumentCheckStatement(provider, ArgumentCheckStatementPatterns.IsMatch, parameterDeclaration);
        }

        private static ICSharpStatement CreateArgumentCheckStatement(ICSharpContextActionDataProvider provider, string pattern, IRegularParameterDeclaration parameterDeclaration)
        {
            Argument.IsNotNull(() => provider);
            Argument.IsNotNullOrWhitespace(() => pattern);
            Argument.IsNotNull(() => parameterDeclaration);

#if R80 || R81 || R82 || R90
            var catelArgumentType = TypeFactory.CreateTypeByCLRName(CatelCore.Argument, provider.PsiModule, provider.SelectedElement.GetResolveContext());
#else
            var catelArgumentType = TypeFactory.CreateTypeByCLRName(CatelCore.Argument, provider.PsiModule);
#endif

            return provider.ElementFactory.CreateStatement(pattern, catelArgumentType.GetTypeElement(), parameterDeclaration.DeclaredName);
        }
    }
}