using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200085A RID: 2138
	internal sealed class ClassificationRuleCollectionLocalizedInfoValidator : LocalizedInfoValidator
	{
		// Token: 0x06004A18 RID: 18968 RVA: 0x00130FF0 File Offset: 0x0012F1F0
		protected override void InternalValidate(XDocument rulePackXDocument)
		{
			ExAssert.RetailAssert(rulePackXDocument != null, "Extra rule package validation must take place after XML schema validation passed!");
			ParallelQuery<string> langCodes = from localizedDetailsElement in rulePackXDocument.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("LocalizedDetails")).AsParallel<XElement>()
			select localizedDetailsElement.Attribute("langcode").Value;
			base.ValidateResourceLangCodes(langCodes, (IEnumerable<string> invalidLangCodes) => Strings.ClassificationRuleCollectionInvalidLangCodesInRulePackDescriptor(string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, invalidLangCodes)), (IEnumerable<string> nonUniqueLangCodes) => Strings.ClassificationRuleCollectionNonUniqueLangCodesInRulePackDescriptor(string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, nonUniqueLangCodes)));
		}
	}
}
