using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000B1 RID: 177
	internal class WorkItemQueue
	{
		// Token: 0x060008E2 RID: 2274 RVA: 0x0003C23B File Offset: 0x0003A43B
		public WorkItemQueue()
		{
			this.workItems = new List<WorkItem>(10);
			this.periodicWorkItems = new List<WorkItem>(1);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0003C25C File Offset: 0x0003A45C
		public void Add(WorkItem workItem)
		{
			if (workItem is IPeriodicWorkItem)
			{
				this.periodicWorkItems.Add(workItem);
				return;
			}
			this.workItems.Add(workItem);
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0003C280 File Offset: 0x0003A480
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x0003C308 File Offset: 0x0003A508
		public ExDateTime ScheduledRunTime
		{
			get
			{
				ExDateTime exDateTime = (this.workItems.Count == 0) ? ExDateTime.MaxValue : this.workItems[0].ScheduledRunTime;
				foreach (WorkItem workItem in this.periodicWorkItems)
				{
					if (workItem.ScheduledRunTime < exDateTime)
					{
						exDateTime = workItem.ScheduledRunTime;
					}
				}
				return exDateTime;
			}
			set
			{
				if (this.workItems.Count > 0)
				{
					this.workItems[0].ScheduledRunTime = value;
					return;
				}
				this.periodicWorkItems[0].ScheduledRunTime = value;
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0003C33D File Offset: 0x0003A53D
		public void Clear()
		{
			this.workItems.Clear();
			this.periodicWorkItems.Clear();
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0003C358 File Offset: 0x0003A558
		public void Remove(WorkItem workItem)
		{
			if (workItem is IPeriodicWorkItem)
			{
				workItem.ScheduledRunTime = ExDateTime.UtcNow.Add(((IPeriodicWorkItem)workItem).PeriodicInterval);
				return;
			}
			this.workItems.Remove(workItem);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0003C399 File Offset: 0x0003A599
		public bool IsEmpty()
		{
			return this.workItems.Count == 0;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0003C570 File Offset: 0x0003A770
		public IEnumerable<WorkItem> GetCandidateWorkItems()
		{
			foreach (WorkItem workItem in this.periodicWorkItems)
			{
				yield return workItem;
			}
			if (!this.IsEmpty())
			{
				yield return this.workItems[0];
			}
			yield break;
		}

		// Token: 0x04000349 RID: 841
		private readonly List<WorkItem> workItems;

		// Token: 0x0400034A RID: 842
		private readonly List<WorkItem> periodicWorkItems;
	}
}
