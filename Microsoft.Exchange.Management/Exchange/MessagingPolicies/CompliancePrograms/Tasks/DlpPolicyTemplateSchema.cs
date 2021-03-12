using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x0200095B RID: 2395
	internal class DlpPolicyTemplateSchema : DlpPolicySchemaBase
	{
		// Token: 0x040031B9 RID: 12729
		public static readonly ADPropertyDefinition LocalizedName = new ADPropertyDefinition("LocalizedName", ExchangeObjectVersion.Exchange2012, typeof(string), "localizedName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040031BA RID: 12730
		public static readonly ADPropertyDefinition RuleParameters = new ADPropertyDefinition("RuleParameters", ExchangeObjectVersion.Exchange2012, typeof(string), "ruleParameters", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
