using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000699 RID: 1689
	internal sealed class SimpleTimeoutCache<T> : IDisposable
	{
		// Token: 0x06001EDD RID: 7901 RVA: 0x00039F44 File Offset: 0x00038144
		public SimpleTimeoutCache(TimeSpan expiration, TimeSpan purgeFrequency)
		{
			this.expiration = expiration;
			this.items = new Dictionary<T, DateTime>();
			this.timer = new Timer(new TimerCallback(this.Expire), null, purgeFrequency, purgeFrequency);
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x00039F78 File Offset: 0x00038178
		public void Dispose()
		{
			this.timer.Dispose();
		}

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x06001EDF RID: 7903 RVA: 0x00039F88 File Offset: 0x00038188
		// (remove) Token: 0x06001EE0 RID: 7904 RVA: 0x00039FC0 File Offset: 0x000381C0
		public event Action<int> CountChanged;

		// Token: 0x06001EE1 RID: 7905 RVA: 0x00039FF8 File Offset: 0x000381F8
		public void Add(T key)
		{
			int count;
			int count2;
			lock (this.items)
			{
				count = this.items.Count;
				this.items[key] = DateTime.UtcNow + this.expiration;
				count2 = this.items.Count;
			}
			this.RaiseCountChanged(count, count2);
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0003A070 File Offset: 0x00038270
		public bool Contains(T key)
		{
			bool result;
			lock (this.items)
			{
				DateTime t;
				if (this.items.TryGetValue(key, out t))
				{
					result = (t > DateTime.UtcNow);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x0003A0CC File Offset: 0x000382CC
		private void Expire(object notUsed)
		{
			int count;
			int count2;
			lock (this.items)
			{
				count = this.items.Count;
				List<T> list = new List<T>(10);
				foreach (KeyValuePair<T, DateTime> keyValuePair in this.items)
				{
					if (keyValuePair.Value < DateTime.UtcNow)
					{
						list.Add(keyValuePair.Key);
					}
				}
				foreach (T key in list)
				{
					this.items.Remove(key);
				}
				count2 = this.items.Count;
			}
			this.RaiseCountChanged(count, count2);
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0003A1D4 File Offset: 0x000383D4
		private void RaiseCountChanged(int oldCount, int newCount)
		{
			if (newCount != oldCount && this.CountChanged != null)
			{
				this.CountChanged(newCount);
			}
		}

		// Token: 0x04001E79 RID: 7801
		private readonly TimeSpan expiration;

		// Token: 0x04001E7A RID: 7802
		private readonly Dictionary<T, DateTime> items;

		// Token: 0x04001E7B RID: 7803
		private readonly Timer timer;
	}
}
