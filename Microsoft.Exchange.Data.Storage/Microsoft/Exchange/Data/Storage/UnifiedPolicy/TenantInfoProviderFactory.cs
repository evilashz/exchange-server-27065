using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E94 RID: 3732
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TenantInfoProviderFactory : ITenantInfoProviderFactory
	{
		// Token: 0x060081D4 RID: 33236 RVA: 0x00237965 File Offset: 0x00235B65
		public TenantInfoProviderFactory(TimeSpan cacheExpiry, int cacheBucket = 10, int cacheBucketSize = 1000)
		{
			this.tenantInfoProviderCache = new TenantInfoProviderFactory.TenantInfoProviderCache(cacheExpiry, cacheBucket, cacheBucketSize);
		}

		// Token: 0x060081D5 RID: 33237 RVA: 0x0023797C File Offset: 0x00235B7C
		public ITenantInfoProvider CreateTenantInfoProvider(TenantContext tenantContext)
		{
			ITenantInfoProvider result;
			try
			{
				result = this.tenantInfoProviderCache.Get(tenantContext.TenantId);
			}
			catch (ADTransientException innerException)
			{
				throw new SyncAgentTransientException("CreateTenantInfoProvider failed with ADTransientException", innerException);
			}
			catch (DataSourceOperationException innerException2)
			{
				throw new SyncAgentPermanentException("CreateTenantInfoProvider failed with SyncAgentPermanentException", innerException2);
			}
			catch (DataValidationException innerException3)
			{
				throw new SyncAgentPermanentException("CreateTenantInfoProvider failed with DataValidationException", innerException3);
			}
			catch (StoragePermanentException innerException4)
			{
				throw new SyncAgentPermanentException("CreateTenantInfoProvider failed with StoragePermanentException", innerException4);
			}
			catch (StorageTransientException innerException5)
			{
				throw new SyncAgentTransientException("CreateTenantInfoProvider failed with StorageTransientException", innerException5);
			}
			return result;
		}

		// Token: 0x04005724 RID: 22308
		private readonly TenantInfoProviderFactory.TenantInfoProviderCache tenantInfoProviderCache;

		// Token: 0x02000E95 RID: 3733
		private sealed class TenantInfoProviderCache : LazyLookupTimeoutCache<Guid, TenantInfoProvider>
		{
			// Token: 0x060081D6 RID: 33238 RVA: 0x00237A24 File Offset: 0x00235C24
			public TenantInfoProviderCache(TimeSpan cacheExpiry, int cacheBucket = 10, int cacheBucketSize = 1001) : base(cacheBucket, cacheBucketSize, false, cacheExpiry)
			{
			}

			// Token: 0x060081D7 RID: 33239 RVA: 0x00237A30 File Offset: 0x00235C30
			protected override TenantInfoProvider CreateOnCacheMiss(Guid key, ref bool shouldAdd)
			{
				OrganizationId scopingOrganizationId = OrganizationId.FromExternalDirectoryOrganizationId(key);
				ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(scopingOrganizationId);
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.PartiallyConsistent, adsessionSettings, 99, "CreateOnCacheMiss", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\UnifiedPolicy\\TenantInfoProviderFactory.cs");
				ADUser discoveryMailbox = MailboxDataProvider.GetDiscoveryMailbox(tenantOrRootOrgRecipientSession);
				ExchangePrincipal syncMailboxPrincipal = ExchangePrincipal.FromADUser(adsessionSettings, discoveryMailbox);
				return new TenantInfoProvider(syncMailboxPrincipal);
			}
		}
	}
}
