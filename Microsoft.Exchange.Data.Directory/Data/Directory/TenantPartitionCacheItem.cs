using System;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020001A5 RID: 421
	internal class TenantPartitionCacheItem : CachableItem
	{
		// Token: 0x060011C5 RID: 4549 RVA: 0x00056C30 File Offset: 0x00054E30
		internal TenantPartitionCacheItem(Guid accountPartitionGuid, string accountPartitionFqdn, string resourceForestFqdn, Guid externalOrgId, string tenantName, bool dataFromOfflineService = false)
		{
			ArgumentValidator.ThrowIfNullOrEmpty(accountPartitionFqdn, "accountPartitionFqdn");
			ArgumentValidator.ThrowIfNullOrEmpty(resourceForestFqdn, "resourceForestFqdn");
			if (PartitionId.IsLocalForestPartition(accountPartitionFqdn))
			{
				this.accountPartitionId = PartitionId.LocalForest;
			}
			else
			{
				this.accountPartitionId = ((accountPartitionGuid == Guid.Empty) ? new PartitionId(accountPartitionFqdn) : new PartitionId(accountPartitionFqdn, accountPartitionGuid));
			}
			this.resourceForestFqdn = resourceForestFqdn;
			this.externalOrgId = externalOrgId;
			this.tenantName = tenantName;
			this.expirationTime = DateTime.UtcNow + TenantPartitionCacheItem.CalculateCacheItemExpirationWindow(dataFromOfflineService, tenantName, this.externalOrgId, this.accountPartitionId);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00056CCC File Offset: 0x00054ECC
		private static TimeSpan CalculateCacheItemExpirationWindow(bool dataFromOfflineService, string tenantName, Guid externalOrgId, PartitionId accountPartitionId)
		{
			if (dataFromOfflineService)
			{
				return TimeSpan.FromMinutes((double)ConfigBase<AdDriverConfigSchema>.GetConfig<int>("OfflineDataCacheExpirationTimeInMinutes"));
			}
			if (!ForestTenantRelocationsCache.IsTenantRelocationAllowed(accountPartitionId.ForestFQDN))
			{
				return ExpiringTenantRelocationStateValue.TenantRelocationStateExpirationWindowProvider.DefaultExpirationWindow;
			}
			if (!string.IsNullOrEmpty(tenantName))
			{
				return TenantRelocationStateCache.GetTenantCacheExpirationWindow(tenantName, accountPartitionId);
			}
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsPartitionId(accountPartitionId), 110, "CalculateCacheItemExpirationWindow", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\TenantPartitionCacheItem.cs");
			ExchangeConfigurationUnit exchangeConfigurationUnitByExternalId = tenantConfigurationSession.GetExchangeConfigurationUnitByExternalId(externalOrgId);
			if (exchangeConfigurationUnitByExternalId != null)
			{
				tenantName = ((ADObjectId)exchangeConfigurationUnitByExternalId.Identity).Parent.Name;
				return TenantRelocationStateCache.GetTenantCacheExpirationWindow(tenantName, accountPartitionId);
			}
			return ExpiringTenantRelocationStateValue.TenantRelocationStateExpirationWindowProvider.DefaultExpirationWindow;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00056D5D File Offset: 0x00054F5D
		public override long ItemSize
		{
			get
			{
				return (long)(32 + ((this.tenantName == null) ? 0 : (this.tenantName.Length * 2)) + this.resourceForestFqdn.Length * 2);
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00056D89 File Offset: 0x00054F89
		public override bool IsExpired(DateTime currentTime)
		{
			return this.expirationTime < currentTime || base.IsExpired(currentTime);
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00056DA2 File Offset: 0x00054FA2
		internal PartitionId AccountPartitionId
		{
			get
			{
				return this.accountPartitionId;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x00056DAA File Offset: 0x00054FAA
		internal string ResourceForestFqdn
		{
			get
			{
				return this.resourceForestFqdn;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00056DB2 File Offset: 0x00054FB2
		internal Guid ExternalOrgId
		{
			get
			{
				return this.externalOrgId;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x00056DBA File Offset: 0x00054FBA
		internal string TenantName
		{
			get
			{
				return this.tenantName;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x00056DC4 File Offset: 0x00054FC4
		internal bool IsRegisteredAccountPartition
		{
			get
			{
				return this.AccountPartitionId == PartitionId.LocalForest || (this.AccountPartitionId.PartitionObjectId != null && this.AccountPartitionId.PartitionObjectId.Value != Guid.Empty);
			}
		}

		// Token: 0x04000A4C RID: 2636
		private readonly PartitionId accountPartitionId;

		// Token: 0x04000A4D RID: 2637
		private readonly string resourceForestFqdn;

		// Token: 0x04000A4E RID: 2638
		private readonly Guid externalOrgId;

		// Token: 0x04000A4F RID: 2639
		private readonly string tenantName;

		// Token: 0x04000A50 RID: 2640
		private readonly DateTime expirationTime;
	}
}
