using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x0200000E RID: 14
	public interface IClassificationRuleStore : IRulePackageLoader
	{
		// Token: 0x06000045 RID: 69
		RULE_PACKAGE_DETAILS[] GetRulePackageDetails(IClassificationItem classificationItem);

		// Token: 0x06000046 RID: 70
		RuleDefinitionDetails GetRuleDetails(string ruleId, string localeName = null);

		// Token: 0x06000047 RID: 71
		IEnumerable<RuleDefinitionDetails> GetAllRuleDetails(bool loadLocalizableData = false);
	}
}
