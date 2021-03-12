using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B84 RID: 2948
	public enum DisclaimerLocation
	{
		// Token: 0x040039A9 RID: 14761
		[LocDescription(RulesTasksStrings.IDs.AppendDisclaimer)]
		Append,
		// Token: 0x040039AA RID: 14762
		[LocDescription(RulesTasksStrings.IDs.PrependDisclaimer)]
		Prepend
	}
}
