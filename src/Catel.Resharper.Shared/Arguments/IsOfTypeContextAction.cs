﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsOfTypeContextAction.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Arguments
{
    using System;
    using System.Xml;

    using Catel.Logging;

#if R8X
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
#else
    using JetBrains.ReSharper.Feature.Services.ContextActions;
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
#endif

    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;

    [ContextAction(Name = Name, Group = "C#", Description = Description, Priority = -20)]
    public sealed class IsOfTypeContextAction : ArgumentContextActionBase
    {
        #region Constants
        private const string Description = "IsOfTypeContextActionDescription";

        private const string Name = "IsOfTypeContextAction";

        #endregion

        #region Static Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors and Destructors
        public IsOfTypeContextAction(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        #endregion

        #region Public Properties
        public override string Text
        {
            get
            {
                return "Add \"Argument.IsOfType\"";
            }
        }

        #endregion

        #region Methods
        protected override ICSharpStatement CreateArgumentCheckStatement(
            IRegularParameterDeclaration parameterDeclaration)
        {
            return ArgumentCheckStatementHelper.CreateIsOfTypeArgumentCheckStatement(
                this.Provider, parameterDeclaration);
        }

        protected override string CreateExceptionXmlDoc(IRegularParameterDeclaration parameterDeclaration)
        {
            return ExceptionXmlDocHelper.GetIsOfTypeExceptionXmlDoc(parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentCheckDocumented(
            XmlNode xmlDocOfTheMethod, IRegularParameterDeclaration parameterDeclaration)
        {
            return ExceptionXmlDocDetectionHelper.IsOfTypeDocumented(
                xmlDocOfTheMethod.InnerXml, parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentChecked(
            ICSharpFunctionDeclaration methodDeclaration, IRegularParameterDeclaration parameterDeclaration)
        {
            return ArgumentCheckStatementDetectionHelper.IsOfTypeInvoked(
                methodDeclaration.Body.GetText(), parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentTypeTheExpected(IType type)
        {
            return type != null && type.Classify == TypeClassification.REFERENCE_TYPE && !type.IsNullable()
                   && !(type is IArrayType || type.IsString());
        }

        #endregion
    }
}