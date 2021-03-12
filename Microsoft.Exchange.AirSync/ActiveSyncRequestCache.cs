using System;
using Microsoft.Exchange.Collections.TimeoutCache;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000071 RID: 113
	internal sealed class ActiveSyncRequestCache : LazyLookupExactTimeoutCache<Guid, ActiveSyncRequestData>
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00024D28 File Offset: 0x00022F28
		public static ActiveSyncRequestCache Instance
		{
			get
			{
				if (ActiveSyncRequestCache.instance == null)
				{
					lock (ActiveSyncRequestCache.lockObject)
					{
						if (ActiveSyncRequestCache.instance == null)
						{
							ActiveSyncRequestCache.instance = new ActiveSyncRequestCache();
						}
					}
				}
				return ActiveSyncRequestCache.instance;
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00024D80 File Offset: 0x00022F80
		private ActiveSyncRequestCache() : base(ActiveSyncRequestCache.MaxCacheCount, false, ActiveSyncRequestCache.AbsoluteLiveTime, CacheFullBehavior.ExpireExisting)
		{
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00024D94 File Offset: 0x00022F94
		protected override ActiveSyncRequestData CreateOnCacheMiss(Guid key, ref bool shouldAdd)
		{
			shouldAdd = true;
			return new ActiveSyncRequestData(key);
		}

		// Token: 0x04000447 RID: 1095
		private static readonly int MaxCacheCount = GlobalSettings.RequestCacheMaxCount;

		// Token: 0x04000448 RID: 1096
		private static readonly TimeSpan AbsoluteLiveTime = TimeSpan.FromMinutes((double)GlobalSettings.RequestCacheTimeInterval);

		// Token: 0x04000449 RID: 1097
		private static object lockObject = new object();

		// Token: 0x0400044A RID: 1098
		private static ActiveSyncRequestCache instance;
	}
}
