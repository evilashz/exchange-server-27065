using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B33 RID: 2867
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PingerCache : LazyLookupExactTimeoutCache<Guid, IMdbSystemMailboxPinger>
	{
		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x060067BD RID: 26557 RVA: 0x001B68DF File Offset: 0x001B4ADF
		// (set) Token: 0x060067BE RID: 26558 RVA: 0x001B68E6 File Offset: 0x001B4AE6
		public static Func<Guid, IMdbSystemMailboxPinger> CreatePingerTestHook { get; set; }

		// Token: 0x060067BF RID: 26559 RVA: 0x001B68EE File Offset: 0x001B4AEE
		private PingerCache() : base(1000000, true, TimeSpan.MaxValue, CacheFullBehavior.ExpireExisting)
		{
		}

		// Token: 0x060067C0 RID: 26560 RVA: 0x001B6904 File Offset: 0x001B4B04
		protected override IMdbSystemMailboxPinger CreateOnCacheMiss(Guid key, ref bool shouldAdd)
		{
			shouldAdd = true;
			if (PingerCache.CreatePingerTestHook != null)
			{
				return PingerCache.CreatePingerTestHook(key);
			}
			IDatabaseInformation databaseInformation = DatabaseInformationCache.Singleton.Get(key);
			if (databaseInformation != null)
			{
				ExTraceGlobals.DatabasePingerTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "[PingerCache.CreateOnCacheMiss] Acquire database information for Mdb: {0}, Guid: {1}", databaseInformation.DatabaseName, databaseInformation.DatabaseGuid);
				return new MdbSystemMailboxPinger(key);
			}
			ExTraceGlobals.DatabasePingerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "[PingerCache.CreateOnCacheMiss] Failed to get database information for Mdb Guid: {0}.  NOT creating pinger - caching null instead.", key);
			return null;
		}

		// Token: 0x060067C1 RID: 26561 RVA: 0x001B6978 File Offset: 0x001B4B78
		protected override void CleanupValue(Guid key, IMdbSystemMailboxPinger value)
		{
			IDisposable disposable = value as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x04003ACE RID: 15054
		public static readonly PingerCache Singleton = new PingerCache();
	}
}
