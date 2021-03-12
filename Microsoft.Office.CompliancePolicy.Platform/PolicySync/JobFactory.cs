using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000123 RID: 291
	internal sealed class JobFactory : IJobFactory
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x000193DC File Offset: 0x000175DC
		public JobBase CreateJob(IEnumerable<WorkItemBase> workItems, Action<JobBase> onJobCompleted, SyncAgentContext syncAgentContext)
		{
			ArgumentValidator.ThrowIfCollectionNullOrEmpty<WorkItemBase>("workItems", workItems);
			WorkItemBase workItemBase = workItems.First<WorkItemBase>();
			JobBase result;
			if (workItemBase is SyncWorkItem)
			{
				result = new SyncJob(workItems, onJobCompleted, syncAgentContext);
			}
			else
			{
				if (!(workItemBase is SyncStatusUpdateWorkitem))
				{
					throw new NotSupportedException(string.Format("This type of work item isn't supported: ", workItemBase.GetType()));
				}
				result = new StatusUpdatePublishJob(workItems.Cast<SyncStatusUpdateWorkitem>(), onJobCompleted, syncAgentContext);
			}
			return result;
		}
	}
}
