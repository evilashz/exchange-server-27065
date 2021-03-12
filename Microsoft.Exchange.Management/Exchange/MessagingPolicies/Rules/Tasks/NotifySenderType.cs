using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B94 RID: 2964
	public enum NotifySenderType
	{
		// Token: 0x040039C3 RID: 14787
		[LocDescription(RulesTasksStrings.IDs.NotifyOnlyActionType)]
		NotifyOnly = 1,
		// Token: 0x040039C4 RID: 14788
		[LocDescription(RulesTasksStrings.IDs.RejectMessageActionType)]
		RejectMessage,
		// Token: 0x040039C5 RID: 14789
		[LocDescription(RulesTasksStrings.IDs.RejectUnlessFalsePositiveOverrideActionType)]
		RejectUnlessFalsePositiveOverride,
		// Token: 0x040039C6 RID: 14790
		[LocDescription(RulesTasksStrings.IDs.RejectUnlessSilentOverrideActionType)]
		RejectUnlessSilentOverride,
		// Token: 0x040039C7 RID: 14791
		[LocDescription(RulesTasksStrings.IDs.RejectUnlessExplicitOverrideActionType)]
		RejectUnlessExplicitOverride
	}
}
