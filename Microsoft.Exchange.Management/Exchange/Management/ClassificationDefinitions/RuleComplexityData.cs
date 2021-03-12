using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000846 RID: 2118
	[Serializable]
	internal class RuleComplexityData
	{
		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x0600497B RID: 18811 RVA: 0x0012E1FC File Offset: 0x0012C3FC
		// (set) Token: 0x0600497C RID: 18812 RVA: 0x0012E204 File Offset: 0x0012C404
		internal int DistinctRegexProcessorReferencesCount { get; private set; }

		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x0600497D RID: 18813 RVA: 0x0012E20D File Offset: 0x0012C40D
		// (set) Token: 0x0600497E RID: 18814 RVA: 0x0012E215 File Offset: 0x0012C415
		internal int TermsFromCustomKeywordProcessorReferencesCount { get; private set; }

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x0600497F RID: 18815 RVA: 0x0012E21E File Offset: 0x0012C41E
		// (set) Token: 0x06004980 RID: 18816 RVA: 0x0012E226 File Offset: 0x0012C426
		internal int DistinctFunctionProcessorReferencesCount { get; private set; }

		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06004981 RID: 18817 RVA: 0x0012E22F File Offset: 0x0012C42F
		// (set) Token: 0x06004982 RID: 18818 RVA: 0x0012E237 File Offset: 0x0012C437
		internal int AnyBlocksCount { get; private set; }

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06004983 RID: 18819 RVA: 0x0012E240 File Offset: 0x0012C440
		// (set) Token: 0x06004984 RID: 18820 RVA: 0x0012E248 File Offset: 0x0012C448
		internal int MaxAnyBlocksDepth { get; private set; }

		// Token: 0x06004985 RID: 18821 RVA: 0x0012E268 File Offset: 0x0012C468
		private void ProcessAnyBlocksComplexityData(XElement ruleElement)
		{
			List<XElement> list = ruleElement.Descendants(RuleComplexityData.AnyElementQualifiedName).AsParallel<XElement>().ToList<XElement>();
			List<XElement> list2 = (from anyElement in list
			where !anyElement.Elements(RuleComplexityData.AnyElementQualifiedName).Any<XElement>()
			select anyElement).ToList<XElement>();
			int maxAnyBlocksDepth = (list2.Count > 0) ? list2.Max(new Func<XElement, int>(RuleComplexityData.CalculateAnyNestingDepth)) : 0;
			this.AnyBlocksCount = list.Count;
			this.MaxAnyBlocksDepth = maxAnyBlocksDepth;
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x0012E2E8 File Offset: 0x0012C4E8
		private static int CalculateAnyNestingDepth(XElement anyLeafElement)
		{
			int num = 0;
			XElement xelement = anyLeafElement;
			while (xelement.Parent != null && xelement.Parent.Name.Equals(RuleComplexityData.AnyElementQualifiedName))
			{
				num++;
				xelement = xelement.Parent;
			}
			return num;
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x0012E328 File Offset: 0x0012C528
		private static bool IsRegexProcessorReference(string textProcessorRef, Dictionary<TextProcessorType, TextProcessorGrouping> oobTextProcessorGroups, Dictionary<TextProcessorType, TextProcessorGrouping> customTextProcessorGroups)
		{
			TextProcessorGrouping textProcessorGrouping;
			return (customTextProcessorGroups.TryGetValue(TextProcessorType.Regex, out textProcessorGrouping) && textProcessorGrouping.Contains(textProcessorRef)) || (oobTextProcessorGroups.TryGetValue(TextProcessorType.Regex, out textProcessorGrouping) && textProcessorGrouping.Contains(textProcessorRef));
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x0012E360 File Offset: 0x0012C560
		private static bool IsFunctionProcessorReference(string textProcessorRef, Dictionary<TextProcessorType, TextProcessorGrouping> oobTextProcessorGroups)
		{
			TextProcessorGrouping textProcessorGrouping;
			return oobTextProcessorGroups.TryGetValue(TextProcessorType.Function, out textProcessorGrouping) && textProcessorGrouping.Contains(textProcessorRef);
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x0012E384 File Offset: 0x0012C584
		private static int GetTermsFromCustomKeywordProcessorCount(string textProcessorRef, Dictionary<TextProcessorType, TextProcessorGrouping> customTextProcessorGroups, Dictionary<string, int> customKeywordProcessorsTermsCount)
		{
			int result = 0;
			TextProcessorGrouping textProcessorGrouping;
			if (customTextProcessorGroups.TryGetValue(TextProcessorType.Keyword, out textProcessorGrouping) && textProcessorGrouping.Contains(textProcessorRef))
			{
				bool condition = customKeywordProcessorsTermsCount.TryGetValue(textProcessorRef, out result);
				ExAssert.RetailAssert(condition, "The terms count lookup for custom keyword processor has unexpectedly failed!");
			}
			return result;
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x0012E3C8 File Offset: 0x0012C5C8
		internal static RuleComplexityData Create(XElement ruleElement, Dictionary<TextProcessorType, TextProcessorGrouping> customTextProcessorGroups, Dictionary<string, int> customKeywordProcessorsTermsCount, Dictionary<TextProcessorType, TextProcessorGrouping> oobTextProcessorGroups = null)
		{
			if (ruleElement == null)
			{
				throw new ArgumentNullException("ruleElement");
			}
			if (customTextProcessorGroups == null)
			{
				throw new ArgumentNullException("customTextProcessorGroups");
			}
			if (customKeywordProcessorsTermsCount == null)
			{
				throw new ArgumentNullException("customKeywordProcessorsTermsCount");
			}
			oobTextProcessorGroups = (oobTextProcessorGroups ?? TextProcessorUtils.OobProcessorsGroupedByType);
			List<string> list = (from versionedTextProcessorReference in TextProcessorUtils.GetTextProcessorReferences(ruleElement)
			select versionedTextProcessorReference.Key).Distinct(ClassificationDefinitionConstants.TextProcessorIdComparer).ToList<string>();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (string textProcessorRef in list)
			{
				if (RuleComplexityData.IsRegexProcessorReference(textProcessorRef, oobTextProcessorGroups, customTextProcessorGroups))
				{
					num++;
				}
				else if (RuleComplexityData.IsFunctionProcessorReference(textProcessorRef, oobTextProcessorGroups))
				{
					num3++;
				}
				else
				{
					num2 += RuleComplexityData.GetTermsFromCustomKeywordProcessorCount(textProcessorRef, customTextProcessorGroups, customKeywordProcessorsTermsCount);
				}
			}
			RuleComplexityData ruleComplexityData = new RuleComplexityData
			{
				DistinctRegexProcessorReferencesCount = num,
				TermsFromCustomKeywordProcessorReferencesCount = num2,
				DistinctFunctionProcessorReferencesCount = num3
			};
			ruleComplexityData.ProcessAnyBlocksComplexityData(ruleElement);
			return ruleComplexityData;
		}

		// Token: 0x04002C4B RID: 11339
		private static readonly XName AnyElementQualifiedName = XmlProcessingUtils.GetMceNsQualifiedNodeName("Any");
	}
}
