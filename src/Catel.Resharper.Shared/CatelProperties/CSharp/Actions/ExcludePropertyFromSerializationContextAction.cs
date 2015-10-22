// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExcludePropertyFromSerializationContextAction.cs" company="Catel development team">
//   Copyright (c) 2008 - 2015 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Catel.ReSharper.CatelProperties.CSharp.Actions
{
    using System;
    using JetBrains.Application.Progress;
    using JetBrains.ProjectModel;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Parsing;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.TextControl;

#if R8X
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
#else
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
    using JetBrains.ReSharper.Feature.Services.ContextActions;
#endif

    [ContextAction(Name = Name, Group = "C#", Description = Description, Priority = -21)]
    public sealed class ExcludePropertyFromSerializationContextAction : FieldContextActionBase
    {
        #region Fields
        private IInvocationExpression _invocationExpression;
        #endregion

        #region Constructors and Destructors
        public ExcludePropertyFromSerializationContextAction(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }
        #endregion

        #region Public Properties
        public override string Text
        {
            get { return "Exclude from serialization"; }
        }
        #endregion

        #region Constants
        private const string Description = "ExcludePropertyFromSerializationContextActionDescription";

        private const string Name = "ExcludePropertyFromSerializationContextAction";
        #endregion

        #region Methods
        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            if (_invocationExpression.ArgumentList.Arguments.Count == 4)
            {
                _invocationExpression.RemoveArgument(_invocationExpression.ArgumentList.Arguments[3]);
                var argument = Provider.ElementFactory.CreateArgument(ParameterKind.VALUE, Provider.ElementFactory.CreateExpression("false"));
                _invocationExpression.AddArgumentAfter(argument, _invocationExpression.ArgumentList.Arguments[2]);
            }
            else
            {
                if (_invocationExpression.ArgumentList.Arguments.Count == 1)
                {
                    var argument = Provider.ElementFactory.CreateArgument(ParameterKind.VALUE, Provider.ElementFactory.CreateExpression("default($0)", PropertyDeclaration.Type));
                    _invocationExpression.AddArgumentAfter(argument, _invocationExpression.ArgumentList.Arguments[0]);
                }

                if (_invocationExpression.ArgumentList.Arguments.Count == 2)
                {
                    var argument = Provider.ElementFactory.CreateArgument(ParameterKind.VALUE, Provider.ElementFactory.CreateExpression("null"));
                    _invocationExpression.AddArgumentAfter(argument, _invocationExpression.ArgumentList.Arguments[1]);
                }

                if (_invocationExpression.ArgumentList.Arguments.Count == 3)
                {
                    var argument = Provider.ElementFactory.CreateArgument(ParameterKind.VALUE, Provider.ElementFactory.CreateExpression("false"));
                    _invocationExpression.AddArgumentAfter(argument, _invocationExpression.ArgumentList.Arguments[2]);
                }
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

            return _invocationExpression != null && (_invocationExpression.ArgumentList.Arguments.Count < 4 || ((_invocationExpression.ArgumentList.Arguments[3].Value is ICSharpLiteralExpression) && (_invocationExpression.ArgumentList.Arguments[3].Value as ICSharpLiteralExpression).Literal.GetTokenType() == CSharpTokenType.TRUE_KEYWORD));
        }
        #endregion
    }
}