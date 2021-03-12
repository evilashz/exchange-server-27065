using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000865 RID: 2149
	internal class DataClassificationLocalizedInfoValidator : LocalizedInfoValidator
	{
		// Token: 0x06004A4D RID: 19021 RVA: 0x00131B94 File Offset: 0x0012FD94
		private void ValidateRuleLangCodes(XElement ruleResourceElement)
		{
			ExAssert.RetailAssert(ruleResourceElement != null, "The resource element to be validated must not be null!");
			string resourceIdRef = ruleResourceElement.Attribute("idRef").Value;
			IEnumerable<string> langCodes = from nameElement in ruleResourceElement.Elements(XmlProcessingUtils.GetMceNsQualifiedNodeName("Name"))
			select nameElement.Attribute("langcode").Value;
			base.ValidateResourceLangCodes(langCodes, (IEnumerable<string> invalidLangCodes) => Strings.ClassificationRuleCollectionInvalidLangCodesInRuleName(resourceIdRef, string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, invalidLangCodes)), (IEnumerable<string> nonUniqueLangCodes) => Strings.ClassificationRuleCollectionNonUniqueLangCodesInRuleName(resourceIdRef, string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, nonUniqueLangCodes)));
			IEnumerable<string> langCodes2 = from descriptionElement in ruleResourceElement.Elements(XmlProcessingUtils.GetMceNsQualifiedNodeName("Description"))
			select descriptionElement.Attribute("langcode").Value;
			base.ValidateResourceLangCodes(langCodes2, (IEnumerable<string> invalidLangCodes) => Strings.ClassificationRuleCollectionInvalidLangCodesInRuleDescription(resourceIdRef, string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, invalidLangCodes)), (IEnumerable<string> nonUniqueLangCodes) => Strings.ClassificationRuleCollectionNonUniqueLangCodesInRuleDescription(resourceIdRef, string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, nonUniqueLangCodes)));
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x00131D64 File Offset: 0x0012FF64
		private static void ValidateDefaultValueCount(XDocument rulePackXDocument)
		{
			List<string> list = (from ruleResourceElement in rulePackXDocument.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Resource")).AsParallel<XElement>().Where(delegate(XElement ruleResourceElement)
			{
				if (ruleResourceElement.Elements(XmlProcessingUtils.GetMceNsQualifiedNodeName("Name")).Count((XElement nameElement) => nameElement.Attribute("default") != null && (bool)nameElement.Attribute("default")) == 1)
				{
					return ruleResourceElement.Elements(XmlProcessingUtils.GetMceNsQualifiedNodeName("Description")).Count((XElement descriptionElement) => descriptionElement.Attribute("default") != null && (bool)descriptionElement.Attribute("default")) != 1;
				}
				return true;
			})
			select ruleResourceElement.Attribute("idRef").Value).ToList<string>();
			if (list.Count > 0)
			{
				LocalizedString message = Strings.ClassificationRuleCollectionIncorrectNumberOfDefaultInRuleResources(string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, list));
				throw ClassificationDefinitionUtils.PopulateExceptionSource<ClassificationRuleCollectionLocalizationInfoValidationException, List<string>>(new ClassificationRuleCollectionLocalizationInfoValidationException(message), list);
			}
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x00131DFC File Offset: 0x0012FFFC
		private void ValidateAllRulesLangCodes(XDocument rulePackXDocument)
		{
			foreach (XElement ruleResourceElement in rulePackXDocument.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Resource")))
			{
				this.ValidateRuleLangCodes(ruleResourceElement);
			}
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x001321DC File Offset: 0x001303DC
		private static void ValidateDefaultValueLangCodesConsistency(XDocument rulePackXDocument)
		{
			List<string> list = (from ruleResourceElement in rulePackXDocument.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Resource")).AsParallel<XElement>()
			let defaultNameLangCode = ruleResourceElement.Elements(XmlProcessingUtils.GetMceNsQualifiedNodeName("Name")).Single((XElement nameElement) => nameElement.Attribute("default") != null && (bool)nameElement.Attribute("default")).Attribute("langcode").Value
			let defaultDescriptionLangCode = ruleResourceElement.Elements(XmlProcessingUtils.GetMceNsQualifiedNodeName("Description")).Single((XElement descriptionElement) => descriptionElement.Attribute("default") != null && (bool)descriptionElement.Attribute("default")).Attribute("langcode").Value
			where !new CultureInfo(defaultNameLangCode).Equals(new CultureInfo(defaultDescriptionLangCode))
			select ruleResourceElement.Attribute("idRef").Value).ToList<string>();
			if (list.Count > 0)
			{
				LocalizedString message = Strings.ClassificationRuleCollectionInconsistentDefaultInRuleResource(string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, list));
				throw ClassificationDefinitionUtils.PopulateExceptionSource<ClassificationRuleCollectionLocalizationInfoValidationException, List<string>>(new ClassificationRuleCollectionLocalizationInfoValidationException(message), list);
			}
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x001322B8 File Offset: 0x001304B8
		protected override void InternalValidate(XDocument rulePackXDocument)
		{
			ExAssert.RetailAssert(rulePackXDocument != null, "Extra rule package validation must take place after XML schema validation passed!");
			DataClassificationLocalizedInfoValidator.ValidateDefaultValueCount(rulePackXDocument);
			this.ValidateAllRulesLangCodes(rulePackXDocument);
			DataClassificationLocalizedInfoValidator.ValidateDefaultValueLangCodesConsistency(rulePackXDocument);
		}
	}
}
