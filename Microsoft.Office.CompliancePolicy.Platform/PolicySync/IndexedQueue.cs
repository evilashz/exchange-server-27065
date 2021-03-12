using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000132 RID: 306
	internal sealed class IndexedQueue
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x0001E070 File Offset: 0x0001C270
		public bool TryGetValue(Guid key, out WorkItemBase value)
		{
			value = null;
			DateTime key2;
			return this.index.TryGetValue(key, out key2) && this.sortedList.TryGetValue(key2, out value);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001E0A0 File Offset: 0x0001C2A0
		public void Enqueue(WorkItemBase item)
		{
			ArgumentValidator.ThrowIfNull("item", item);
			Guid primaryKey = item.GetPrimaryKey();
			DateTime dateTime = this.AdjustExecuteTimeIfNecessary(item.ExecuteTimeUTC, item.ProcessNow);
			item.ExecuteTimeUTC = dateTime;
			if (!this.index.ContainsKey(primaryKey))
			{
				this.sortedList.Add(dateTime, item);
				this.index.Add(primaryKey, dateTime);
				return;
			}
			if (this.index[primaryKey] != dateTime)
			{
				this.sortedList.Remove(this.index[primaryKey]);
				this.sortedList.Add(dateTime, item);
				this.index[primaryKey] = dateTime;
				return;
			}
			this.sortedList[dateTime] = item;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001E158 File Offset: 0x0001C358
		public IList<WorkItemBase> Dequeue(int maxCount)
		{
			ArgumentValidator.ThrowIfZeroOrNegative("maxCount", maxCount);
			List<WorkItemBase> list = new List<WorkItemBase>();
			DateTime utcNow = DateTime.UtcNow;
			while (list.Count < maxCount && !this.IsEmpty() && this.sortedList.Keys[0] <= utcNow)
			{
				WorkItemBase workItemBase = this.sortedList.Values[0];
				this.index.Remove(workItemBase.GetPrimaryKey());
				this.sortedList.RemoveAt(0);
				list.Add(workItemBase);
			}
			if (!list.Any<WorkItemBase>())
			{
				return null;
			}
			return list;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001E1EA File Offset: 0x0001C3EA
		public bool IsEmpty()
		{
			return !this.index.Any<KeyValuePair<Guid, DateTime>>();
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001E1FA File Offset: 0x0001C3FA
		public int Count()
		{
			return this.index.Count;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001E208 File Offset: 0x0001C408
		private DateTime AdjustExecuteTimeIfNecessary(DateTime originalTime, bool insertToHead)
		{
			DateTime dateTime = originalTime;
			if (this.sortedList.Any<KeyValuePair<DateTime, WorkItemBase>>())
			{
				if (insertToHead)
				{
					if (dateTime >= this.sortedList.Keys[0])
					{
						dateTime = this.sortedList.Keys[0] - new TimeSpan(0, 0, 1);
					}
				}
				else
				{
					while (this.sortedList.ContainsKey(dateTime))
					{
						dateTime += new TimeSpan(1L);
					}
				}
			}
			return dateTime;
		}

		// Token: 0x040004AC RID: 1196
		private readonly Dictionary<Guid, DateTime> index = new Dictionary<Guid, DateTime>();

		// Token: 0x040004AD RID: 1197
		private readonly SortedList<DateTime, WorkItemBase> sortedList = new SortedList<DateTime, WorkItemBase>();
	}
}
