// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentContextActionBase.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Arguments
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    using Catel.Logging;
    using Catel.ReSharper.CSharp;
    using Catel.ReSharper.Identifiers;

    using JetBrains.Application;
    using JetBrains.Application.Progress;
#if R80 || R81 || R82 ||R90
    using JetBrains.DocumentModel;
#endif
    using JetBrains.ProjectModel;
#if R90
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
    using JetBrains.ReSharper.Resources.Shell;
#endif
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
    using JetBrains.ReSharper.Feature.Services.LiveTemplates.Hotspots;
    using JetBrains.ReSharper.Feature.Services.LiveTemplates.LiveTemplates;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
    using JetBrains.ReSharper.Psi.Tree;
    using JetBrains.TextControl;
    using JetBrains.Util;

    public abstract class ArgumentContextActionBase : ContextActionBase
    {
        #region Static Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #endregion

        #region Fields
        private ICSharpFunctionDeclaration _methodDeclaration;

        private IRegularParameterDeclaration _parameterDeclaration;

        #endregion

        #region Constructors and Destructors
        protected ArgumentContextActionBase(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override sealed bool IsAvailable(IUserDataHolder cache)
        {
            using (ReadLockCookie.Create())
            {
                if (this.Provider.SelectedElement != null)
                {
#if R80 || R81 || R82 || R90
                    IDeclaredType catelArgumentType = TypeFactory.CreateTypeByCLRName(CatelCore.Argument, this.Provider.PsiModule, this.Provider.SelectedElement.GetResolveContext());
#else
                    IDeclaredType catelArgumentType = TypeFactory.CreateTypeByCLRName(CatelCore.Argument, this.Provider.PsiModule);
#endif
                    this._parameterDeclaration = null;
                    this._methodDeclaration = null;

                    if (catelArgumentType.GetTypeElement() != null)
                    {
                        if (this.Provider.SelectedElement != null && this.Provider.SelectedElement.Parent is IRegularParameterDeclaration)
                        {
                            this._parameterDeclaration = this.Provider.SelectedElement.Parent as IRegularParameterDeclaration;
                            if (this._parameterDeclaration.Parent != null && this._parameterDeclaration.Parent != null
                                && this._parameterDeclaration.Parent.Parent is ICSharpFunctionDeclaration)
                            {
                                this._methodDeclaration =
                                    this._parameterDeclaration.Parent.Parent as ICSharpFunctionDeclaration;
                            }
                        }
                    }
                    
                }
            }

            return this._parameterDeclaration != null && this.IsArgumentTypeTheExpected(this._parameterDeclaration.Type)
                   && this._methodDeclaration != null
                   && !this.IsArgumentChecked(this._methodDeclaration, this._parameterDeclaration);
        }

        #endregion

        #region Methods
        protected abstract ICSharpStatement CreateArgumentCheckStatement(
            IRegularParameterDeclaration parameterDeclaration);

        protected abstract string CreateExceptionXmlDoc(IRegularParameterDeclaration parameterDeclaration);

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            Argument.IsNotNull(() => solution);
            Argument.IsNotNull(() => progress);
#if !R90
            IDocCommentBlockNode exceptionCommentBlock = null;
#else
            IDocCommentBlock exceptionCommentBlock = null;
#endif
            XmlNode xmlDoc = this._methodDeclaration.GetXMLDoc(false);
            if (xmlDoc == null || !this.IsArgumentCheckDocumented(xmlDoc, this._parameterDeclaration))
            {
                // TODO: Move out the initialization of the exception block down (CreateExceptionXmlDoc).
                exceptionCommentBlock =
                    this.Provider.ElementFactory.CreateDocCommentBlock(
                        this.CreateExceptionXmlDoc(this._parameterDeclaration));

                // TODO: Detect the right position to insert the document node.
#if !R90
                if (this._methodDeclaration.FirstChild is IDocCommentBlockNode)
#else
                if (this._methodDeclaration.FirstChild is IDocCommentBlock)
#endif
                {
                    ITreeNode lastChild = this._methodDeclaration.FirstChild.LastChild;
                    if (lastChild != null)
                    {
                        exceptionCommentBlock = ModificationUtil.AddChildAfter(lastChild, exceptionCommentBlock);
                    }
                }
                else if (this._methodDeclaration.Parent != null)
                {
                    exceptionCommentBlock = ModificationUtil.AddChildBefore(this._methodDeclaration.Parent, this._methodDeclaration.FirstChild, exceptionCommentBlock);
                }
            }

            // TODO: Detect the right position to insert the code.
            ITreeNode methodBodyFirstChild = this._methodDeclaration.Body.FirstChild;
#if R81 || R82 || R90
            Dictionary<string, List<DocumentRange>> fields = null;
#else
            Dictionary<string, List<TextRange>> fields = null;
#endif
            if (methodBodyFirstChild != null)
            {
                ICSharpStatement checkStatement = ModificationUtil.AddChildAfter(methodBodyFirstChild, this.CreateArgumentCheckStatement(this._parameterDeclaration));
                fields = checkStatement.GetFields();
                if (exceptionCommentBlock != null)
                {
                    fields.Merge(exceptionCommentBlock.GetFields());
                }
            }
            
            HotspotInfo[] hotspotInfos = fields != null ? fields.AsHotspotInfos() : new HotspotInfo[] { };
            return hotspotInfos.Length == 0
                       ? (Action<ITextControl>)null
                       : textControl =>
                       {
                           HotspotSession hotspotSession =
                               Shell.Instance.GetComponent<LiveTemplatesManager>()
                                    .CreateHotspotSessionAtopExistingText(
                                        solution,
                                        TextRange.InvalidRange,
                                        textControl,
                                        LiveTemplatesManager.EscapeAction.LeaveTextAndCaret,
                                        hotspotInfos);
                           hotspotSession.Execute();
                       };
        }

        protected abstract bool IsArgumentCheckDocumented(XmlNode xmlDocOfTheMethod, IRegularParameterDeclaration parameterDeclaration);

        protected abstract bool IsArgumentChecked(ICSharpFunctionDeclaration methodDeclaration, IRegularParameterDeclaration parameterDeclaration);

        protected abstract bool IsArgumentTypeTheExpected(IType type);

        #endregion
    }
}