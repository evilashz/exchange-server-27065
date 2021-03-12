using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BD6 RID: 3030
	public enum ManagementRelationship
	{
		// Token: 0x04003A55 RID: 14933
		[LocDescription(RulesTasksStrings.IDs.ManagementRelationshipManager)]
		Manager,
		// Token: 0x04003A56 RID: 14934
		[LocDescription(RulesTasksStrings.IDs.ManagementRelationshipDirectReport)]
		DirectReport
	}
}
