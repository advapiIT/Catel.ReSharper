// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyDataBuilderBase.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.ReSharper.CatelProperties.CSharp.Builders
{
    using System.Collections.Generic;

    using JetBrains.ReSharper.Feature.Services.CSharp.Generate;
    using JetBrains.ReSharper.Feature.Services.Generate;

    internal abstract class PropertyDataBuilderBase : GeneratorBuilderBase<CSharpGeneratorContext>
    {
        protected override IList<IGeneratorOption> GetGlobalOptions(CSharpGeneratorContext context)
        {
            return GetGeneratorOptions();
        }

        private static IList<IGeneratorOption> GetGeneratorOptions()
        {
            return new List<IGeneratorOption>
                {
                    new GeneratorOptionBoolean(OptionIds.IncludePropertyInSerialization, OptionTitles.IncludePropertyInSerialization, true) { Persist = false }, 
                    new GeneratorOptionBoolean(OptionIds.ImplementPropertyChangedNotificationMethod, OptionTitles.ImplementPropertyChangedNotificationMethod, false) { Persist = false }, 
                    new GeneratorOptionBoolean(OptionIds.ForwardEventArgumentToImplementedPropertyChangedNotificationMethod, OptionTitles.ForwardEventArgumentToImplementedPropertyChangedNotificationMethod, false) { Persist = false }
                };
        }

        protected static class OptionTitles
        {
            public const string ForwardEventArgumentToImplementedPropertyChangedNotificationMethod = "&Forward property changed event argument to notification method";

            public const string ImplementPropertyChangedNotificationMethod = "Implement property changed &notification method";

            public const string IncludePropertyInSerialization = "Include property on &serialization";
        }

        protected static class OptionIds
        {
            public const string ImplementPropertyChangedNotificationMethod = "ImplementPropertyChangedNotificationMethod";

            public const string ForwardEventArgumentToImplementedPropertyChangedNotificationMethod = "ForwardEventArgumentToImplementedPropertyChangedNotificationMethod";

            public const string IncludePropertyInSerialization = "IncludePropertyOnSerialization";
        }
    }
}