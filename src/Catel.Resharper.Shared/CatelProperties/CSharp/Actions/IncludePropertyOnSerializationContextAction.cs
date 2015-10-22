// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IncludePropertyOnSerializationContextAction.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.CSharp.Actions
{
    using System;

    using JetBrains.Application.Progress;
    using JetBrains.ProjectModel;
    using JetBrains.ReSharper.Psi.CSharp.Parsing;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.TextControl;

#if R8X
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
#else
    using JetBrains.ReSharper.Feature.Services.ContextActions;
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
#endif

    [ContextAction(Name = Name, Group = "C#", Description = Description, Priority = -22)]
    public sealed class IncludePropertyOnSerializationContextAction : FieldContextActionBase
    {
        #region Constants
        private const string Description = "IncludePropertyOnSerializationContextActionDescription";

        private const string Name = "IncludePropertyOnSerializationContextAction";

        #endregion

        #region Fields

        private IInvocationExpression _invocationExpression;

        #endregion

        #region Constructors and Destructors
        public IncludePropertyOnSerializationContextAction(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        #endregion

        #region Public Properties
        public override string Text
        {
            get
            {
                return "Include on serialization";
            }
        }

        #endregion

        #region Methods
        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            if (_invocationExpression.ArgumentList.Arguments.Count == 4)
            {
                _invocationExpression.RemoveArgument(_invocationExpression.ArgumentList.Arguments[3]);
            }

            if (_invocationExpression.ArgumentList.Arguments.Count == 3
                && (_invocationExpression.ArgumentList.Arguments[2].Value is ICSharpLiteralExpression)
                && (_invocationExpression.ArgumentList.Arguments[2].Value as ICSharpLiteralExpression).Literal.GetTokenType() == CSharpTokenType.NULL_KEYWORD)
            {
                _invocationExpression.RemoveArgument(_invocationExpression.ArgumentList.Arguments[2]);
            }

            return null;
        }

        protected override bool IsAvailable()
        {
            var expressionInitializer = FieldDeclaration.Initial as IExpressionInitializer;
            _invocationExpression = null;
            if (expressionInitializer != null)
            {
                _invocationExpression = expressionInitializer.Value as IInvocationExpression;
            }

            return _invocationExpression != null
                   && (_invocationExpression.ArgumentList.Arguments.Count == 4
                       && ((_invocationExpression.ArgumentList.Arguments[3].Value is ICSharpLiteralExpression)
                           && (_invocationExpression.ArgumentList.Arguments[3].Value as ICSharpLiteralExpression).Literal.GetTokenType() == CSharpTokenType.FALSE_KEYWORD));
        }

        #endregion
    }
}