using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000044 RID: 68
	internal sealed class MruDictionaryListCache<TKey, TValue> : IDisposable
	{
		// Token: 0x060001CB RID: 459 RVA: 0x00008FBC File Offset: 0x000071BC
		public MruDictionaryListCache(int capacity, int expireTimeInMinutes, Func<TValue, TKey> getKeyFromValue)
		{
			this.getKeyFromValue = getKeyFromValue;
			this.lookup = new Dictionary<TKey, IList<Guid>>();
			this.cache = new MruDictionaryCache<Guid, TValue>(capacity, capacity, expireTimeInMinutes, new Action<Guid, TValue>(this.OnEntryExpired));
			this.cache.OnExpirationStart = delegate()
			{
				Monitor.Enter(this.lockObject);
			};
			this.cache.OnExpirationComplete = delegate()
			{
				Monitor.Exit(this.lockObject);
			};
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009044 File Offset: 0x00007244
		public bool TryGetAndRemoveValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			lock (this.lockObject)
			{
				IList<Guid> list;
				if (this.lookup.TryGetValue(key, out list))
				{
					Guid token = list[0];
					list.RemoveAt(0);
					if (list.Count == 0)
					{
						this.lookup.Remove(key);
					}
					return this.cache.TryGetAndRemoveValue(token, out value);
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000090E4 File Offset: 0x000072E4
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue data = default(TValue);
			try
			{
				lock (this.lockObject)
				{
					Guid guid = Guid.NewGuid();
					IList<Guid> list;
					if (!this.lookup.TryGetValue(key, out list))
					{
						list = new List<Guid>();
						this.lookup.Add(key, list);
					}
					list.Insert(0, guid);
					data = this.cache.AddAndReturnOriginalData(guid, value);
				}
			}
			finally
			{
				MruDictionaryCache<Guid, TValue>.DisposeData(data);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009190 File Offset: 0x00007390
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000091A0 File Offset: 0x000073A0
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					lock (this.lockObject)
					{
						this.cache.Dispose();
						this.cache = null;
						this.lookup.Clear();
						this.lookup = null;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009210 File Offset: 0x00007410
		private void OnEntryExpired(Guid cacheId, TValue value)
		{
			TKey tkey = this.getKeyFromValue(value);
			IList<Guid> list;
			if (tkey != null && this.lookup.TryGetValue(tkey, out list))
			{
				list.Remove(cacheId);
				if (list.Count == 0)
				{
					this.lookup.Remove(tkey);
				}
			}
		}

		// Token: 0x04000136 RID: 310
		private MruDictionaryCache<Guid, TValue> cache;

		// Token: 0x04000137 RID: 311
		private Dictionary<TKey, IList<Guid>> lookup;

		// Token: 0x04000138 RID: 312
		private Func<TValue, TKey> getKeyFromValue;

		// Token: 0x04000139 RID: 313
		private bool disposed;

		// Token: 0x0400013A RID: 314
		private object lockObject = new object();
	}
}
