// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CatelCore.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Identifiers
{
    using Catel.ReSharper.Helpers;

    using JetBrains.Metadata.Reader.API;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.Modules;

    internal static class CatelCore
    {
        public const string AdvancedPropertyChangedEventArgs = "Catel.Data.AdvancedPropertyChangedEventArgs";

        public const string DataObjectBase = "Catel.Data.DataObjectBase";

        public const string ModelBase = "Catel.Data.ModelBase";

        public const string Argument = "Catel.Argument";

        public const string PropertyData = "Catel.Data.PropertyData";

        public static bool TryGetModelBaseTypeElement(IPsiModule psiModule, IModuleReferenceResolveContext moduleReferenceResolveContext, out ITypeElement typeElement)
        {
            return TypeHelper.TryGetTypeElement(ModelBase, psiModule, moduleReferenceResolveContext, out typeElement);
        }

        public static ITypeElement GetModelBaseTypeElement(IPsiModule psiModule, IModuleReferenceResolveContext moduleReferenceResolveContext)
        {
            ITypeElement result;
            TryGetModelBaseTypeElement(psiModule, moduleReferenceResolveContext, out result);
            return result;
        }

        public static bool TryGetDataObjectBaseTypeElement(IPsiModule psiModule, IModuleReferenceResolveContext moduleReferenceResolveContext, out ITypeElement typeElement)
        {
            return TypeHelper.TryGetTypeElement(DataObjectBase, psiModule, moduleReferenceResolveContext, out typeElement);
        }

        public static bool TryGetArgumentTypeElement(IPsiModule psiModule, IModuleReferenceResolveContext moduleReferenceResolveContext, out ITypeElement typeElement)
        {
            return TypeHelper.TryGetTypeElement(Argument, psiModule, moduleReferenceResolveContext, out typeElement);
        }

        public static bool TryGetPropertyDataTypeElement(IPsiModule psiModule, IModuleReferenceResolveContext moduleReferenceResolveContext, out ITypeElement typeElement)
        {
            return TypeHelper.TryGetTypeElement(PropertyData, psiModule, moduleReferenceResolveContext, out typeElement);
        }

        public static bool TryGetAdvancedPropertyChangedEventArgsTypeElement(IPsiModule psiModule, IModuleReferenceResolveContext moduleReferenceResolveContext, out ITypeElement typeElement)
        {
            return TypeHelper.TryGetTypeElement(AdvancedPropertyChangedEventArgs, psiModule, moduleReferenceResolveContext, out typeElement);
        }

        public static ITypeElement GetDataObjectBaseTypeElement(IPsiModule psiModule, IModuleReferenceResolveContext moduleReferenceResolveContext)
        {
            ITypeElement result;
            TryGetDataObjectBaseTypeElement(psiModule, moduleReferenceResolveContext, out result);
            return result;
        }
    }
}