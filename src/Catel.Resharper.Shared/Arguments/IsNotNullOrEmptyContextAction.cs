// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsNotNullOrEmptyContextAction.cs" company="Catel development team">
//   Copyright (c) 2008 - 2013 Catel development team. All rights reserved.
// </copyright>
// <summary>
//   The is not null or empty context action.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Arguments
{
    using System;
    using System.Xml;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using Catel.Logging;

#if R8X
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
#else
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
    using JetBrains.ReSharper.Feature.Services.ContextActions;
#endif

    [ContextAction(Name = Name, Group = "C#", Description = Description, Priority = -20)]
    public sealed class IsNotNullOrEmptyContextAction : ArgumentContextActionBase
    {
        #region Constants
        private const string Description = "IsNotNullOrEmptyContextActionDescription";

        private const string Name = "IsNotNullOrEmptyContextAction";

        #endregion

        #region Static Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors and Destructors
        public IsNotNullOrEmptyContextAction(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        #endregion

        #region Public Properties
        public override string Text
        {
            get { return "Add \"Argument.IsNotNullOrEmpty\""; }
        }

        #endregion

        #region Methods

        protected override ICSharpStatement CreateArgumentCheckStatement(IRegularParameterDeclaration parameterDeclaration)
        {
            return ArgumentCheckStatementHelper.CreateIsNotNullOrEmptyArgumentCheckStatement(this.Provider, parameterDeclaration);
        }

        protected override string CreateExceptionXmlDoc(IRegularParameterDeclaration parameterDeclaration)
        {
            return ExceptionXmlDocHelper.GetIsNotNullOrEmptyExceptionXmlDoc(parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentCheckDocumented(XmlNode xmlDocOfTheMethod, IRegularParameterDeclaration parameterDeclaration)
        {
            return ExceptionXmlDocDetectionHelper.NotNullOrEmptyDocumented(xmlDocOfTheMethod.InnerXml, parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentChecked(ICSharpFunctionDeclaration methodDeclaration, IRegularParameterDeclaration parameterDeclaration)
        {
            return ArgumentCheckStatementDetectionHelper.IsNotNullOrEmptyInvoked(methodDeclaration.Body.GetText(), parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentTypeTheExpected(IType type)
        {
            IDeclaredType declaredType;
            return type != null && (type.IsString() || ((declaredType = type.GetScalarType()) != null && declaredType.GetClrName().FullName == "System.Guid"));
        }

        #endregion
    }
}