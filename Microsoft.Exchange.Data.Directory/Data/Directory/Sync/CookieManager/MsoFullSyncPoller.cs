using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007D4 RID: 2004
	internal abstract class MsoFullSyncPoller : FullSyncPoller
	{
		// Token: 0x06006379 RID: 25465 RVA: 0x001590B3 File Offset: 0x001572B3
		protected MsoFullSyncPoller(string serviceInstanceName)
		{
			this.msoServiceInstanceName = serviceInstanceName;
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x001590CC File Offset: 0x001572CC
		public override IEnumerable<string> GetFullSyncTenants()
		{
			QueryFilter filter = this.RetrieveFullSyncTenantsSearchFilter();
			return from cu in PartitionDataAggregator.FindTenantCookieContainers(filter)
			select cu.ExternalDirectoryOrganizationId;
		}

		// Token: 0x0600637B RID: 25467 RVA: 0x00159108 File Offset: 0x00157308
		protected virtual QueryFilter RetrieveFullSyncTenantsSearchFilter()
		{
			return new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.DirSyncServiceInstance, this.msoServiceInstanceName),
				new NotFilter(new ExistsFilter(ExchangeConfigurationUnitSchema.DirSyncServiceInstance))
			});
		}

		// Token: 0x0400424B RID: 16971
		private readonly string msoServiceInstanceName;
	}
}
