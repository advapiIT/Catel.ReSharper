// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsNotMatchContextAction.cs" company="Catel development team">
//   Copyright (c) 2008 - 2013 Catel development team. All rights reserved.
// </copyright>
// <summary>
//   The is not match context action.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Catel.ReSharper.Arguments
{
    using System;
    using System.Xml;

    using Catel.Logging;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;

#if R8X
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
#else
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
    using JetBrains.ReSharper.Feature.Services.ContextActions;
#endif


    [ContextAction(Name = Name, Group = "C#", Description = Description, Priority = -20)]
    public sealed class IsMatchContextAction : ArgumentContextActionBase
    {
        #region Constants
        private const string Description = "IsNotMatchContextActionDescription";
        private const string Name = "IsNotMatchContextAction";

        #endregion

        #region Static Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors and Destructors
        public IsMatchContextAction(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        #endregion

        #region Public Properties
        public override string Text
        {
            get
            {
                return "Add \"Argument.IsMatch\"";
            }
        }

        #endregion

        #region Methods
        protected override ICSharpStatement CreateArgumentCheckStatement(IRegularParameterDeclaration parameterDeclaration)
        {
            return ArgumentCheckStatementHelper.CreateIsMatchArgumentCheckStatement(this.Provider, parameterDeclaration);
        }

        protected override string CreateExceptionXmlDoc(IRegularParameterDeclaration parameterDeclaration)
        {
            return ExceptionXmlDocHelper.GetIsMatchExceptionXmlDoc(parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentCheckDocumented(XmlNode xmlDocOfTheMethod, IRegularParameterDeclaration parameterDeclaration)
        {
            return ExceptionXmlDocDetectionHelper.IsMatchDocumented(xmlDocOfTheMethod.InnerXml, parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentChecked(ICSharpFunctionDeclaration methodDeclaration, IRegularParameterDeclaration parameterDeclaration)
        {
            return ArgumentCheckStatementDetectionHelper.IsMatchInvoked(methodDeclaration.Body.GetText(), parameterDeclaration.DeclaredName);
        }

        protected override bool IsArgumentTypeTheExpected(IType type)
        {
            return type != null && type.IsString();
        }

        #endregion
    }
}