using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Local
{
	// Token: 0x0200007E RID: 126
	internal class ResultQueue<TWorkItemResult> where TWorkItemResult : WorkItemResult
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x0001C999 File Offset: 0x0001AB99
		internal ResultQueue(int size)
		{
			this.data = new ResultQueue<TWorkItemResult>.Cell[size];
			this.index = -1;
			this.lastUsedId = 0;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001C9BB File Offset: 0x0001ABBB
		internal bool IsEmpty
		{
			get
			{
				return this.lastUsedId == 0;
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001C9C8 File Offset: 0x0001ABC8
		internal bool Add(TWorkItemResult item)
		{
			if (item == null)
			{
				throw new ArgumentException("item");
			}
			this.GetNewest();
			bool flag = false;
			this.spinLock.Enter(ref flag);
			bool isEmpty = this.IsEmpty;
			int num = this.index + 1;
			if (num == this.data.Length)
			{
				num = 0;
			}
			this.lastUsedId++;
			this.data[num] = default(ResultQueue<TWorkItemResult>.Cell);
			this.data[num].Item = item;
			this.data[num].Id = (long)this.lastUsedId;
			this.index = num;
			this.spinLock.Exit();
			return isEmpty;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001CA78 File Offset: 0x0001AC78
		internal TWorkItemResult GetNewest()
		{
			if (this.IsEmpty)
			{
				return default(TWorkItemResult);
			}
			return this.data[this.index].Item;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001CCB8 File Offset: 0x0001AEB8
		internal IEnumerable<TWorkItemResult> GetItems(DateTime startTime)
		{
			int currentIndex = this.index;
			if (!this.IsEmpty)
			{
				long previousId = long.MaxValue;
				TWorkItemResult currentItem = this.data[currentIndex].Item;
				long currentId = this.data[currentIndex].Id;
				while (currentId < previousId && currentId > 0L)
				{
					if (currentItem.ExecutionEndTime >= startTime)
					{
						yield return currentItem;
					}
					previousId = currentId;
					currentIndex--;
					if (currentIndex < 0)
					{
						currentIndex = this.data.Length - 1;
					}
					currentItem = this.data[currentIndex].Item;
					currentId = this.data[currentIndex].Id;
				}
			}
			yield break;
		}

		// Token: 0x04000455 RID: 1109
		private ResultQueue<TWorkItemResult>.Cell[] data;

		// Token: 0x04000456 RID: 1110
		private int index;

		// Token: 0x04000457 RID: 1111
		private int lastUsedId;

		// Token: 0x04000458 RID: 1112
		private SpinLock spinLock;

		// Token: 0x0200007F RID: 127
		private struct Cell
		{
			// Token: 0x04000459 RID: 1113
			public long Id;

			// Token: 0x0400045A RID: 1114
			public TWorkItemResult Item;
		}
	}
}
