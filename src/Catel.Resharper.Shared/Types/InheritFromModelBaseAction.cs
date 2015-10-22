// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InheritFromModelBaseAction.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Types
{
    using Catel.ReSharper.Identifiers;

#if R8X
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
#else
    using JetBrains.ReSharper.Feature.Services.ContextActions;
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
#endif

    [ContextAction(Name = Name, Group = "C#", Description = Description, Priority = -20)]
    public sealed class InheritFromModelBaseAction : InheritFromActionBase
    {
        #region Constants

        private const string Description = "InheritFromModelBaseActionDescription";

        private const string Name = "InheritFromModelBaseAction";

        #endregion

        #region Constructors and Destructors

        public InheritFromModelBaseAction(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        #endregion

        #region Properties

        protected override string SuperTypeName
        {
            get
            {
                return CatelCore.ModelBase;
            }
        }

        #endregion
    }
}