using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200012F RID: 303
	public abstract class JobDispatcherBase
	{
		// Token: 0x060008DB RID: 2267 RVA: 0x0001D9D4 File Offset: 0x0001BBD4
		public JobDispatcherBase(SyncAgentContext syncAgentContext, int maxWorkItemsPerJob)
		{
			ArgumentValidator.ThrowIfNull("syncAgentContext", syncAgentContext);
			ArgumentValidator.ThrowIfZeroOrNegative("maxWorkItemsPerJob", maxWorkItemsPerJob);
			this.HostStateProvider = syncAgentContext.HostStateProvider;
			this.JobFactory = syncAgentContext.JobFactory;
			this.LogProvider = syncAgentContext.LogProvider;
			this.SyncAgentContext = syncAgentContext;
			this.MaxWorkItemsPerJob = maxWorkItemsPerJob;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0001DA2F File Offset: 0x0001BC2F
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x0001DA37 File Offset: 0x0001BC37
		public IWorkItemQueueProvider WorkItemQueue
		{
			protected get
			{
				return this.workItemQueue;
			}
			set
			{
				ArgumentValidator.ThrowIfNull("WorkItemQueue", value);
				this.workItemQueue = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x0001DA4B File Offset: 0x0001BC4B
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x0001DA53 File Offset: 0x0001BC53
		internal IJobFactory JobFactory { get; private set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0001DA5C File Offset: 0x0001BC5C
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x0001DA64 File Offset: 0x0001BC64
		private protected HostStateProvider HostStateProvider { protected get; private set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0001DA6D File Offset: 0x0001BC6D
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x0001DA75 File Offset: 0x0001BC75
		private protected ExecutionLog LogProvider { protected get; private set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0001DA7E File Offset: 0x0001BC7E
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x0001DA86 File Offset: 0x0001BC86
		private protected SyncAgentContext SyncAgentContext { protected get; private set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0001DA8F File Offset: 0x0001BC8F
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x0001DA97 File Offset: 0x0001BC97
		private protected int MaxWorkItemsPerJob { protected get; private set; }

		// Token: 0x060008E8 RID: 2280
		public abstract void Dispatch(object state);

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001DAA0 File Offset: 0x0001BCA0
		internal static IEnumerable<WorkItemBase> TrimWorkItemsOverBatchSizeLimit(IEnumerable<WorkItemBase> workItems)
		{
			List<WorkItemBase> list = new List<WorkItemBase>();
			foreach (WorkItemBase workItemBase in workItems)
			{
				WorkItemBase workItemBase2 = workItemBase.Split();
				if (workItemBase2 != null)
				{
					list.Add(workItemBase2);
				}
			}
			return list;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001DAFC File Offset: 0x0001BCFC
		internal virtual void OnJobCompleted(JobBase job)
		{
			this.OnWorkItemsComplete(job.End());
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001DB0C File Offset: 0x0001BD0C
		protected void OnWorkItemsComplete(IEnumerable<WorkItemBase> workItems)
		{
			foreach (WorkItemBase item in workItems)
			{
				if (this.HostStateProvider.IsShuttingDown())
				{
					break;
				}
				this.workItemQueue.OnWorkItemCompleted(item);
			}
		}

		// Token: 0x040004A2 RID: 1186
		private IWorkItemQueueProvider workItemQueue;
	}
}
