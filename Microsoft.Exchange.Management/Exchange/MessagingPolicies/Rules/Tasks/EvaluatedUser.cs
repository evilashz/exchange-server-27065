using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BD8 RID: 3032
	public enum EvaluatedUser
	{
		// Token: 0x04003A59 RID: 14937
		[LocDescription(RulesTasksStrings.IDs.EvaluatedUserSender)]
		Sender,
		// Token: 0x04003A5A RID: 14938
		[LocDescription(RulesTasksStrings.IDs.EvaluatedUserRecipient)]
		Recipient
	}
}
