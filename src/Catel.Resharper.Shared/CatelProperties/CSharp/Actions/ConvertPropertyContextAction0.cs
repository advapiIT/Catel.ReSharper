// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConvertPropertyContextAction0.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.CSharp.Actions
{
    using Catel.Logging;

#if R8X
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
#else
    using JetBrains.ReSharper.Feature.Services.ContextActions;
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
#endif

    using JetBrains.ReSharper.Psi.CSharp.Tree;

    [ContextAction(Name = Name, Group = "C#", Description = Description, Priority = -20)]
    public sealed class ConvertPropertyContextAction0 : PropertyContextActionBase
    {
        #region Constants

        private const string Description = "ConvertPropertyContextAction0Description";

        private const string Name = "ConvertPropertyContextAction0";

        #endregion

        #region Static Fields

        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors and Destructors

        public ConvertPropertyContextAction0(ICSharpContextActionDataProvider provider)
            : base(provider)
        {
        }

        #endregion

        #region Public Properties

        public override string Text
        {
            get
            {
                return "To Catel property";
            }
        }

        #endregion

        #region Methods

        protected override void ConvertProperty(
            PropertyConverter propertyConverter, IPropertyDeclaration propertyDeclaration)
        {
            Argument.IsNotNull(() => propertyConverter);

            propertyConverter.Convert(propertyDeclaration);
        }

        #endregion
    }
}