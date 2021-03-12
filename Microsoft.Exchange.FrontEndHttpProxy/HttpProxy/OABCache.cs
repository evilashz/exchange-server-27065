using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000033 RID: 51
	internal sealed class OABCache
	{
		// Token: 0x06000194 RID: 404 RVA: 0x000093CD File Offset: 0x000075CD
		private OABCache()
		{
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000093F0 File Offset: 0x000075F0
		public static OABCache Instance
		{
			get
			{
				if (OABCache.instance == null)
				{
					lock (OABCache.staticLock)
					{
						if (OABCache.instance == null)
						{
							OABCache.instance = new OABCache();
						}
					}
				}
				return OABCache.instance;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009448 File Offset: 0x00007648
		public OABCache.OABCacheEntry GetOABFromCacheOrAD(Guid exchangeObjectId, string userAcceptedDomain)
		{
			OABCache.OABCacheEntry oabcacheEntry = null;
			if (this.oabTimeoutCache.TryGetValue(exchangeObjectId, out oabcacheEntry))
			{
				return oabcacheEntry;
			}
			IConfigurationSession configurationSessionFromDomain = DirectoryHelper.GetConfigurationSessionFromDomain(userAcceptedDomain);
			OfflineAddressBook offlineAddressBook = configurationSessionFromDomain.FindByExchangeObjectId<OfflineAddressBook>(exchangeObjectId);
			if (offlineAddressBook == null)
			{
				throw new ADNoSuchObjectException(new LocalizedString(exchangeObjectId.ToString()));
			}
			oabcacheEntry = new OABCache.OABCacheEntry(offlineAddressBook);
			this.oabTimeoutCache.TryInsertAbsolute(exchangeObjectId, oabcacheEntry, OABCache.cacheTimeToLive.Value);
			return oabcacheEntry;
		}

		// Token: 0x040000A4 RID: 164
		private static TimeSpanAppSettingsEntry cacheTimeToLive = new TimeSpanAppSettingsEntry("OabCacheTimeToLive", TimeSpanUnit.Seconds, TimeSpan.FromMinutes(10.0), ExTraceGlobals.FrameworkTracer);

		// Token: 0x040000A5 RID: 165
		private static IntAppSettingsEntry cacheBucketSize = new IntAppSettingsEntry("OabCacheMaximumBucketSize", 1000, ExTraceGlobals.FrameworkTracer);

		// Token: 0x040000A6 RID: 166
		private static OABCache instance;

		// Token: 0x040000A7 RID: 167
		private static object staticLock = new object();

		// Token: 0x040000A8 RID: 168
		private ExactTimeoutCache<Guid, OABCache.OABCacheEntry> oabTimeoutCache = new ExactTimeoutCache<Guid, OABCache.OABCacheEntry>(null, null, null, OABCache.cacheBucketSize.Value, false);

		// Token: 0x02000034 RID: 52
		internal sealed class OABCacheEntry
		{
			// Token: 0x06000198 RID: 408 RVA: 0x00009508 File Offset: 0x00007708
			internal OABCacheEntry(OfflineAddressBook oab)
			{
				this.exchangeVersion = oab.ExchangeVersion;
				this.virtualDirectories = oab.VirtualDirectories;
				this.globalWebDistributionEnabled = oab.GlobalWebDistributionEnabled;
				this.generatingMailbox = oab.GeneratingMailbox;
				this.shadowMailboxDistributionEnabled = oab.ShadowMailboxDistributionEnabled;
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000199 RID: 409 RVA: 0x00009557 File Offset: 0x00007757
			internal ExchangeObjectVersion ExchangeVersion
			{
				get
				{
					return this.exchangeVersion;
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x0600019A RID: 410 RVA: 0x0000955F File Offset: 0x0000775F
			internal MultiValuedProperty<ADObjectId> VirtualDirectories
			{
				get
				{
					return this.virtualDirectories;
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x0600019B RID: 411 RVA: 0x00009567 File Offset: 0x00007767
			internal bool GlobalWebDistributionEnabled
			{
				get
				{
					return this.globalWebDistributionEnabled;
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x0600019C RID: 412 RVA: 0x0000956F File Offset: 0x0000776F
			internal bool ShadowMailboxDistributionEnabled
			{
				get
				{
					return this.shadowMailboxDistributionEnabled;
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x0600019D RID: 413 RVA: 0x00009577 File Offset: 0x00007777
			internal ADObjectId GeneratingMailbox
			{
				get
				{
					return this.generatingMailbox;
				}
			}

			// Token: 0x040000A9 RID: 169
			private readonly ExchangeObjectVersion exchangeVersion;

			// Token: 0x040000AA RID: 170
			private readonly MultiValuedProperty<ADObjectId> virtualDirectories;

			// Token: 0x040000AB RID: 171
			private readonly bool globalWebDistributionEnabled;

			// Token: 0x040000AC RID: 172
			private readonly bool shadowMailboxDistributionEnabled;

			// Token: 0x040000AD RID: 173
			private readonly ADObjectId generatingMailbox;
		}
	}
}
