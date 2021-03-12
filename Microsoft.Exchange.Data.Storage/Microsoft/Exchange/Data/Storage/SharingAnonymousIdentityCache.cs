using System;
using System.Security.Principal;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DBF RID: 3519
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SharingAnonymousIdentityCache : LazyLookupTimeoutCache<SharingAnonymousIdentityCacheKey, SharingAnonymousIdentityCacheValue>
	{
		// Token: 0x060078DF RID: 30943 RVA: 0x00215F58 File Offset: 0x00214158
		private SharingAnonymousIdentityCache() : base(SharingAnonymousIdentityCache.SharingAnonymousIdentityCacheBuckets.Value, SharingAnonymousIdentityCache.SharingAnonymousIdentityCacheCacheBucketSize.Value, false, SharingAnonymousIdentityCache.SharingAnonymousIdentityCacheTimeToExpire.Value, SharingAnonymousIdentityCache.SharingAnonymousIdentityCacheTimeToLive.Value)
		{
		}

		// Token: 0x060078E0 RID: 30944 RVA: 0x00215F8C File Offset: 0x0021418C
		protected override SharingAnonymousIdentityCacheValue CreateOnCacheMiss(SharingAnonymousIdentityCacheKey key, ref bool shouldAdd)
		{
			SecurityIdentifier sid = null;
			string folderId = key.Lookup(out sid);
			return new SharingAnonymousIdentityCacheValue(folderId, sid);
		}

		// Token: 0x1700204E RID: 8270
		// (get) Token: 0x060078E1 RID: 30945 RVA: 0x00215FAB File Offset: 0x002141AB
		public static SharingAnonymousIdentityCache Instance
		{
			get
			{
				return SharingAnonymousIdentityCache.instance;
			}
		}

		// Token: 0x0400537C RID: 21372
		private static readonly IntAppSettingsEntry SharingAnonymousIdentityCacheCacheBucketSize = new IntAppSettingsEntry("SharingAnonymousIdentityCacheCacheBucketSize", 1000, ExTraceGlobals.SharingTracer);

		// Token: 0x0400537D RID: 21373
		private static readonly IntAppSettingsEntry SharingAnonymousIdentityCacheBuckets = new IntAppSettingsEntry("SharingAnonymousIdentityCacheBuckets", 5, ExTraceGlobals.SharingTracer);

		// Token: 0x0400537E RID: 21374
		private static readonly TimeSpanAppSettingsEntry SharingAnonymousIdentityCacheTimeToExpire = new TimeSpanAppSettingsEntry("SharingAnonymousIdentityCacheTimeToExpire", TimeSpanUnit.Minutes, TimeSpan.FromMinutes(2.0), ExTraceGlobals.SharingTracer);

		// Token: 0x0400537F RID: 21375
		private static readonly TimeSpanAppSettingsEntry SharingAnonymousIdentityCacheTimeToLive = new TimeSpanAppSettingsEntry("SharingAnonymousIdentityCacheTimeToLive", TimeSpanUnit.Minutes, TimeSpan.FromMinutes(20.0), ExTraceGlobals.SharingTracer);

		// Token: 0x04005380 RID: 21376
		private static readonly SharingAnonymousIdentityCache instance = new SharingAnonymousIdentityCache();
	}
}
