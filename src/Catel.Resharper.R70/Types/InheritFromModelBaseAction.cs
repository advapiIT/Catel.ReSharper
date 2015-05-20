﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InheritFromModelBaseAction.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.Types
{
    using Catel.ReSharper.Identifiers;

#if R90
    using JetBrains.ReSharper.Feature.Services.ContextActions;
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
#else
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
#endif

    /// <summary>
    ///     The convert to data object base action.
    /// </summary>
    [ContextAction(Name = Name, Group = "C#", Description = Description, Priority = -20)]
    public sealed class InheritFromModelBaseAction : InheritFromActionBase
    {
        #region Constants

        /// <summary>
        /// The description.
        /// </summary>
        private const string Description = "InheritFromModelBaseActionDescription";

        /// <summary>
        /// The name.
        /// </summary>
        private const string Name = "InheritFromModelBaseAction";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InheritFromModelBaseAction"/> class. 
        /// Initializes a new instance of the <see cref="InheritFromDataObjectBaseAction"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        public InheritFromModelBaseAction(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets SuperTypeName.
        /// </summary>
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