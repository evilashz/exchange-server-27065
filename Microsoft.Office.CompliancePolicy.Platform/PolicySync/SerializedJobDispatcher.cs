using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000131 RID: 305
	public sealed class SerializedJobDispatcher : JobDispatcherBase
	{
		// Token: 0x060008F0 RID: 2288 RVA: 0x0001DF88 File Offset: 0x0001C188
		public SerializedJobDispatcher(SyncAgentContext syncAgentContext, int maxWorkItemsPerJob) : base(syncAgentContext, maxWorkItemsPerJob)
		{
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001DF94 File Offset: 0x0001C194
		public override void Dispatch(object state)
		{
			ArgumentValidator.ThrowIfNull("WorkItemQueue", base.WorkItemQueue);
			while (!base.HostStateProvider.IsShuttingDown() && !base.WorkItemQueue.IsEmpty())
			{
				IEnumerable<WorkItemBase> enumerable = base.WorkItemQueue.Dequeue(base.MaxWorkItemsPerJob);
				if (enumerable == null)
				{
					Thread.Sleep(base.SyncAgentContext.SyncAgentConfig.JobDispatcherWaitIntervalWhenStarve);
				}
				else
				{
					IEnumerable<WorkItemBase> enumerable2 = JobDispatcherBase.TrimWorkItemsOverBatchSizeLimit(enumerable);
					if (enumerable2.Any<WorkItemBase>())
					{
						base.OnWorkItemsComplete(enumerable2);
					}
					JobBase jobBase = base.JobFactory.CreateJob(enumerable, new Action<JobBase>(this.OnJobCompleted), base.SyncAgentContext);
					jobBase.Begin(null);
				}
			}
			if (!base.HostStateProvider.IsShuttingDown())
			{
				base.WorkItemQueue.OnAllWorkItemDispatched();
			}
		}
	}
}
