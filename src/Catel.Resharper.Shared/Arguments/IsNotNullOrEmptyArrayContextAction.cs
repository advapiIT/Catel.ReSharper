// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsNotNullOrEmptyArrayContextAction.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Arguments
{
    using System;
    using System.Xml;

    using Catel.Logging;

#if R90
    using JetBrains.ReSharper.Feature.Services.ContextActions;
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
#else
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
#endif

    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;

    [ContextAction(Name = Name, Group = "C#", Description = Description, Priority = -20)]
    public sealed class IsNotNullOrEmptyArrayContextAction : ArgumentContextActionBase
    {
        #region Constants
        private const string Description = "IsNotNullOrEmptyArrayContextActionDescription";

        private const string Name = "IsNotNullOrEmptyArrayContextAction";

        #endregion

        #region Static Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors and Destructors
        public IsNotNullOrEmptyArrayContextAction(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        #endregion

        #region Public Properties
        public override string Text
        {
            get
            {
                return "Add \"Argument.IsNotNullOrEmptyArray\"";
            }
        }

        #endregion

        #region Methods
        protected override ICSharpStatement CreateArgumentCheckStatement(
            IRegularParameterDeclaration parameterDeclaration)
        {
            return ArgumentCheckStatementHelper.CreateIsNotNullOrEmptyArrayArgumentCheckStatement(
                this.Provider, parameterDeclaration);
        }

        protected override string CreateExceptionXmlDoc(IRegularParameterDeclaration parameterDeclaration)
        {
            return ExceptionXmlDocHelper.GetIsNotNullOrEmptyArrayExceptionXmlDoc(parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentCheckDocumented(
            XmlNode xmlDocOfTheMethod, IRegularParameterDeclaration parameterDeclaration)
        {
            return ExceptionXmlDocDetectionHelper.NotNullOrEmptyArrayDocumented(
                xmlDocOfTheMethod.InnerXml, parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentChecked(
            ICSharpFunctionDeclaration methodDeclaration, IRegularParameterDeclaration parameterDeclaration)
        {
            return ArgumentCheckStatementDetectionHelper.IsNotNullOrEmptyArrayInvoked(
                methodDeclaration.Body.GetText(), parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentTypeTheExpected(IType type)
        {
            // NOTE: type.IsArray() is false, I don't know why.
            return type != null && type is IArrayType && !type.IsString();
        }

        #endregion
    }
}