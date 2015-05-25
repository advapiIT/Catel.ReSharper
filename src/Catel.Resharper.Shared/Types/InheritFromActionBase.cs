// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InheritFromActionBase.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Types
{
    using System;

    using Catel.ReSharper.CatelProperties.CSharp.Extensions;
    using Catel.ReSharper.CSharp;

    using JetBrains.Application;
    using JetBrains.Application.Progress;
    using JetBrains.ProjectModel;

#if R90
    using JetBrains.ReSharper.Resources.Shell;
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
#endif

    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;

#if R80 || R81 || R82 || R90
    using JetBrains.ReSharper.Psi.Tree;
#endif

    using JetBrains.TextControl;
    using JetBrains.Util;

    public abstract class InheritFromActionBase : ContextActionBase
    {
        private const string InheritFromTextPattern = "Inherit from '{0}'";

        private IClassDeclaration _classDeclaration;

        private IDeclaredType _superType;

        protected InheritFromActionBase(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        public override string Text
        {
            get
            {
                return string.Format(InheritFromTextPattern, SuperTypeName);
            }
        }

        protected abstract string SuperTypeName { get; }

        public override bool IsAvailable(IUserDataHolder cache)
        {
            using (ReadLockCookie.Create())
            {
                if (Provider.SelectedElement != null)
                {
#if R80 || R81 || R82 || R90

                    _superType = TypeFactory.CreateTypeByCLRName(this.SuperTypeName, this.Provider.PsiModule, this.Provider.SelectedElement.GetResolveContext());
#else
                    _superType = TypeFactory.CreateTypeByCLRName(SuperTypeName, Provider.PsiModule);
#endif
                    if (_superType.GetTypeElement() != null)
                    {
                        _classDeclaration = Provider.SelectedElement.Parent as IClassDeclaration;
                    }
                }
            }

            // !this._classDeclaration.IsStatic doesn't work, IsStatic is returns true
            return _classDeclaration != null && !_classDeclaration.IsStaticEx() && _classDeclaration.SuperTypes.IsEmpty();
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            _classDeclaration.SetSuperClass(_superType);
            return null;
        }
    }
}