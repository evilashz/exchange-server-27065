using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000D8 RID: 216
	internal sealed class PsHoldRuleSchema : PsComplianceRuleBaseSchema
	{
		// Token: 0x040003B6 RID: 950
		public static readonly ADPropertyDefinition ContentDateFrom = RuleStorageSchema.ContentDateFrom;

		// Token: 0x040003B7 RID: 951
		public static readonly ADPropertyDefinition ContentDateTo = RuleStorageSchema.ContentDateTo;

		// Token: 0x040003B8 RID: 952
		public static readonly ADPropertyDefinition HoldContent = RuleStorageSchema.HoldContent;

		// Token: 0x040003B9 RID: 953
		public static readonly ADPropertyDefinition HoldDurationDisplayHint = RuleStorageSchema.HoldDurationDisplayHint;
	}
}
