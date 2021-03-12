using System;
using Microsoft.Exchange.Collections.TimeoutCache;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000010 RID: 16
	internal sealed class PopImapRequestCache : LazyLookupExactTimeoutCache<Guid, PopImapRequestData>
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004F94 File Offset: 0x00003194
		public static PopImapRequestCache Instance
		{
			get
			{
				if (PopImapRequestCache.instance == null)
				{
					lock (PopImapRequestCache.lockObject)
					{
						if (PopImapRequestCache.instance == null)
						{
							PopImapRequestCache.instance = new PopImapRequestCache();
						}
					}
				}
				return PopImapRequestCache.instance;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004FEC File Offset: 0x000031EC
		private PopImapRequestCache() : base(PopImapRequestCache.MaxCacheCount, false, PopImapRequestCache.AbsoluteLiveTime, CacheFullBehavior.ExpireExisting)
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005000 File Offset: 0x00003200
		protected override PopImapRequestData CreateOnCacheMiss(Guid key, ref bool shouldAdd)
		{
			shouldAdd = true;
			return new PopImapRequestData(key);
		}

		// Token: 0x0400007C RID: 124
		private static readonly int MaxCacheCount = 5000;

		// Token: 0x0400007D RID: 125
		private static readonly TimeSpan AbsoluteLiveTime = TimeSpan.FromMinutes(10.0);

		// Token: 0x0400007E RID: 126
		private static object lockObject = new object();

		// Token: 0x0400007F RID: 127
		private static PopImapRequestCache instance;
	}
}
