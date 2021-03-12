using System;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000B4 RID: 180
	[Flags]
	public enum RuleOverrideOptions
	{
		// Token: 0x040002E0 RID: 736
		None = 0,
		// Token: 0x040002E1 RID: 737
		AllowFalsePositiveOverride = 1,
		// Token: 0x040002E2 RID: 738
		AllowOverrideWithoutJustification = 2,
		// Token: 0x040002E3 RID: 739
		AllowOverrideWithJustification = 4
	}
}
