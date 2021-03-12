using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000130 RID: 304
	public sealed class ParallelJobDispatcher : JobDispatcherBase
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x0001DB6C File Offset: 0x0001BD6C
		public ParallelJobDispatcher(SyncAgentContext syncAgentContext, int maxWorkItemsPerJob) : base(syncAgentContext, maxWorkItemsPerJob)
		{
			this.dispatchTimer = new Timer(new TimerCallback(this.Dispatch), null, TimeSpan.FromMilliseconds(-1.0), TimeSpan.FromMilliseconds(-1.0));
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001DBCC File Offset: 0x0001BDCC
		public override void Dispatch(object state)
		{
			ArgumentValidator.ThrowIfNull("WorkItemQueue", base.WorkItemQueue);
			base.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", null, ExecutionLog.EventType.Information, "Parallel dispatcher is triggered. " + Utility.GetThreadPoolStatus(), null);
			int num = base.SyncAgentContext.SyncAgentConfig.ParallelJobDispatcherMaxPendingJobNumber - this.currentPendingJobNumber;
			int num2 = 0;
			while (num2 < num && !base.HostStateProvider.IsShuttingDown())
			{
				IList<WorkItemBase> list = base.WorkItemQueue.Dequeue(base.MaxWorkItemsPerJob);
				if (list == null)
				{
					break;
				}
				List<WorkItemBase> list2 = this.RemovePendingWorkItems(list);
				list2.AddRange(JobDispatcherBase.TrimWorkItemsOverBatchSizeLimit(list));
				if (list2.Any<WorkItemBase>())
				{
					base.OnWorkItemsComplete(list2);
				}
				if (list.Any<WorkItemBase>())
				{
					JobBase jobBase = base.JobFactory.CreateJob(list, new Action<JobBase>(this.OnJobCompleted), base.SyncAgentContext);
					lock (this.syncObject)
					{
						foreach (WorkItemBase workItemBase in list)
						{
							this.pendingWorkItemList.Add(workItemBase.GetPrimaryKey());
						}
						this.currentPendingJobNumber++;
					}
					if (!ThreadPool.QueueUserWorkItem(new WaitCallback(jobBase.Begin)))
					{
						base.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", null, ExecutionLog.EventType.Warning, ".net thread pool fails to spawn a thread to begin a job", null);
						this.OnJobCompleted(jobBase);
						break;
					}
					WorkItemBase workItemBase2 = list.First<WorkItemBase>();
					base.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", workItemBase2.TenantContext.TenantId.ToString(), workItemBase2.ExternalIdentity, ExecutionLog.EventType.Information, string.Empty, string.Format("Notification {0} has been scheduled with .net thread pool.", workItemBase2.ExternalIdentity), null, new KeyValuePair<string, object>[0]);
				}
				num2++;
			}
			if (!base.HostStateProvider.IsShuttingDown())
			{
				if (base.WorkItemQueue.IsEmpty())
				{
					base.WorkItemQueue.OnAllWorkItemDispatched();
					return;
				}
				this.dispatchTimer.Change(base.SyncAgentContext.SyncAgentConfig.JobDispatcherWaitIntervalWhenStarve, TimeSpan.FromMilliseconds(-1.0));
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001DE14 File Offset: 0x0001C014
		internal override void OnJobCompleted(JobBase job)
		{
			lock (this.syncObject)
			{
				IEnumerable<WorkItemBase> enumerable = job.End();
				foreach (WorkItemBase workItemBase in enumerable)
				{
					this.pendingWorkItemList.Remove(workItemBase.GetPrimaryKey());
				}
				this.currentPendingJobNumber--;
			}
			base.OnJobCompleted(job);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001DEB4 File Offset: 0x0001C0B4
		private List<WorkItemBase> RemovePendingWorkItems(IList<WorkItemBase> newWorkItems)
		{
			List<WorkItemBase> list = new List<WorkItemBase>();
			lock (this.syncObject)
			{
				foreach (WorkItemBase workItemBase in newWorkItems)
				{
					if (this.pendingWorkItemList.Contains(workItemBase.GetPrimaryKey()))
					{
						list.Add(workItemBase);
					}
				}
				foreach (WorkItemBase item in list)
				{
					newWorkItems.Remove(item);
				}
			}
			return list;
		}

		// Token: 0x040004A8 RID: 1192
		private readonly List<Guid> pendingWorkItemList = new List<Guid>();

		// Token: 0x040004A9 RID: 1193
		private readonly object syncObject = new object();

		// Token: 0x040004AA RID: 1194
		private int currentPendingJobNumber;

		// Token: 0x040004AB RID: 1195
		private Timer dispatchTimer;
	}
}
