// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsNotOutOfRangeContextAction.cs" company="Catel development team">
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
    public sealed class IsNotOutOfRangeContextAction : ArgumentContextActionBase
    {
        #region Constants

        private const string Description = "IsNotOutOfRangeContextActionDescription";

        private const string Name = "IsNotOutOfRangeContextAction";

        #endregion

        #region Static Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors and Destructors
        public IsNotOutOfRangeContextAction(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        #endregion

        #region Public Properties
        public override string Text
        {
            get
            {
                return "Add \"Argument.IsNotOutOfRange\"";
            }
        }

        #endregion

        #region Methods
        protected override ICSharpStatement CreateArgumentCheckStatement(
            IRegularParameterDeclaration parameterDeclaration)
        {
            return ArgumentCheckStatementHelper.CreateIsNotOutOfRangeArgumentCheckStatement(
                this.Provider, parameterDeclaration);
        }

        protected override string CreateExceptionXmlDoc(IRegularParameterDeclaration parameterDeclaration)
        {
            return ExceptionXmlDocHelper.GetIsNotOutOfRangeExceptionXmlDoc(parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentCheckDocumented(
            XmlNode xmlDocOfTheMethod, IRegularParameterDeclaration parameterDeclaration)
        {
            return ExceptionXmlDocDetectionHelper.IsNotOutOfRangeDocumented(
                xmlDocOfTheMethod.InnerXml, parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentChecked(
            ICSharpFunctionDeclaration methodDeclaration, IRegularParameterDeclaration parameterDeclaration)
        {
            // TODO: Verify the completion of the condition || (InvokationDetectionHelper.IsMaximunInvoked(methodDeclaration.Body.GetText(), parameterDeclaration.DeclaredName) && InvokationDetectionHelper.IsMinimalInvoked(methodDeclaration.Body.GetText(), parameterDeclaration.DeclaredName));
            return ArgumentCheckStatementDetectionHelper.IsNotOutOfRangeInvoked(
                methodDeclaration.Body.GetText(), parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentTypeTheExpected(IType type)
        {
            return type != null && (type.IsDouble() || type.IsInt() || type.IsDecimal() || type.IsDouble());
        }

        #endregion
    }
}