using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B85 RID: 2949
	public enum DisclaimerFallbackAction
	{
		// Token: 0x040039AC RID: 14764
		[LocDescription(RulesTasksStrings.IDs.FallbackWrap)]
		Wrap,
		// Token: 0x040039AD RID: 14765
		[LocDescription(RulesTasksStrings.IDs.FallbackIgnore)]
		Ignore,
		// Token: 0x040039AE RID: 14766
		[LocDescription(RulesTasksStrings.IDs.FallbackReject)]
		Reject
	}
}
