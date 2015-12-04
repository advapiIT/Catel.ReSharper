// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewModelBaseModelPropertyProvider.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.CSharp.Providers
{
    using System.Collections.Generic;
    using System.Linq;

    using Catel.Logging;
    using Catel.ReSharper.Helpers;
    using Catel.ReSharper.Identifiers;

    using JetBrains.ReSharper.Feature.Services.CSharp.Generate;
    using JetBrains.ReSharper.Feature.Services.Generate;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Resolve;
    using JetBrains.ReSharper.Psi.Tree;
    using JetBrains.Util;

    /// <summary>
    /// The view model base model property provider.
    /// </summary>
    [GeneratorElementProvider(WellKnownGenerationActionKinds.ExposeModelPropertiesAsCatelDataProperties, typeof(CSharpLanguage))]
    public class ViewModelBaseModelPropertyProvider : GeneratorProviderBase<CSharpGeneratorContext>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #region Public Properties
        public override double Priority
        {
            get { return 0; }
        }
        #endregion

        #region Public Methods and Operators
        public override void Populate(CSharpGeneratorContext context)
        {
            Argument.IsNotNull(() => context);

            if (context.Anchor.Parent != null && context.Anchor.Parent.Parent is IClassLikeDeclaration)
            {
                var classLikeDeclaration = context.ClassDeclaration;
                var declaredElement = classLikeDeclaration.DeclaredElement;
                var moduleReferenceResolveContext = context.Anchor.GetResolveContext();
                var viewModelBaseElement = TypeHelper.CreateTypeByCLRName(CatelMVVM.ViewModelBase, context.PsiModule, moduleReferenceResolveContext).GetTypeElement();

                if (declaredElement is IClass && declaredElement.IsDescendantOf(viewModelBaseElement))
                {
                    var modelAttributeClrType = TypeHelper.CreateTypeByCLRName(CatelMVVM.ModelAttribute, context.PsiModule, moduleReferenceResolveContext);
                    var viewModelToModelAttributeClrType = TypeHelper.CreateTypeByCLRName(CatelMVVM.ViewModelToModelAttribute, context.PsiModule, moduleReferenceResolveContext);
                    var properties = new List<IProperty>();
                    var element = declaredElement;

                    do
                    {
                        properties.AddRange(element.GetMembers().OfType<IProperty>());
                        var declaredType = element.GetSuperTypes().FirstOrDefault(type => type.GetClrName().FullName != CatelMVVM.ViewModelBase);
                        element = declaredType != null ? declaredType.GetTypeElement() : null;
                    }
                    while (element != null);

                    Log.Debug("Looking for ViewModelToModel properties");
                    var viewModelProperties = new Dictionary<string, List<string>>();
                    foreach (var property in properties)
                    {
                        var viewModelToModel = property.GetAttributeInstances(false).FirstOrDefault(instance => Equals(instance.GetAttributeType(), viewModelToModelAttributeClrType));
                        if (viewModelToModel != null)
                        {
                            var positionParameters = viewModelToModel.PositionParameters().ToList();
                            var modelName = (string)positionParameters[0].ConstantValue.Value;
                            var propertyName = (string)positionParameters[1].ConstantValue.Value;
                            if (string.IsNullOrEmpty(propertyName))
                            {
                                propertyName = property.ShortName;
                            }

                            if (!viewModelProperties.ContainsKey(modelName))
                            {
                                viewModelProperties.Add(modelName, new List<string>());
                            }

                            viewModelProperties[modelName].Add(propertyName);
                        }
                    }

                    Log.Debug("Looking for Model properties");

                    foreach (IProperty property in properties)
                    {
                        if (property.GetAttributeInstances(false).FirstOrDefault(instance => Equals(instance.GetAttributeType(), modelAttributeClrType)) != null)
                        {
                            var propertyDeclaration = property.GetDeclarations().FirstOrDefault() as IPropertyDeclaration;
                            if (propertyDeclaration != null && propertyDeclaration.Type is IDeclaredType)
                            {
                                var declaredType = propertyDeclaration.Type as IDeclaredType;
                                var typeElement = declaredType.GetTypeElement();
                                if (typeElement != null)
                                {
                                    IProperty copyProperty = property;
                                    context.ProvidedElements.AddRange(from member in typeElement.GetMembers().OfType<IProperty>() where !viewModelProperties.ContainsKey(copyProperty.ShortName) || !viewModelProperties[copyProperty.ShortName].Contains(member.ShortName) select new GeneratorDeclaredElement(member, EmptySubstitution.INSTANCE, copyProperty));
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}