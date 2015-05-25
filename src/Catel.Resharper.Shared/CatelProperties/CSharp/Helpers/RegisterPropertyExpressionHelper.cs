// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterPropertyExpressionHelper.cs" company="Catel development team">
//   Copyright (c) 2008 - 2015 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Catel.ReSharper.CatelProperties.CSharp.Helpers
{
    using System.Linq;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;

    internal static class RegisterPropertyExpressionHelper
    {
        #region Constants
        public const string RegisterPropertyMethodName = "RegisterProperty";
        #endregion

        #region Public Methods and Operators
        public static IPropertyDeclaration GetPropertyDeclaration(IClassDeclaration classDeclaration, IInvocationExpression invocationExpression)
        {
            IPropertyDeclaration propertyDeclaration = null;
            if (invocationExpression.ArgumentList.Arguments.Count >= 1)
            {
                string propertyName = null;
                var argument = invocationExpression.ArgumentList.Arguments[0];
                if (argument.Value is ILambdaExpression)
                {
                    var lambdaExpression = argument.Value as ILambdaExpression;
                    var referenceExpression = lambdaExpression.BodyExpression as IReferenceExpression;
                    if (referenceExpression != null)
                    {
                        propertyName = referenceExpression.NameIdentifier.Name;
                    }
                }
                else if (argument.Value != null && argument.Value.ConstantValue != null)
                {
                    propertyName = argument.Value.ConstantValue.Value.ToString();
                }

                if (!string.IsNullOrEmpty(propertyName) && classDeclaration.DeclaredElement != null)
                {
                    var property = (from member in classDeclaration.DeclaredElement.GetMembers().OfType<IProperty>() where member.ShortName == propertyName select member).FirstOrDefault();
                    if (property != null)
                    {
                        propertyDeclaration = (IPropertyDeclaration) property.GetDeclarations().FirstOrDefault();
                    }
                }
            }

            return propertyDeclaration;
        }
        #endregion
    }
}