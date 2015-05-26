// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CatelMVVM.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Identifiers
{
    using Catel.ReSharper.Helpers;
    using JetBrains.ReSharper.Psi;
    using JetBrains.Metadata.Reader.API;
    using JetBrains.ReSharper.Psi.Modules;

    /// <summary>
    /// The MVVM well known type names.
    /// </summary>
    public static class CatelMVVM
    {
        /// <summary>
        /// The view model base type name.
        /// </summary>
        public const string ViewModelBase = "Catel.MVVM.ViewModelBase";

        /// <summary>
        /// The model attribute type name.
        /// </summary>
        public const string ModelAttribute = "Catel.MVVM.ModelAttribute";

        /// <summary>
        /// The view model to model attribute.
        /// </summary>
        public const string ViewModelToModelAttribute = "Catel.MVVM.ViewModelToModelAttribute";

        public static bool TryGetViewModelToModelAttributeTypeElement(IPsiModule psiModule, IModuleReferenceResolveContext moduleReferenceResolveContext, out ITypeElement typeElement)
        {
            return TypeHelper.TryGetTypeElement(ViewModelToModelAttribute, psiModule, moduleReferenceResolveContext, out typeElement);
        }

        public static bool TryGetModelAttributeTypeElement(IPsiModule psiModule, IModuleReferenceResolveContext moduleReferenceResolveContext, out ITypeElement typeElement)
        {
            return TypeHelper.TryGetTypeElement(ModelAttribute, psiModule, moduleReferenceResolveContext, out typeElement);
        }

        public static bool TryGetViewModelBaseTypeElement(IPsiModule psiModule, IModuleReferenceResolveContext moduleReferenceResolveContext, out ITypeElement typeElement)
        {
            return TypeHelper.TryGetTypeElement(ViewModelBase, psiModule, moduleReferenceResolveContext, out typeElement);
        }
    }
}