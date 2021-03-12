using System;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000B2 RID: 178
	public enum RuleMode
	{
		// Token: 0x040002D7 RID: 727
		Disabled,
		// Token: 0x040002D8 RID: 728
		Audit,
		// Token: 0x040002D9 RID: 729
		AuditAndNotify,
		// Token: 0x040002DA RID: 730
		Enforce,
		// Token: 0x040002DB RID: 731
		PendingDeletion
	}
}
