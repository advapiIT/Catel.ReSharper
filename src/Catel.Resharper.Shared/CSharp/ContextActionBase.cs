// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextActionBase.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CSharp
{
#if R90
    using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
#endif

#if R70 || R71 || R80 || R81 || R82
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;

#elif R61
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
    using JetBrains.Util;
#endif

    public abstract class ContextActionBase : 
#if R90
        JetBrains.ReSharper.Feature.Services.ContextActions.ContextActionBase
#elif R70 || R71 || R80 || R81 || R82 
        JetBrains.ReSharper.Intentions.Extensibility.ContextActionBase
#elif R61
        BulbItemImpl, IContextAction
#endif
    {
        protected ContextActionBase(ICSharpContextActionDataProvider provider)
        {
            Argument.IsNotNull(() => provider);

            Provider = provider;
        }

        protected ICSharpContextActionDataProvider Provider { get; private set; }

#if R61
        public abstract bool IsAvailable(IUserDataHolder cache);
#endif
    }
}