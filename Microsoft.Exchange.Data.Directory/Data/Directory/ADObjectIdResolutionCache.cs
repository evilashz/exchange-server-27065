using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000053 RID: 83
	internal class ADObjectIdResolutionCache
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00017B0C File Offset: 0x00015D0C
		internal static ADObjectIdResolutionCache Default
		{
			get
			{
				return ADObjectIdResolutionCache.defaultInstance;
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00017B13 File Offset: 0x00015D13
		internal ADObjectIdResolutionCache(Func<ADObjectId, ADObjectId> resolutionFunc, int capacityLimit)
		{
			this.cache = new ConcurrentDictionary<Guid, ExpiringADObjectIdValue>();
			this.resolutionFunc = resolutionFunc;
			this.capacityLimit = capacityLimit;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00017B34 File Offset: 0x00015D34
		internal int Count
		{
			get
			{
				return this.cache.Count;
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00017B44 File Offset: 0x00015D44
		internal ADObjectId GetEntry(ADObjectId obj)
		{
			ExpiringADObjectIdValue expiringADObjectIdValue;
			if (this.cache.TryGetValue(obj.ObjectGuid, out expiringADObjectIdValue) && !expiringADObjectIdValue.Expired)
			{
				return expiringADObjectIdValue.Value;
			}
			ADObjectId adobjectId = this.resolutionFunc(obj);
			this.UpdateEntry(adobjectId);
			return adobjectId;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00017B9C File Offset: 0x00015D9C
		internal bool UpdateEntry(ADObjectId obj)
		{
			if (string.IsNullOrEmpty(obj.DistinguishedName))
			{
				return false;
			}
			ExpiringADObjectIdValue expiringValue = new ExpiringADObjectIdValue(obj);
			this.cache.AddOrUpdate(obj.ObjectGuid, expiringValue, (Guid key, ExpiringADObjectIdValue oldValue) => expiringValue);
			this.EvictIfNecessary();
			return true;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00017BF8 File Offset: 0x00015DF8
		private void EvictIfNecessary()
		{
			if (this.cache.Count <= this.capacityLimit)
			{
				return;
			}
			int num = 2;
			try
			{
				num = Interlocked.CompareExchange(ref this.evictionOwnershipFlag, 1, 0);
				if (num == 0)
				{
					List<Guid> list = this.cache.Keys.ToList<Guid>();
					foreach (Guid key in list)
					{
						ExpiringADObjectIdValue expiringADObjectIdValue;
						if (this.cache.TryGetValue(key, out expiringADObjectIdValue) && expiringADObjectIdValue.Expired)
						{
							this.cache.TryRemove(key, out expiringADObjectIdValue);
						}
					}
					if (this.cache.Count > this.capacityLimit)
					{
						list = this.cache.Keys.ToList<Guid>();
						int num2 = (int)((double)this.capacityLimit * 0.8);
						Random random = new Random();
						foreach (Guid key2 in list)
						{
							if (random.Next(list.Count<Guid>()) > num2)
							{
								ExpiringADObjectIdValue expiringADObjectIdValue2;
								this.cache.TryRemove(key2, out expiringADObjectIdValue2);
							}
						}
					}
				}
			}
			finally
			{
				if (num == 0)
				{
					Interlocked.Exchange(ref this.evictionOwnershipFlag, 0);
				}
			}
		}

		// Token: 0x04000172 RID: 370
		private const int CapacityDefault = 3000;

		// Token: 0x04000173 RID: 371
		private static readonly ADObjectIdResolutionCache defaultInstance = new ADObjectIdResolutionCache(new Func<ADObjectId, ADObjectId>(ADObjectIdResolutionHelper.ResolveADObjectIdWithoutCache), 3000);

		// Token: 0x04000174 RID: 372
		private readonly int capacityLimit;

		// Token: 0x04000175 RID: 373
		private ConcurrentDictionary<Guid, ExpiringADObjectIdValue> cache;

		// Token: 0x04000176 RID: 374
		private int evictionOwnershipFlag;

		// Token: 0x04000177 RID: 375
		private readonly Func<ADObjectId, ADObjectId> resolutionFunc;
	}
}
