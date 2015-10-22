// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratePropertyDataItemProvider.cs" company="Catel development team">
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
    using JetBrains.UI.Icons;

    using DataConstants = JetBrains.ProjectModel.DataContext.DataConstants;

    [GenerateProvider]
    public class GeneratePropertyDataItemProvider : IGenerateActionProvider
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #region Public Methods and Operators

        public IEnumerable<IGenerateActionWorkflow> CreateWorkflow(IDataContext dataContext)
        {
            Argument.IsNotNull(() => dataContext);

            var solution = dataContext.GetData(DataConstants.SOLUTION);
            var iconManager = solution.GetComponent<PsiIconManager>();
            var icon = iconManager.GetImage(CLRDeclaredElementType.PROPERTY);

            yield return new GeneratePropertyDataWorkflow(icon);
        }

        #endregion
    }
}