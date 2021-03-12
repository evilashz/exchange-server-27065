using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Local
{
	// Token: 0x0200007A RID: 122
	internal class ResultsTable<TWorkItemResult> : SimpleTable<TWorkItemResult, int, ResultQueue<TWorkItemResult>> where TWorkItemResult : WorkItemResult
	{
		// Token: 0x060006C8 RID: 1736 RVA: 0x0001C6C4 File Offset: 0x0001A8C4
		public ResultsTable(int resultHistorySize, bool requiresNameIndex) : base(WorkItemResultIndex<TWorkItemResult>.WorkItemIdAndExecutionEndTime(0, DateTime.MinValue))
		{
			this.resultHistorySize = resultHistorySize;
			if (requiresNameIndex)
			{
				IIndexDescriptor<TWorkItemResult, string> indexDescriptor = WorkItemResultIndex<TWorkItemResult>.ResultNameAndExecutionEndTime(string.Empty, DateTime.MinValue);
				base.AddIndex<string>(indexDescriptor);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001C703 File Offset: 0x0001A903
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x0001C70B File Offset: 0x0001A90B
		public Action<TWorkItemResult> OnInsertNotificationDelegate { get; set; }

		// Token: 0x060006CB RID: 1739 RVA: 0x0001C714 File Offset: 0x0001A914
		protected override ResultQueue<TWorkItemResult> CreateSegment(TWorkItemResult item)
		{
			return new ResultQueue<TWorkItemResult>(this.resultHistorySize);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001C724 File Offset: 0x0001A924
		protected override bool AddToSegment(ResultQueue<TWorkItemResult> segment, TWorkItemResult item)
		{
			TWorkItemResult newest = segment.GetNewest();
			if (newest != null && newest.Exception == item.Exception)
			{
				item.Exception = newest.Exception;
			}
			bool result = segment.Add(item);
			if (this.OnInsertNotificationDelegate != null)
			{
				this.OnInsertNotificationDelegate(item);
			}
			return result;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001C798 File Offset: 0x0001A998
		protected override IEnumerable<TWorkItemResult> GetItemsFromSegment<TKey>(ResultQueue<TWorkItemResult> segment, IIndexDescriptor<TWorkItemResult, TKey> indexDescriptor)
		{
			DateTime minExecutionEndTime = ((WorkItemResultIndex<TWorkItemResult>.WorkItemResultIndexBase<TWorkItemResult, TKey>)indexDescriptor).MinExecutionEndTime;
			return segment.GetItems(minExecutionEndTime);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001C7D0 File Offset: 0x0001A9D0
		protected override IEnumerable<TWorkItemResult> GetItemsFromSegments<TKey>(IEnumerable<ResultQueue<TWorkItemResult>> segments, IIndexDescriptor<TWorkItemResult, TKey> indexDescriptor)
		{
			DateTime minExecutionEndTime = ((WorkItemResultIndex<TWorkItemResult>.WorkItemResultIndexBase<TWorkItemResult, TKey>)indexDescriptor).MinExecutionEndTime;
			return segments.SelectMany((ResultQueue<TWorkItemResult> segment) => segment.GetItems(minExecutionEndTime));
		}

		// Token: 0x0400044E RID: 1102
		private readonly int resultHistorySize;
	}
}
