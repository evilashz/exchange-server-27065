using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000E9 RID: 233
	public enum Importance
	{
		// Token: 0x04000349 RID: 841
		[LocDescription(RulesTasksStrings.IDs.ImportanceLow)]
		Low,
		// Token: 0x0400034A RID: 842
		[LocDescription(RulesTasksStrings.IDs.ImportanceNormal)]
		Normal,
		// Token: 0x0400034B RID: 843
		[LocDescription(RulesTasksStrings.IDs.ImportanceHigh)]
		High
	}
}
