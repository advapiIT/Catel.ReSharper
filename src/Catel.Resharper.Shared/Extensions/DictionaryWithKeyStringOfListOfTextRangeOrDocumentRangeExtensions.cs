// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DictionaryWithKeyStringOfListOfTextRangeOrDocumentRangeExtensions.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Catel.ReSharper
{
    using System.Collections.Generic;
    using System.Linq;

#if R80
    using JetBrains.Util;
    using JetBrains.DocumentModel;
#endif
    using JetBrains.ReSharper.Feature.Services.LiveTemplates.Hotspots;

#if R81 || R82 || R90
    using JetBrains.DocumentModel;
    using JetBrains.ReSharper.Feature.Services.LiveTemplates.Templates;
#endif

    using JetBrains.ReSharper.LiveTemplates;
    using JetBrains.Util;

    internal static class DictionaryWithKeyStringOfListOfTextRangeOrDocumentRangeExtensions
    {
        #region Public Methods and Operators
#if R81 || R82 || R90
        public static HotspotInfo[] AsHotspotInfos(this Dictionary<string, List<DocumentRange>> @this)
        {
            Argument.IsNotNull(() => @this);

            return (from fieldName in @this.Keys
                    let nameSuggestionsExpression = new NameSuggestionsExpression(new[] { " " })
                    let field = new TemplateField(fieldName, nameSuggestionsExpression, 0)
                    where @this[fieldName].Count > 0
                    select new HotspotInfo(field, @this[fieldName])).ToArray();
        }
#else
        public static HotspotInfo[] AsHotspotInfos(this Dictionary<string, List<TextRange>> @this)
        {
            Argument.IsNotNull(() => @this);

            return (from fieldName in @this.Keys
                    let nameSuggestionsExpression = new NameSuggestionsExpression(new[] { " " })
                    let field = new TemplateField(fieldName, nameSuggestionsExpression, 0)
                    where @this[fieldName].Count > 0
                    select new HotspotInfo(field, @this[fieldName])).ToArray();
        }
#endif

#if R81 || R82 || R90
        public static void Merge(this Dictionary<string, List<DocumentRange>> @this, Dictionary<string, List<DocumentRange>> fields)
        {
            Argument.IsNotNull(() => @this);

            if (fields != null)
            {
                foreach (var fieldName in fields.Keys)
                {
                    List<DocumentRange> textRanges = fields[fieldName];
                    if (@this.ContainsKey(fieldName))
                    {
                        @this[fieldName].AddRange(textRanges);
                    }
                    else
                    {
                        @this.Add(fieldName, textRanges);
                    }
                }
            }
        }

#else
        public static void Merge(this Dictionary<string, List<TextRange>> @this, Dictionary<string, List<TextRange>> fields)
        {
            Argument.IsNotNull(() => @this);

            if (fields != null)
            {
                foreach (var fieldName in fields.Keys)
                {
                    List<TextRange> textRanges = fields[fieldName];
                    if (@this.ContainsKey(fieldName))
                    {
                        @this[fieldName].AddRange(textRanges);
                    }
                    else
                    {
                        @this.Add(fieldName, textRanges);
                    }
                }
            }
        }
#endif

        #endregion
    }
}