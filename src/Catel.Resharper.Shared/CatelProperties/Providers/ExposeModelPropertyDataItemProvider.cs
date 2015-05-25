// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExposeModelPropertyDataItemProvider.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.Providers
{
    using System.Collections.Generic;

    using Catel.Logging;
    using Catel.ReSharper.CatelProperties.Workflows;

    using JetBrains.Application.DataContext;
    using JetBrains.ProjectModel;
    using JetBrains.ReSharper.Feature.Services.Generate.Actions;
    using JetBrains.ReSharper.Psi;

#if R70 || R71 || R80 || R81 || R82 || R90
    using JetBrains.UI.Icons;
#elif R61
    using System.Drawing;
    using JetBrains.ReSharper.Psi.DeclaredElements;
#endif

    using DataConstants = JetBrains.ProjectModel.DataContext.DataConstants;

    [GenerateProvider]
    public class ExposeModelPropertyDataItemProvider : IGenerateActionProvider
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public IEnumerable<IGenerateActionWorkflow> CreateWorkflow(IDataContext dataContext)
        {
            Argument.IsNotNull(() => dataContext);

            var solution = dataContext.GetData(DataConstants.SOLUTION);
            var iconManager = solution.GetComponent<PsiIconManager>();
            #if R61
            var icon = iconManager.GetImage(CLRDeclaredElementType.PROPERTY);
            #else
            var icon = iconManager.GetImage(CLRDeclaredElementType.PROPERTY);
            #endif

            yield return new ExposeModelPropertyDataWorkflow(icon);
        }
    }
}