using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000122 RID: 290
	internal interface IJobFactory
	{
		// Token: 0x06000843 RID: 2115
		JobBase CreateJob(IEnumerable<WorkItemBase> workItems, Action<JobBase> onJobCompleted, SyncAgentContext syncAgentContext);
	}
}
