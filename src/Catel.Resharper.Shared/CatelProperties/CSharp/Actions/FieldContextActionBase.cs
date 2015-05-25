// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FieldContextActionBase.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.CSharp.Actions
{
    using Catel.ReSharper.CatelProperties.CSharp.Helpers;
    using Catel.ReSharper.CSharp;
    using Catel.ReSharper.Identifiers;

    using JetBrains.Application;
#if R90
    using JetBrains.ReSharper.Resources.Shell;
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
#endif
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Tree;
    using JetBrains.Util;

    public abstract class FieldContextActionBase : ContextActionBase
    {
        #region Constructors and Destructors
        protected FieldContextActionBase(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        #endregion

        #region Properties

        protected IClassDeclaration ClassDeclaration { get; private set; }

        protected IFieldDeclaration FieldDeclaration { get; private set; }

        protected IPropertyDeclaration PropertyDeclaration { get; private set; }

        #endregion

        #region Public Methods and Operators
        public override bool IsAvailable(IUserDataHolder cache)
        {
            ClassDeclaration = null;
            PropertyDeclaration = null;
            FieldDeclaration = null;

            using (ReadLockCookie.Create())
            {
                ITreeNode selectedElement = Provider.SelectedElement;
                if (selectedElement != null && selectedElement.Parent != null
                    && selectedElement.Parent is IFieldDeclaration)
                {
                    FieldDeclaration = selectedElement.Parent as IFieldDeclaration;
                    if (FieldDeclaration.Parent != null && FieldDeclaration.Parent.Parent != null
                        && FieldDeclaration.Parent.Parent.Parent is IClassDeclaration)
                    {
                        ClassDeclaration = FieldDeclaration.Parent.Parent.Parent as IClassDeclaration;
                        ITypeElement classDeclaredElement = ClassDeclaration.DeclaredElement;
#if R80 || R81 || R82 || R90
                        if (classDeclaredElement != null && (classDeclaredElement.IsDescendantOf(CatelCore.GetDataObjectBaseTypeElement(Provider.PsiModule, selectedElement.GetResolveContext())) || classDeclaredElement.IsDescendantOf(CatelCore.GetModelBaseTypeElement(Provider.PsiModule, selectedElement.GetResolveContext()))) && (FieldDeclaration.IsStatic && FieldDeclaration.Initial is IExpressionInitializer))
#else
                        if (classDeclaredElement != null && (classDeclaredElement.IsDescendantOf(CatelCore.GetDataObjectBaseTypeElement(Provider.PsiModule)) || classDeclaredElement.IsDescendantOf(CatelCore.GetModelBaseTypeElement(Provider.PsiModule))) && (FieldDeclaration.IsStatic && FieldDeclaration.Initial is IExpressionInitializer))
#endif
                        {
                            var expressionInitializer = FieldDeclaration.Initial as IExpressionInitializer;
                            if (expressionInitializer.Value is IInvocationExpression)
                            {
                                var invocationExpression = expressionInitializer.Value as IInvocationExpression;
                                if (invocationExpression.InvokedExpression is IReferenceExpression)
                                {
                                    var referenceExpression = invocationExpression.InvokedExpression as IReferenceExpression;
                                    if (referenceExpression.NameIdentifier != null && referenceExpression.NameIdentifier.GetText() == RegisterPropertyExpressionHelper.RegisterPropertyMethodName)
                                    {
                                        PropertyDeclaration = RegisterPropertyExpressionHelper.GetPropertyDeclaration(ClassDeclaration, invocationExpression);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ClassDeclaration != null && FieldDeclaration != null && PropertyDeclaration != null
                   && IsAvailable();
        }

        #endregion

        #region Methods
        protected abstract bool IsAvailable();

        #endregion
    }
}