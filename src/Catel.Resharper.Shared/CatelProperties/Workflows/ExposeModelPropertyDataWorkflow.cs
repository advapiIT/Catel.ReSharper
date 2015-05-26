// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExposeModelPropertyDataWorkflow.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.Workflows
{
    using Catel.Logging;
    using Catel.ReSharper.CatelProperties.Actions;

    using JetBrains.Application.DataContext;
    using JetBrains.ProjectModel;
    using JetBrains.ReSharper.Feature.Services.Generate;
    using JetBrains.ReSharper.Feature.Services.Generate.Actions;
    using JetBrains.ReSharper.Psi;
    using JetBrains.UI.Icons;

#if R90 || R91
    using JetBrains.ReSharper.Feature.Services.Generate.UI.New;
#endif

    using DataConstants = JetBrains.ProjectModel.DataContext.DataConstants;

    public class ExposeModelPropertyDataWorkflow : StandardGenerateActionWorkflow
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private const string Description = "Expose properties from available models";

        private const string WindowTitle = "Expose model properties";

        private const string MenuText = "Expose model properties";

        public ExposeModelPropertyDataWorkflow(IconId icon)
            : base(WellKnownGenerationActionKinds.ExposeModelPropertiesAsCatelDataProperties, icon, MenuText, GenerateActionGroup.CLR_LANGUAGE, WindowTitle, Description, GeneratePropertyDataAction.Id)
        {
        }

        public override double Order
        {
            get { return 101; }
        }

        public override bool IsAvailable(IDataContext dataContext)
        {
            Argument.IsNotNull(() => dataContext);
            IGeneratorContextFactory generatorContextFactory = null;

            var solution = dataContext.GetData(DataConstants.SOLUTION);
            if (solution != null)
            {
                var generatorManager = GeneratorManager.GetInstance(solution);
                if (generatorManager != null)
                {
                    var languageType = generatorManager.GetPsiLanguageFromContext(dataContext);
                    if (languageType != null)
                    {
                        generatorContextFactory = LanguageManager.Instance.TryGetService<IGeneratorContextFactory>(languageType);
                    }
                }
            }

            return generatorContextFactory != null;
        }
    }
}