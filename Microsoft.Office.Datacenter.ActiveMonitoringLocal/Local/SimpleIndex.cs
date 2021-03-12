using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Local
{
	// Token: 0x0200007C RID: 124
	internal class SimpleIndex<TItem, TKey> : IIndex<TItem>
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x0001C808 File Offset: 0x0001AA08
		public SimpleIndex(Func<TItem, IEnumerable<TKey>> keySelector)
		{
			int maxRunningTasks = Settings.MaxRunningTasks;
			this.dictionary = new ConcurrentDictionary<TKey, ConcurrentQueue<TItem>>(maxRunningTasks, 1024, SimpleIndex<TItem, TKey>.keyComparer);
			this.keySelector = keySelector;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001C848 File Offset: 0x0001AA48
		public void Add(TItem item, TracingContext traceContext)
		{
			foreach (TKey tkey in this.keySelector(item))
			{
				if (tkey != null)
				{
					ConcurrentQueue<TItem> orAdd = this.dictionary.GetOrAdd(tkey, (TKey k) => new ConcurrentQueue<TItem>());
					orAdd.Enqueue(item);
				}
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001C8D0 File Offset: 0x0001AAD0
		public IEnumerable<TItem> GetItems(TKey key)
		{
			ConcurrentQueue<TItem> result;
			if (!this.dictionary.TryGetValue(key, out result))
			{
				return Enumerable.Empty<TItem>();
			}
			return result;
		}

		// Token: 0x04000450 RID: 1104
		private const int DictionarySize = 1024;

		// Token: 0x04000451 RID: 1105
		private static KeyComparer<TKey> keyComparer = new KeyComparer<TKey>();

		// Token: 0x04000452 RID: 1106
		private ConcurrentDictionary<TKey, ConcurrentQueue<TItem>> dictionary;

		// Token: 0x04000453 RID: 1107
		private Func<TItem, IEnumerable<TKey>> keySelector;
	}
}
