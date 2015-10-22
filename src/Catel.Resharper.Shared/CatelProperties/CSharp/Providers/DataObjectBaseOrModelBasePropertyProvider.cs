// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataObjectBaseOrModelBasePropertyProvider.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.CSharp
{
    using System.Linq;

    using Catel.Logging;
    using Catel.ReSharper.Identifiers;

    using JetBrains.ReSharper.Feature.Services.CSharp.Generate;
    using JetBrains.ReSharper.Feature.Services.Generate;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Tree;
    using JetBrains.Util;

    [GeneratorElementProvider(WellKnownGenerationActionKinds.GenerateCatelDataProperties, typeof(CSharpLanguage))]
    public class DataObjectBaseOrModelBasePropertyProvider : GeneratorProviderBase<CSharpGeneratorContext>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #region Public Properties
        public override double Priority
        {
            get
            {
                return 0;
            }
        }
        #endregion

        #region Public Methods and Operators
        public override void Populate(CSharpGeneratorContext context)
        {
            Argument.IsNotNull(() => context);

            var classLikeDeclaration = context.ClassDeclaration;
            var declaredElement = classLikeDeclaration.DeclaredElement;
            if (declaredElement is IClass && (declaredElement.IsDescendantOf(CatelCore.GetDataObjectBaseTypeElement(context.PsiModule, classLikeDeclaration.GetResolveContext())) || declaredElement.IsDescendantOf(CatelCore.GetModelBaseTypeElement(context.PsiModule, classLikeDeclaration.GetResolveContext()))))
            {
                // TODO: Consider remove or improve this restriction, walking to super types declaration. 
                // (declaredElement.GetSuperTypes().FirstOrDefault().GetTypeElement().GetDeclarations().FirstOrDefault() as IClassLikeDeclaration).GetCSharpRegisterPropertyNames();
                // List<string> registeredPropertyNames = classLikeDeclaration.GetCSharpRegisterPropertyNames();

                // NOTE: ProvidedElements collection only includes auto properties which it name is not used to register a data property.
                // context.ProvidedElements.AddRange(from member in declaredElement.GetMembers().OfType<IProperty>() let propertyDeclaration = member.GetDeclarations().FirstOrDefault() as IPropertyDeclaration where propertyDeclaration != null && (!registeredPropertyNames.Contains(member.ShortName) && propertyDeclaration.IsAuto) select new GeneratorDeclaredElement<ITypeOwner>(member));
                context.ProvidedElements.AddRange(from member in declaredElement.GetMembers().OfType<IProperty>() let propertyDeclaration = member.GetDeclarations().FirstOrDefault() as IPropertyDeclaration where propertyDeclaration != null && propertyDeclaration.IsAuto select new GeneratorDeclaredElement<ITypeOwner>(member));
            }
        }

        #endregion
    }
}