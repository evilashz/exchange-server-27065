using System;
using System.Diagnostics;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.PartnerToken;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200006F RID: 111
	internal sealed class MailboxAccessPartnerInfoCache : LazyLookupTimeoutCache<MailboxAccessPartnerInfoCacheKey, PartnerInfo>
	{
		// Token: 0x06000302 RID: 770 RVA: 0x00013DF2 File Offset: 0x00011FF2
		private MailboxAccessPartnerInfoCache() : base(1, MailboxAccessPartnerInfoCache.cacheSize.Value, false, MailboxAccessPartnerInfoCache.cacheTimeToLive.Value)
		{
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00013E10 File Offset: 0x00012010
		public static MailboxAccessPartnerInfoCache Singleton
		{
			get
			{
				MailboxAccessPartnerInfoCache result;
				lock (MailboxAccessPartnerInfoCache.lockObj)
				{
					if (MailboxAccessPartnerInfoCache.singleton == null)
					{
						MailboxAccessPartnerInfoCache.singleton = new MailboxAccessPartnerInfoCache();
					}
					result = MailboxAccessPartnerInfoCache.singleton;
				}
				return result;
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00013E64 File Offset: 0x00012064
		protected override PartnerInfo CreateOnCacheMiss(MailboxAccessPartnerInfoCacheKey key, ref bool shouldAdd)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				for (int i = 0; i < MailboxAccessPartnerInfoCache.lookupRetryMax.Value; i++)
				{
					try
					{
						return PartnerInfo.CreateFromADObjectId(key.ADObjectId, key.OrganizationId);
					}
					catch (ADTransientException arg)
					{
						ExTraceGlobals.FrameworkTracer.TraceDebug<int, ADTransientException>(0L, "[MailboxAccessPartnerInfoCache::CreateOnCacheMiss] PartnerInfo.CreateFromSid will retry for {0} times, for ADTransientException {1}", i, arg);
					}
				}
			}
			finally
			{
				PerformanceCounters.UpdateAveragePartnerInfoQueryTime(stopwatch.ElapsedMilliseconds);
			}
			ExTraceGlobals.FrameworkTracer.TraceWarning(0L, "[MailboxAccessPartnerInfoCache::CreateOnCacheMiss] PartnerInfo.CreateFromSid will return Invalid after several retries");
			return PartnerInfo.Invalid;
		}

		// Token: 0x040002D8 RID: 728
		private static readonly TimeSpanAppSettingsEntry cacheTimeToLive = new TimeSpanAppSettingsEntry("PartnerInfoCacheTimeToLive", TimeSpanUnit.Seconds, TimeSpan.FromMinutes(60.0), ExTraceGlobals.FrameworkTracer);

		// Token: 0x040002D9 RID: 729
		private static readonly IntAppSettingsEntry cacheSize = new IntAppSettingsEntry("PartnerInfoCacheMaxItems", 1024, ExTraceGlobals.FrameworkTracer);

		// Token: 0x040002DA RID: 730
		private static readonly IntAppSettingsEntry lookupRetryMax = new IntAppSettingsEntry("PartnerInfoCacheLookupRetryMax", 3, ExTraceGlobals.FrameworkTracer);

		// Token: 0x040002DB RID: 731
		private static readonly object lockObj = new object();

		// Token: 0x040002DC RID: 732
		private static MailboxAccessPartnerInfoCache singleton = null;
	}
}
