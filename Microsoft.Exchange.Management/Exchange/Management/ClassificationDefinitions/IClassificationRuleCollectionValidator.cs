using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200083F RID: 2111
	internal interface IClassificationRuleCollectionValidator
	{
		// Token: 0x06004951 RID: 18769
		void Validate(ValidationContext context, XDocument rulePackXDocument);
	}
}
