using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BA4 RID: 2980
	public enum Evaluation
	{
		// Token: 0x04003A0C RID: 14860
		[LocDescription(RulesTasksStrings.IDs.EvaluationEqual)]
		Equal,
		// Token: 0x04003A0D RID: 14861
		[LocDescription(RulesTasksStrings.IDs.EvaluationNotEqual)]
		NotEqual
	}
}
