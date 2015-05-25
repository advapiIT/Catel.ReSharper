// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExposeModelPropertyDataAction.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.Actions
{
    using Catel.Logging;
    using Catel.ReSharper.CatelProperties.Providers;

    using JetBrains.ActionManagement;
    using JetBrains.ReSharper.Feature.Services.Generate.Actions;
#if R90
    using JetBrains.UI.ActionsRevised;
#endif
    using JetBrains.UI.RichText;

#if R90 
    [Action(Id)]
#else
    [ActionHandler(Id)]
#endif
    public class ExposeModelPropertyDataAction : GenerateActionBase<ExposeModelPropertyDataItemProvider>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public const string Id = "Expose.ModelPropertyData";

        protected override RichText Caption
        {
            get { return "Expose model properties"; }
        }

        protected override bool ShowMenuWithOneItem
        {
            get { return true; }
        }
    }
}