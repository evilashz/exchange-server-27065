using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BED RID: 3053
	public enum ToUserScope
	{
		// Token: 0x04003A8D RID: 14989
		[LocDescription(RulesTasksStrings.IDs.InternalUser)]
		InOrganization,
		// Token: 0x04003A8E RID: 14990
		[LocDescription(RulesTasksStrings.IDs.ExternalUser)]
		NotInOrganization,
		// Token: 0x04003A8F RID: 14991
		[LocDescription(RulesTasksStrings.IDs.ExternalPartner)]
		ExternalPartner,
		// Token: 0x04003A90 RID: 14992
		[LocDescription(RulesTasksStrings.IDs.ExternalNonPartner)]
		ExternalNonPartner
	}
}
