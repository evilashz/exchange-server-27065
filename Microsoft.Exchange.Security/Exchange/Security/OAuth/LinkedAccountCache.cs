using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000BF RID: 191
	internal class LinkedAccountCache : LazyLookupTimeoutCache<ADObjectId, ADUser>
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x0002F40E File Offset: 0x0002D60E
		private LinkedAccountCache() : base(1, LinkedAccountCache.CacheSize.Value, false, LinkedAccountCache.CacheTimeToLive.Value)
		{
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0002F42C File Offset: 0x0002D62C
		public static LinkedAccountCache Instance
		{
			get
			{
				return LinkedAccountCache.LazyInstance.Value;
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0002F45C File Offset: 0x0002D65C
		protected override ADUser CreateOnCacheMiss(ADObjectId key, ref bool shouldAdd)
		{
			ADUser result = null;
			ADNotificationAdapter.RunADOperation(delegate()
			{
				result = this.ResolveLinkedAccount(key);
			});
			if (result == null)
			{
				ExTraceGlobals.OAuthTracer.Information<ADObjectId>((long)this.GetHashCode(), "[LinkedAccountCache::CreateOnCacheMiss] Skip to put null in the cache. Key: {0}", key);
				shouldAdd = false;
			}
			return result;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0002F4C4 File Offset: 0x0002D6C4
		private ADUser ResolveLinkedAccount(ADObjectId linkedAccount)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(linkedAccount);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 99, "ResolveLinkedAccount", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\OAuth\\LinkedAccountCache.cs");
			ExTraceGlobals.OAuthTracer.TraceDebug<ADObjectId>(0, 0L, "[LinkedAccountCache::ResolveLinkedAccount] Lookup linkedAccount in AD. LinkedAccount: {0}", linkedAccount);
			return tenantOrRootOrgRecipientSession.FindADUserByObjectId(linkedAccount);
		}

		// Token: 0x0400062F RID: 1583
		private static readonly TimeSpanAppSettingsEntry CacheTimeToLive = new TimeSpanAppSettingsEntry("OAuthLinkedAccountCacheTimeToLive", TimeSpanUnit.Seconds, TimeSpan.FromHours(24.0), ExTraceGlobals.OAuthTracer);

		// Token: 0x04000630 RID: 1584
		private static readonly IntAppSettingsEntry CacheSize = new IntAppSettingsEntry("OAuthLinkedAccountCacheMaxItems", 1000, ExTraceGlobals.OAuthTracer);

		// Token: 0x04000631 RID: 1585
		private static readonly Lazy<LinkedAccountCache> LazyInstance = new Lazy<LinkedAccountCache>(() => new LinkedAccountCache());
	}
}
