using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200003C RID: 60
	internal class RefCountTable<TKey>
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x000115AD File Offset: 0x0000F7AD
		internal RefCountTable()
		{
			this.refCounters = new Dictionary<TKey, int>();
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x000115C0 File Offset: 0x0000F7C0
		public Dictionary<TKey, int> Counters
		{
			get
			{
				return this.refCounters;
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000115C8 File Offset: 0x0000F7C8
		public int Add(TKey key)
		{
			int result;
			lock (((ICollection)this.refCounters).SyncRoot)
			{
				int num;
				if (this.refCounters.TryGetValue(key, out num))
				{
					num = (this.refCounters[key] = num + 1);
					result = num;
				}
				else
				{
					num = 1;
					this.refCounters.Add(key, num);
					result = num;
				}
			}
			return result;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00011640 File Offset: 0x0000F840
		public void Remove(TKey key)
		{
			lock (((ICollection)this.refCounters).SyncRoot)
			{
				int num;
				if (this.refCounters.TryGetValue(key, out num))
				{
					if (num > 0)
					{
						Dictionary<TKey, int> dictionary;
						(dictionary = this.refCounters)[key] = dictionary[key] - 1;
					}
					else
					{
						this.refCounters.Remove(key);
					}
				}
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000116C0 File Offset: 0x0000F8C0
		public void Remove(TKey key, int number)
		{
			lock (((ICollection)this.refCounters).SyncRoot)
			{
				int num;
				if (this.refCounters.TryGetValue(key, out num))
				{
					if (num > number)
					{
						this.refCounters[key] = num - number;
					}
					else
					{
						this.refCounters.Remove(key);
					}
				}
			}
		}

		// Token: 0x04000206 RID: 518
		private Dictionary<TKey, int> refCounters;
	}
}
