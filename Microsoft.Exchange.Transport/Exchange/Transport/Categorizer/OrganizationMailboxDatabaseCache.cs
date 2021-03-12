using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001BB RID: 443
	internal class OrganizationMailboxDatabaseCache : DisposeTrackableBase
	{
		// Token: 0x0600146C RID: 5228 RVA: 0x000524BC File Offset: 0x000506BC
		public OrganizationMailboxDatabaseCache(TransportAppConfig.PerTenantCacheConfig settings, ProcessTransportRole processTransportRole)
		{
			this.cache = new TenantConfigurationCache<PerTenantOrganizationMailboxDatabases>((long)settings.OrganizationMailboxDatabaseCacheMaxSize.ToBytes(), settings.OrganizationMailboxDatabaseCacheExpiryInterval, settings.OrganizationMailboxDatabaseCacheCleanupInterval, new PerTenantCacheTracer(ProxyHubSelectorComponent.Tracer, "OrganizationMailboxDatabases"), new PerTenantCachePerformanceCounters(processTransportRole, "OrganizationMailboxDatabases"));
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0005250E File Offset: 0x0005070E
		protected OrganizationMailboxDatabaseCache()
		{
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00052518 File Offset: 0x00050718
		public virtual bool TryGetOrganizationMailboxDatabases(OrganizationId organizationId, out IList<ADObjectId> databaseIds)
		{
			databaseIds = null;
			if (this.cache == null)
			{
				throw new ObjectDisposedException("OrganizationMailboxDatabaseCache has been disposed");
			}
			PerTenantOrganizationMailboxDatabases perTenantOrganizationMailboxDatabases;
			if (!this.cache.TryGetValue(organizationId, out perTenantOrganizationMailboxDatabases))
			{
				return false;
			}
			databaseIds = perTenantOrganizationMailboxDatabases.Databases;
			return true;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00052556 File Offset: 0x00050756
		public virtual void Clear()
		{
			if (this.cache != null)
			{
				this.cache.Clear();
			}
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0005256B File Offset: 0x0005076B
		protected override void InternalDispose(bool disposing)
		{
			if (this.cache != null)
			{
				this.cache.Dispose();
				this.cache = null;
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00052587 File Offset: 0x00050787
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OrganizationMailboxDatabaseCache>(this);
		}

		// Token: 0x04000A58 RID: 2648
		private TenantConfigurationCache<PerTenantOrganizationMailboxDatabases> cache;
	}
}
