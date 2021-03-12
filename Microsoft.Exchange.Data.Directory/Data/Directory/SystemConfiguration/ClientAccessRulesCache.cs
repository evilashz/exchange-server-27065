using System;
using Microsoft.Exchange.Data.ClientAccessRules;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000632 RID: 1586
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ClientAccessRulesCache : TenantConfigurationCache<ClientAccessRuleCollectionCacheableItem>
	{
		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x06004B1B RID: 19227 RVA: 0x001150BF File Offset: 0x001132BF
		private static TimeSpan CacheTimeToLive
		{
			get
			{
				return ClientAccessRulesCache.CacheTimeToLiveData.Value;
			}
		}

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x06004B1C RID: 19228 RVA: 0x001150CB File Offset: 0x001132CB
		public static ClientAccessRulesCache Instance
		{
			get
			{
				return ClientAccessRulesCache.instance;
			}
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x001150D4 File Offset: 0x001132D4
		public ClientAccessRuleCollection GetCollection(OrganizationId orgId)
		{
			if (OrganizationId.ForestWideOrgId.Equals(orgId))
			{
				return this.GetValue(orgId).ClientAccessRuleCollection;
			}
			ClientAccessRuleCollection clientAccessRuleCollection = new ClientAccessRuleCollection(orgId.ToString());
			clientAccessRuleCollection.AddClientAccessRuleCollection(this.GetValue(OrganizationId.ForestWideOrgId).ClientAccessRuleCollection);
			clientAccessRuleCollection.AddClientAccessRuleCollection(this.GetValue(orgId).ClientAccessRuleCollection);
			return clientAccessRuleCollection;
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x00115130 File Offset: 0x00113330
		private ClientAccessRulesCache() : base(ClientAccessRulesCache.CacheSizeInBytes, ClientAccessRulesCache.CacheTimeToLive, TimeSpan.Zero, null, null)
		{
		}

		// Token: 0x040033A0 RID: 13216
		private static readonly long CacheSizeInBytes = (long)ByteQuantifiedSize.FromMB(1UL).ToBytes();

		// Token: 0x040033A1 RID: 13217
		private static readonly TimeSpanAppSettingsEntry CacheTimeToLiveData = new TimeSpanAppSettingsEntry("ClientAccessRulesCacheTimeToLive", TimeSpanUnit.Seconds, TimeSpan.FromMinutes((double)VariantConfiguration.InvariantNoFlightingSnapshot.ClientAccessRulesCommon.ClientAccessRulesCacheExpiryTime.Value), ExTraceGlobals.SystemConfigurationCacheTracer);

		// Token: 0x040033A2 RID: 13218
		private static ClientAccessRulesCache instance = new ClientAccessRulesCache();
	}
}
