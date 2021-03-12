using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability
{
	// Token: 0x0200018A RID: 394
	public class CachedList<T, U>
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x00049815 File Offset: 0x00047A15
		public CachedList(CachedList<T, U>.IndividualUpdateMethod<T> updateMethod, TimeSpan expirationTime) : this(expirationTime)
		{
			this.isMassUpdate = false;
			this.individualUpdateMethod = updateMethod;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0004982C File Offset: 0x00047A2C
		public CachedList(CachedList<T, U>.MassUpdateMethod<T> massUpdateMethod, TimeSpan expirationTime) : this(expirationTime)
		{
			this.isMassUpdate = true;
			this.massUpdateMethod = massUpdateMethod;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00049843 File Offset: 0x00047A43
		private CachedList(TimeSpan expirationTime)
		{
			this.expirationTime = expirationTime;
			this.cacheTable = new Dictionary<U, CachedList<T, U>.CachedValueStruct>();
			this.rwLock = new ReaderWriterLockSlim();
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00049868 File Offset: 0x00047A68
		public T GetValue(U index)
		{
			this.rwLock.EnterUpgradeableReadLock();
			T value;
			try
			{
				this.UpdateCacheIfNecessary(new U[]
				{
					index
				});
				value = this.cacheTable[index].Value;
			}
			finally
			{
				this.rwLock.ExitUpgradeableReadLock();
			}
			return value;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000498C8 File Offset: 0x00047AC8
		public KeyValuePair<U, T>[] GetValues(params U[] indexes)
		{
			this.rwLock.EnterUpgradeableReadLock();
			KeyValuePair<U, T>[] result;
			try
			{
				this.UpdateCacheIfNecessary(indexes);
				List<KeyValuePair<U, T>> list = new List<KeyValuePair<U, T>>();
				foreach (U key in indexes)
				{
					list.Add(new KeyValuePair<U, T>(key, this.cacheTable[key].Value));
				}
				result = list.ToArray();
			}
			finally
			{
				this.rwLock.ExitUpgradeableReadLock();
			}
			return result;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00049950 File Offset: 0x00047B50
		private void UpdateCacheIfNecessary(params U[] indexes)
		{
			List<U> list = new List<U>();
			DateTime utcNow = DateTime.UtcNow;
			foreach (U u in indexes)
			{
				if (!this.cacheTable.ContainsKey(u) || utcNow - this.cacheTable[u].LastUpdate > this.expirationTime)
				{
					list.Add(u);
				}
			}
			if (list.Count > 0)
			{
				this.rwLock.EnterWriteLock();
				try
				{
					if (this.isMassUpdate)
					{
						KeyValuePair<U, T>[] array = this.massUpdateMethod(list.ToArray());
						foreach (KeyValuePair<U, T> keyValuePair in array)
						{
							if (this.cacheTable.ContainsKey(keyValuePair.Key))
							{
								this.cacheTable[keyValuePair.Key] = new CachedList<T, U>.CachedValueStruct
								{
									LastUpdate = utcNow,
									Value = keyValuePair.Value
								};
							}
							else
							{
								this.cacheTable.Add(keyValuePair.Key, new CachedList<T, U>.CachedValueStruct
								{
									LastUpdate = utcNow,
									Value = keyValuePair.Value
								});
							}
						}
					}
					else
					{
						foreach (U u2 in list)
						{
							if (this.cacheTable.ContainsKey(u2))
							{
								this.cacheTable[u2] = new CachedList<T, U>.CachedValueStruct
								{
									LastUpdate = utcNow,
									Value = this.individualUpdateMethod(u2)
								};
							}
							else
							{
								this.cacheTable.Add(u2, new CachedList<T, U>.CachedValueStruct
								{
									LastUpdate = utcNow,
									Value = this.individualUpdateMethod(u2)
								});
							}
						}
					}
				}
				finally
				{
					this.rwLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x040008BD RID: 2237
		private readonly TimeSpan expirationTime;

		// Token: 0x040008BE RID: 2238
		private readonly bool isMassUpdate;

		// Token: 0x040008BF RID: 2239
		private Dictionary<U, CachedList<T, U>.CachedValueStruct> cacheTable;

		// Token: 0x040008C0 RID: 2240
		private CachedList<T, U>.IndividualUpdateMethod<T> individualUpdateMethod;

		// Token: 0x040008C1 RID: 2241
		private CachedList<T, U>.MassUpdateMethod<T> massUpdateMethod;

		// Token: 0x040008C2 RID: 2242
		private ReaderWriterLockSlim rwLock;

		// Token: 0x0200018B RID: 395
		// (Invoke) Token: 0x06000B86 RID: 2950
		public delegate V IndividualUpdateMethod<V>(U index);

		// Token: 0x0200018C RID: 396
		// (Invoke) Token: 0x06000B8A RID: 2954
		public delegate KeyValuePair<U, W>[] MassUpdateMethod<W>(params U[] indexes);

		// Token: 0x0200018D RID: 397
		private struct CachedValueStruct
		{
			// Token: 0x040008C3 RID: 2243
			public T Value;

			// Token: 0x040008C4 RID: 2244
			public DateTime LastUpdate;
		}
	}
}
