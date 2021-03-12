using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000DB RID: 219
	internal sealed class PsDlpComplianceRuleSchema : PsComplianceRuleBaseSchema
	{
		// Token: 0x040003BD RID: 957
		public static readonly ADPropertyDefinition ContentPropertyContainsWords = RuleStorageSchema.ContentPropertyContainsWords;

		// Token: 0x040003BE RID: 958
		public static readonly ADPropertyDefinition ContentContainsSensitiveInformation = RuleStorageSchema.ContentContainsSensitiveInformation;

		// Token: 0x040003BF RID: 959
		public static readonly ADPropertyDefinition AccessScopeIs = RuleStorageSchema.AccessScopeIs;

		// Token: 0x040003C0 RID: 960
		public static readonly ADPropertyDefinition BlockAccess = RuleStorageSchema.BlockAccess;
	}
}
