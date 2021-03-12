using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BA8 RID: 2984
	public enum ADAttributeEvaluationType
	{
		// Token: 0x04003A14 RID: 14868
		[LocDescription(RulesTasksStrings.IDs.ADAttributeEvaluationTypeEquals)]
		Equals,
		// Token: 0x04003A15 RID: 14869
		[LocDescription(RulesTasksStrings.IDs.ADAttributeEvaluationTypeContains)]
		Contains
	}
}
