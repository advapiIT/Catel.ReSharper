// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratePropertyDataAction.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.Actions
{
    using Catel.Logging;
    using Catel.ReSharper.CatelProperties.Providers;

    using JetBrains.ActionManagement;
    using JetBrains.ReSharper.Feature.Services.Generate.Actions;
    using JetBrains.UI.RichText;

#if R8X
    using JbActionAttribute = JetBrains.ActionManagement.ActionHandlerAttribute;
#else
    using JbActionAttribute = JetBrains.UI.ActionsRevised.ActionAttribute;
#endif

    [JbActionAttribute(Id)]
    public class GeneratePropertyDataAction : GenerateActionBase<GeneratePropertyDataItemProvider>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public const string Id = "Generate.PropertyData";

        protected override RichText Caption
        {
            get { return "Generate Catel properties"; }
        }

        protected override bool ShowMenuWithOneItem
        {
            get { return true; }
        }
    }
}