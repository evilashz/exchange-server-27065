using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200085D RID: 2141
	internal sealed class ComplexityValidator : IClassificationRuleCollectionValidator
	{
		// Token: 0x06004A24 RID: 18980 RVA: 0x001312F0 File Offset: 0x0012F4F0
		internal ComplexityValidator(Dictionary<TextProcessorType, TextProcessorGrouping> cannedOobTextProcessorIdsGroupedByType, IList<IDataClassificationComplexityValidator> dataClassificationComplexityValidators)
		{
			if (cannedOobTextProcessorIdsGroupedByType == null)
			{
				throw new ArgumentNullException("cannedOobTextProcessorIdsGroupedByType");
			}
			if (dataClassificationComplexityValidators == null)
			{
				throw new ArgumentNullException("dataClassificationComplexityValidators");
			}
			if (dataClassificationComplexityValidators.Any((IDataClassificationComplexityValidator complexityValidator) => null == complexityValidator))
			{
				throw new ArgumentException(new ArgumentException().Message, "dataClassificationComplexityValidators");
			}
			this.cannedOobTextProcessorIdsGroupedByType = cannedOobTextProcessorIdsGroupedByType;
			this.dataClassificationComplexityValidators = dataClassificationComplexityValidators;
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x001313A0 File Offset: 0x0012F5A0
		private static Dictionary<string, int> GetKeywordProcessorTermsCount(XDocument rulePackXDocument)
		{
			return (from keywordElement in rulePackXDocument.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Keyword"))
			select keywordElement).ToDictionary((XElement keywordElement) => keywordElement.Attribute("id").Value, (XElement keywordElement) => keywordElement.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Term")).AsParallel<XElement>().Count<XElement>());
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x00131480 File Offset: 0x0012F680
		private IEnumerable<KeyValuePair<string, RuleComplexityData>> GetRulePackComplexityData(XDocument rulePackXDocument)
		{
			ExAssert.RetailAssert(rulePackXDocument != null, "The rule pack document instance passed to GetRulePackComplexityData cannot be null!");
			Dictionary<TextProcessorType, TextProcessorGrouping> customTextProcessorGroups = TextProcessorUtils.GetRulePackScopedTextProcessorsGroupedByType(rulePackXDocument).ToDictionary((TextProcessorGrouping textProcessorGroup) => textProcessorGroup.Key);
			Dictionary<string, int> customKeywordProcessorsTermsCount = ComplexityValidator.GetKeywordProcessorTermsCount(rulePackXDocument);
			IEnumerable<XElement> source = from rulePackElement in rulePackXDocument.Descendants()
			where ClassificationDefinitionConstants.MceRuleElementNames.Contains(rulePackElement.Name.LocalName)
			select rulePackElement;
			return from ruleElement in source.AsParallel<XElement>().AsOrdered<XElement>()
			select new KeyValuePair<string, RuleComplexityData>(ruleElement.Attribute("id").Value, RuleComplexityData.Create(ruleElement, customTextProcessorGroups, customKeywordProcessorsTermsCount, this.cannedOobTextProcessorIdsGroupedByType));
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x00131550 File Offset: 0x0012F750
		private static void ValidateRuleComplexity(IEnumerable<KeyValuePair<string, RuleComplexityData>> rulePackComplexityData, IDataClassificationComplexityValidator complexityValidator)
		{
			List<string> list = (from ruleComplexityData in rulePackComplexityData
			where complexityValidator.IsRuleComplexityLimitExceeded(ruleComplexityData.Value)
			select ruleComplexityData.Key).ToList<string>();
			if (list.Count > 0)
			{
				LocalizedString message = complexityValidator.CreateExceptionMessage(list);
				throw ClassificationDefinitionUtils.PopulateExceptionSource<ClassificationRuleCollectionComplexityValidationException, List<string>>(new ClassificationRuleCollectionComplexityValidationException(message), list);
			}
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x001315C8 File Offset: 0x0012F7C8
		public void Validate(ValidationContext context, XDocument rulePackXDocument)
		{
			if (context.DcValidationConfig == null || context.IsPayloadOobRuleCollection)
			{
				return;
			}
			DataClassificationConfig dcValidationConfig = context.DcValidationConfig;
			List<KeyValuePair<string, RuleComplexityData>> rulePackComplexityData = this.GetRulePackComplexityData(rulePackXDocument).ToList<KeyValuePair<string, RuleComplexityData>>();
			foreach (IDataClassificationComplexityValidator dataClassificationComplexityValidator in this.dataClassificationComplexityValidators)
			{
				dataClassificationComplexityValidator.Initialize(dcValidationConfig);
				ComplexityValidator.ValidateRuleComplexity(rulePackComplexityData, dataClassificationComplexityValidator);
			}
		}

		// Token: 0x04002C9E RID: 11422
		private readonly Dictionary<TextProcessorType, TextProcessorGrouping> cannedOobTextProcessorIdsGroupedByType;

		// Token: 0x04002C9F RID: 11423
		private readonly IList<IDataClassificationComplexityValidator> dataClassificationComplexityValidators;
	}
}
