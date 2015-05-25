// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeHelper.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Helpers
{
    using JetBrains.ReSharper.Psi;
#if R80 || R81 || R82 || R90
    using JetBrains.Metadata.Reader.API;
    using JetBrains.ReSharper.Psi.Modules;
#endif

    /// <summary>
    /// The type helper.
    /// </summary>
    internal static class TypeHelper
    {
#if R80 || R81 || R82 || R90
        public static bool TryGetTypeElement(string typeName, IPsiModule psiModule, IModuleReferenceResolveContext moduleReferenceResolveContext, out ITypeElement typeElement)
        {
            typeElement = TypeFactory.CreateTypeByCLRName(typeName, psiModule, moduleReferenceResolveContext).GetTypeElement();
            return typeElement != null;
        }
#else
        public static bool TryGetTypeElement(string typeName, IPsiModule psiModule, out ITypeElement typeElement)
        {
            typeElement = TypeFactory.CreateTypeByCLRName(typeName, psiModule).GetTypeElement();
            return typeElement != null;
        }
#endif
    }
}