using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000026 RID: 38
	public enum RuleMode
	{
		// Token: 0x04000039 RID: 57
		[LocDescription(RulesStrings.IDs.ModeAudit)]
		Audit = 1,
		// Token: 0x0400003A RID: 58
		[LocDescription(RulesStrings.IDs.ModeAuditAndNotify)]
		AuditAndNotify,
		// Token: 0x0400003B RID: 59
		[LocDescription(RulesStrings.IDs.ModeEnforce)]
		Enforce
	}
}
