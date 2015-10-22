// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyConverter.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.CSharp
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Catel.Logging;
    using Catel.ReSharper.CatelProperties.CSharp.Patterns;
    using Catel.ReSharper.CatelProperties.Patterns;
    using Catel.ReSharper.Identifiers;

    using JetBrains.Annotations;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

    using JetBrains.ReSharper.Psi.Modules;
    using JetBrains.ReSharper.Psi.Tree;

    public class PropertyConverter
    {
        #region Static Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #endregion

        #region Fields
        private readonly IClassDeclaration _classDeclaration;

        private readonly CSharpElementFactory _factory;

        private readonly IPsiModule _psiModule;

        #endregion

        #region Constructors and Destructors
        public PropertyConverter(CSharpElementFactory factory, IPsiModule psiModule, IClassDeclaration classDeclaration)
        {
            Argument.IsNotNull(() => factory);
            Argument.IsNotNull(() => psiModule);
            Argument.IsNotNull(() => classDeclaration);

            _factory = factory;
            _psiModule = psiModule;
            _classDeclaration = classDeclaration;
        }

        #endregion

        #region Public Methods and Operators
        public void Convert(
            [NotNull] IPropertyDeclaration propertyDeclaration, 
            bool includeInSerialization = true, 
            bool notificationMethod = false, 
            bool forwardEventArgument = false)
        {
            Argument.IsNotNull(() => propertyDeclaration);

            var propertyDataType = TypeFactory.CreateTypeByCLRName(CatelCore.PropertyData, _psiModule, propertyDeclaration.GetResolveContext());
            if (!propertyDeclaration.IsAuto)
            {
                throw new ArgumentException("The 'propertyDeclaration' is not auto");
            }

            var propertyName = propertyDeclaration.DeclaredName;
            var propertyDataName = ComputeMemberName(string.Format(NamePatterns.PropertyDataName, propertyName));

            IFieldDeclaration propertyDataMemberDeclaration;

            var declaredName = _classDeclaration.DeclaredName;
            if (_classDeclaration.TypeParameters.Count > 0)
            {
                var parameters = _classDeclaration.TypeParameters.Aggregate(string.Empty, (current, parameter) => current + (parameter.DeclaredName + ", "));
                declaredName = string.Format(CultureInfo.InvariantCulture, "{0}<{1}>", declaredName, parameters.Substring(0, parameters.Length - 2));
            }

            if (notificationMethod)
            {
                var methodName = ComputeMemberName(string.Format(NamePatterns.NotificationMethodName, propertyName));

                IMethodDeclaration methodDeclaration;
#if R8X
                IDocCommentBlockNode methodComment;
#else
                IDocCommentBlock methodComment;
#endif

                if (forwardEventArgument)
                {
                    if (includeInSerialization)
                    {
                        propertyDataMemberDeclaration =
                            (IFieldDeclaration)
                            _factory.CreateTypeMemberDeclaration(
                                ImplementationPatterns.PropertyDataWithNotificationMethodForwardingEventArgument, 
                                propertyName, 
                                propertyDataName, 
                                propertyDeclaration.DeclaredElement.Type, 
                                declaredName, 
                                propertyDataType.GetTypeElement(), 
                                methodName);
                    }
                    else
                    {
                        propertyDataMemberDeclaration =
                            (IFieldDeclaration)
                            _factory.CreateTypeMemberDeclaration(
                                ImplementationPatterns
                                .PropertyDataNonSerializedWithNotificationMethodForwardingEventArgument, 
                                propertyName, 
                                propertyDataName, 
                                propertyDeclaration.DeclaredElement.Type, 
                                declaredName, 
                                propertyDataType.GetTypeElement(), 
                                methodName);
                    }

                    var advancedPropertyChangedEventArgsType = TypeFactory.CreateTypeByCLRName(CatelCore.AdvancedPropertyChangedEventArgs, _psiModule, propertyDeclaration.GetResolveContext());
                    methodDeclaration =
                        (IMethodDeclaration)
                        _factory.CreateTypeMemberDeclaration(
                            ImplementationPatterns.PropertyChangedNotificationMethodWithEventArgs, 
                            methodName, 
                            advancedPropertyChangedEventArgsType.GetTypeElement());
                    methodComment =
                        _factory.CreateDocCommentBlock(
                            string.Format(
                                DocumentationPatterns.PropertyChangedNotificationMethodWithEventArgument, propertyName));
                }
                else
                {
                    if (includeInSerialization)
                    {
                        propertyDataMemberDeclaration =
                            (IFieldDeclaration)
                            _factory.CreateTypeMemberDeclaration(
                                ImplementationPatterns.PropertyDataPlusNotificationMethod, 
                                propertyName, 
                                propertyDataName, 
                                propertyDeclaration.DeclaredElement.Type, 
                                declaredName, 
                                propertyDataType.GetTypeElement(), 
                                methodName);
                    }
                    else
                    {
                        propertyDataMemberDeclaration =
                            (IFieldDeclaration)
                            _factory.CreateTypeMemberDeclaration(
                                ImplementationPatterns.PropertyDataNonSerializedPlusNotificationMethod, 
                                propertyName, 
                                propertyDataName, 
                                propertyDeclaration.DeclaredElement.Type, 
                                declaredName, 
                                propertyDataType.GetTypeElement(), 
                                methodName);
                    }

                    methodDeclaration =
                        (IMethodDeclaration)
                        _factory.CreateTypeMemberDeclaration(
                            ImplementationPatterns.PropertyChangedNotificationMethod, methodName);
                    methodComment =
                        _factory.CreateDocCommentBlock(
                            string.Format(DocumentationPatterns.PropertyChangedNotification, propertyName));
                }

                methodDeclaration = ModificationUtil.AddChildAfter(
                    _classDeclaration, propertyDeclaration, methodDeclaration);

                // context.PutMemberDeclaration(methodDeclaration, null, declaration => new GeneratorDeclarationElement(declaration));
                // NOTE: Add xml doc to a method
                // TODO: Move this to an extension method
                // ICSharpTypeMemberDeclaration pushedMethodDeclaration = context.ClassDeclaration.MemberDeclarations.FirstOrDefault(declaration => declaration.DeclaredName == methodName);
                if (methodDeclaration != null && methodDeclaration.Parent != null)
                {
                    ModificationUtil.AddChildBefore(
                        methodDeclaration.Parent, methodDeclaration.FirstChild, methodComment);
                }
            }
            else if (includeInSerialization)
            {
                propertyDataMemberDeclaration =
                    (IFieldDeclaration)
                    _factory.CreateTypeMemberDeclaration(
                        ImplementationPatterns.PropertyData, 
                        propertyName, 
                        propertyDeclaration.DeclaredElement.Type, 
                        propertyDataType.GetTypeElement(),
                        declaredName);
            }
            else
            {
                propertyDataMemberDeclaration =
                    (IFieldDeclaration)
                    _factory.CreateTypeMemberDeclaration(
                        ImplementationPatterns.PropertyDataNonSerialized, 
                        propertyName, 
                        propertyDeclaration.DeclaredElement.Type, 
                        propertyDataType.GetTypeElement(),
                        declaredName);
            }

            // context.PutMemberDeclaration(propertyDataMemberDeclaration, null, declaration => new GeneratorDeclarationElement(declaration));
            var multipleFieldDeclaration =
                (IMultipleFieldDeclaration)
                ModificationUtil.AddChildBefore(
                    _classDeclaration, propertyDeclaration, propertyDataMemberDeclaration.Parent);

            // NOTE: Add xml doc to a property
            // TODO: Move this to an extension method
            // ICSharpTypeMemberDeclaration pushedPropertyDataMemberDeclaration = context.ClassDeclaration.MemberDeclarations.FirstOrDefault(declaration => declaration.DeclaredName == propertyDataName);
            if (multipleFieldDeclaration != null && multipleFieldDeclaration.Parent != null)
            {

                var propertyComment = _factory.CreateDocCommentBlock(string.Format(DocumentationPatterns.PropertyData, propertyName));
                ModificationUtil.AddChildBefore(multipleFieldDeclaration, multipleFieldDeclaration.FirstChild, propertyComment);
            }

            // TODO: Move this behavoir to an extension method or helper class is duplicated.
            foreach (IAccessorDeclaration accessorDeclaration in propertyDeclaration.AccessorDeclarations)
            {
                accessorDeclaration.SetBody(
                    accessorDeclaration.Kind == AccessorKind.GETTER
                        ? _factory.CreateBlock(
                            ImplementationPatterns.PropertyGetAccessor, 
                            propertyDeclaration.DeclaredElement.Type, 
                            propertyName)
                        : _factory.CreateBlock(ImplementationPatterns.PropertySetAccessor, propertyName));
            }
        }

        #endregion

        #region Methods
        private string ComputeMemberName(string memberNameBase)
        {
            var memberName = memberNameBase;
            int idx = 0;
            while (
                _classDeclaration.MemberDeclarations.ToList()
                    .Exists(declaration => declaration.DeclaredName == memberName))
            {
                memberName = memberNameBase + idx;
                idx++;
            }

            return memberName;
        }

        #endregion
    }
}