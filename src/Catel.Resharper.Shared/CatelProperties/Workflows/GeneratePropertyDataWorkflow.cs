// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratePropertyDataWorkflow.cs" company="Catel development team">
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
#if R90
    using JetBrains.ReSharper.Feature.Services.Generate.UI.New;
#endif
    using JetBrains.ReSharper.Psi;

#if R70 || R71 || R80 || R81 || R82 || R90
    using JetBrains.UI.Icons;
#elif R61
    using System.Drawing;
#endif

    using DataConstants = JetBrains.ProjectModel.DataContext.DataConstants;

    public class GeneratePropertyDataWorkflow : StandardGenerateActionWorkflow
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private const string Description = "Generate Catel properties from available auto properties";

        private const string WindowTitle = "Generate Catel properties";

        private const string MenuText = "Catel properties";

        #region Constructors and Destructors

#if R70 || R71 || R80 || R81 || R82 || R90
        public GeneratePropertyDataWorkflow(IconId icon)
            : base(WellKnownGenerationActionKinds.GenerateCatelDataProperties, icon, MenuText, GenerateActionGroup.CLR_LANGUAGE, WindowTitle, Description, GeneratePropertyDataAction.Id)
#elif R61
        public GeneratePropertyDataWorkflow(Image icon)
            : base(WellKnownGenerationActionKinds.GenerateCatelDataProperties, icon, MenuText, GenerateActionGroup.CLR_LANGUAGE, WindowTitle, Description, GeneratePropertyDataAction.Id)
#endif
        {
        }

        #endregion

        #region Public Properties
        public override double Order
        {
            get { return 100; }
        }
        #endregion

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
    }
}