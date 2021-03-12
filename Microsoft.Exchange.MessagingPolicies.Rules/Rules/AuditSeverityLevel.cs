using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200005E RID: 94
	internal enum AuditSeverityLevel
	{
		// Token: 0x0400020C RID: 524
		[LocDescription(CoreStrings.IDs.AuditSeverityLevelLow)]
		Low = 1,
		// Token: 0x0400020D RID: 525
		[LocDescription(CoreStrings.IDs.AuditSeverityLevelMedium)]
		Medium,
		// Token: 0x0400020E RID: 526
		[LocDescription(CoreStrings.IDs.AuditSeverityLevelHigh)]
		High,
		// Token: 0x0400020F RID: 527
		[LocDescription(CoreStrings.IDs.AuditSeverityLevelDoNotAudit)]
		DoNotAudit
	}
}
