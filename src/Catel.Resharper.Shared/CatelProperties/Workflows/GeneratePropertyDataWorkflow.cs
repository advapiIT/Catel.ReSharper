// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratePropertyDataWorkflow.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.Workflows
{

    using Catel.ReSharper.CatelProperties.Actions;

    using JetBrains.Application.DataContext;
    using JetBrains.ReSharper.Feature.Services.Generate;
    using JetBrains.ReSharper.Feature.Services.Generate.Actions;
    using JetBrains.ReSharper.Psi;
    using JetBrains.UI.Icons;

#if R92
    using JetBrains.ReSharper.Feature.Services.Generate.Workflows;
#endif


    using DataConstants = JetBrains.ProjectModel.DataContext.DataConstants;

#if R9X
    using JetBrains.ReSharper.Feature.Services.Generate.UI.New;
#endif

#if R92
    public class GeneratePropertyDataWorkflow : GenerateCodeWorkflowBase
#else
    public class GeneratePropertyDataWorkflow : StandardGenerateActionWorkflow
#endif
    {
        private const string Description = "Generate Catel properties from available auto properties";

        private const string WindowTitle = "Generate Catel properties";

        private const string MenuText = "Catel properties";

        #region Constructors and Destructors

        public GeneratePropertyDataWorkflow(IconId icon)
            : base(WellKnownGenerationActionKinds.GenerateCatelDataProperties, icon, MenuText, GenerateActionGroup.CLR_LANGUAGE, WindowTitle, Description, GeneratePropertyDataAction.Id)
        {
        }

        #endregion

        #region Public Properties
        public override double Order
        {
            get { return 100; }
        }

        #endregion

#if R81 || R82 || R90 || R91 

        #region Public Methods and Operators

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

        #endregion
#endif
    }
}