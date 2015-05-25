// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RevertToAutoPropertyContextAction.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.CSharp.Actions
{
    using System;


    using JetBrains.Application.Progress;
    using JetBrains.ProjectModel;
    
#if R90
    using JetBrains.ReSharper.Feature.Services.ContextActions;
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
#else
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
#endif
    using JetBrains.TextControl;

    [ContextAction(Name = Name, Group = "C#", Description = Description, Priority = -23)]
    public sealed class RevertToAutoPropertyContextAction : FieldContextActionBase
    {
        #region Constants
        private const string Description = "RevertToAutoPropertyContextActionDescription";

        private const string Name = "RevertToAutoPropertyContextAction";

        #endregion

        #region Constructors and Destructors
        public RevertToAutoPropertyContextAction(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        #endregion

        #region Public Properties
        public override string Text
        {
            get
            {
                return "To auto property";
            }
        }

        #endregion

        #region Methods
        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            ClassDeclaration.RemoveClassMemberDeclaration(FieldDeclaration);

            PropertyDeclaration.AccessorDeclarations[0].SetBody(null);
            PropertyDeclaration.AccessorDeclarations[1].SetBody(null);

            return null;
        }
        protected override bool IsAvailable()
        {
            return PropertyDeclaration.HasDefaultCatelImplementation();
        }

        #endregion
    }
}