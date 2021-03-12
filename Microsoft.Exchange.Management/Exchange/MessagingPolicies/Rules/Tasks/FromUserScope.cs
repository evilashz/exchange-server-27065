using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BCF RID: 3023
	public enum FromUserScope
	{
		// Token: 0x04003A4B RID: 14923
		[LocDescription(RulesTasksStrings.IDs.InternalUser)]
		InOrganization,
		// Token: 0x04003A4C RID: 14924
		[LocDescription(RulesTasksStrings.IDs.ExternalUser)]
		NotInOrganization
	}
}
