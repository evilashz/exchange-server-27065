using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Directory.TopologyService.Common;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x0200001D RID: 29
	[DebuggerDisplay("Urgent = {urgentWork.Count}, Timed = {timedWorkItems.Count} , Untimed = {untimedWork.Count}")]
	internal class TopologyDiscoveryWorkProvider
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x0000867C File Offset: 0x0000687C
		public TopologyDiscoveryWorkProvider()
		{
			this.untimedWork = new ConcurrentQueue<TopologyDiscoveryWorkProvider.WorkItemCallBackTuple>();
			this.urgentWork = new ConcurrentQueue<TopologyDiscoveryWorkProvider.WorkItemCallBackTuple>();
			this.timedWorkItems = new List<TopologyDiscoveryWorkProvider.TimedWorkItemCallBackTuple>();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000E7 RID: 231 RVA: 0x000086E0 File Offset: 0x000068E0
		// (remove) Token: 0x060000E8 RID: 232 RVA: 0x00008718 File Offset: 0x00006918
		public event Action NewWork = delegate()
		{
		};

		// Token: 0x060000E9 RID: 233 RVA: 0x0000874D File Offset: 0x0000694D
		public void AddUrgentWork(IWorkItem workItem, Action<IWorkItemResult> completionCallback)
		{
			ArgumentValidator.ThrowIfNull("workItem", workItem);
			ArgumentValidator.ThrowIfNull("completionCallback", completionCallback);
			this.urgentWork.Enqueue(new TopologyDiscoveryWorkProvider.WorkItemCallBackTuple(workItem, completionCallback));
			this.NewWork();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00008782 File Offset: 0x00006982
		public void AddWork(IWorkItem workItem, Action<IWorkItemResult> completionCallback)
		{
			ArgumentValidator.ThrowIfNull("workItem", workItem);
			ArgumentValidator.ThrowIfNull("completionCallback", completionCallback);
			this.untimedWork.Enqueue(new TopologyDiscoveryWorkProvider.WorkItemCallBackTuple(workItem, completionCallback));
			this.NewWork();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000087B8 File Offset: 0x000069B8
		public void ScheduleWork(IWorkItem workItem, DateTime executeOn, Action<IWorkItemResult> completionCallback)
		{
			ArgumentValidator.ThrowIfNull("workItem", workItem);
			ArgumentValidator.ThrowIfNull("completionCallback", completionCallback);
			DateTime utcNow = DateTime.UtcNow;
			lock (this.scheduledWorkLock)
			{
				int num = 0;
				while (num < this.timedWorkItems.Count && !(this.timedWorkItems[num].ExecuteOn > executeOn))
				{
					num++;
				}
				if (num == this.timedWorkItems.Count)
				{
					this.timedWorkItems.Add(new TopologyDiscoveryWorkProvider.TimedWorkItemCallBackTuple(workItem, executeOn, completionCallback));
				}
				else
				{
					this.timedWorkItems.Insert(num, new TopologyDiscoveryWorkProvider.TimedWorkItemCallBackTuple(workItem, executeOn, completionCallback));
				}
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00008878 File Offset: 0x00006A78
		public TopologyDiscoveryWorkProvider.WorkItemCallBackTuple NextWork()
		{
			TopologyDiscoveryWorkProvider.WorkItemCallBackTuple workItemCallBackTuple = null;
			while (!this.urgentWork.IsEmpty)
			{
				if (this.urgentWork.TryDequeue(out workItemCallBackTuple) && !workItemCallBackTuple.IsCancelled)
				{
					return workItemCallBackTuple;
				}
			}
			TopologyDiscoveryWorkProvider.TimedWorkItemCallBackTuple timedWorkItemCallBackTuple = null;
			lock (this.scheduledWorkLock)
			{
				if (this.timedWorkItems.Count > 0 && this.timedWorkItems[0].ExecuteOn <= DateTime.UtcNow)
				{
					timedWorkItemCallBackTuple = this.timedWorkItems[0];
					this.timedWorkItems.RemoveAt(0);
				}
			}
			if (timedWorkItemCallBackTuple != null)
			{
				return timedWorkItemCallBackTuple;
			}
			while (!this.untimedWork.IsEmpty)
			{
				if (this.untimedWork.TryDequeue(out workItemCallBackTuple) && !workItemCallBackTuple.IsCancelled)
				{
					return workItemCallBackTuple;
				}
			}
			return null;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008974 File Offset: 0x00006B74
		internal void RemoveAllMatchingWorkItems(string workItemId, QueueType queueType)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("workItemId", workItemId);
			if ((queueType & QueueType.Urgent) != QueueType.None)
			{
				foreach (TopologyDiscoveryWorkProvider.WorkItemCallBackTuple workItemCallBackTuple in this.urgentWork)
				{
					if (workItemCallBackTuple.WorkItem.Id.Equals(workItemId, StringComparison.OrdinalIgnoreCase))
					{
						workItemCallBackTuple.IsCancelled = true;
					}
				}
			}
			if ((queueType & QueueType.UnTimed) != QueueType.None)
			{
				foreach (TopologyDiscoveryWorkProvider.WorkItemCallBackTuple workItemCallBackTuple2 in this.untimedWork)
				{
					if (workItemCallBackTuple2.WorkItem.Id.Equals(workItemId, StringComparison.OrdinalIgnoreCase))
					{
						workItemCallBackTuple2.IsCancelled = true;
					}
				}
			}
			if ((queueType & QueueType.Timed) != QueueType.None)
			{
				lock (this.scheduledWorkLock)
				{
					this.timedWorkItems.RemoveAll((TopologyDiscoveryWorkProvider.TimedWorkItemCallBackTuple x) => x.WorkItem.Id.Equals(workItemId, StringComparison.OrdinalIgnoreCase));
				}
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00008B00 File Offset: 0x00006D00
		[Conditional("DEBUG")]
		private void DbgCheckTimedWorkItemsAreOrdered()
		{
			lock (this.scheduledWorkLock)
			{
				for (int i = 0; i < this.timedWorkItems.Count - 1; i++)
				{
					if (this.timedWorkItems[i].ExecuteOn > this.timedWorkItems[i + 1].ExecuteOn)
					{
						StringBuilder sb = new StringBuilder();
						sb.Append("WorkItems: ");
						this.timedWorkItems.ForEach(delegate(TopologyDiscoveryWorkProvider.TimedWorkItemCallBackTuple x)
						{
							sb.AppendFormat("Id {0} Time {2},", x.WorkItem.Id, x.ExecuteOn.ToString());
						});
					}
				}
			}
		}

		// Token: 0x04000075 RID: 117
		private ConcurrentQueue<TopologyDiscoveryWorkProvider.WorkItemCallBackTuple> untimedWork;

		// Token: 0x04000076 RID: 118
		private ConcurrentQueue<TopologyDiscoveryWorkProvider.WorkItemCallBackTuple> urgentWork;

		// Token: 0x04000077 RID: 119
		private List<TopologyDiscoveryWorkProvider.TimedWorkItemCallBackTuple> timedWorkItems;

		// Token: 0x04000078 RID: 120
		private object scheduledWorkLock = new object();

		// Token: 0x0200001E RID: 30
		internal class WorkItemCallBackTuple
		{
			// Token: 0x060000F0 RID: 240 RVA: 0x00008BB8 File Offset: 0x00006DB8
			public WorkItemCallBackTuple(IWorkItem workItem, Action<IWorkItemResult> completionCallback)
			{
				this.WorkItem = workItem;
				this.CompletionCallback = completionCallback;
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060000F1 RID: 241 RVA: 0x00008BCE File Offset: 0x00006DCE
			// (set) Token: 0x060000F2 RID: 242 RVA: 0x00008BD6 File Offset: 0x00006DD6
			public IWorkItem WorkItem { get; private set; }

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060000F3 RID: 243 RVA: 0x00008BDF File Offset: 0x00006DDF
			// (set) Token: 0x060000F4 RID: 244 RVA: 0x00008BE7 File Offset: 0x00006DE7
			public Action<IWorkItemResult> CompletionCallback { get; private set; }

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060000F5 RID: 245 RVA: 0x00008BF0 File Offset: 0x00006DF0
			// (set) Token: 0x060000F6 RID: 246 RVA: 0x00008BF8 File Offset: 0x00006DF8
			public bool IsCancelled { get; set; }
		}

		// Token: 0x0200001F RID: 31
		internal class TimedWorkItemCallBackTuple : TopologyDiscoveryWorkProvider.WorkItemCallBackTuple
		{
			// Token: 0x060000F7 RID: 247 RVA: 0x00008C01 File Offset: 0x00006E01
			public TimedWorkItemCallBackTuple(IWorkItem workItem, DateTime executeOn, Action<IWorkItemResult> completionCallback) : base(workItem, completionCallback)
			{
				this.ExecuteOn = executeOn;
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x00008C12 File Offset: 0x00006E12
			// (set) Token: 0x060000F9 RID: 249 RVA: 0x00008C1A File Offset: 0x00006E1A
			public DateTime ExecuteOn { get; private set; }
		}
	}
}
