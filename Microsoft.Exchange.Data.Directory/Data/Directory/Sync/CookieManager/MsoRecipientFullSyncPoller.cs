using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007E1 RID: 2017
	internal sealed class MsoRecipientFullSyncPoller : MsoFullSyncPoller
	{
		// Token: 0x060063E2 RID: 25570 RVA: 0x0015ACB7 File Offset: 0x00158EB7
		public MsoRecipientFullSyncPoller(string serviceInstanceName) : base(serviceInstanceName)
		{
		}

		// Token: 0x060063E3 RID: 25571 RVA: 0x0015ACC0 File Offset: 0x00158EC0
		protected override QueryFilter RetrieveFullSyncTenantsSearchFilter()
		{
			return new AndFilter(new QueryFilter[]
			{
				new ExistsFilter(MsoTenantCookieContainerSchema.MsoForwardSyncRecipientCookie),
				base.RetrieveFullSyncTenantsSearchFilter(),
				new ComparisonFilter(ComparisonOperator.NotEqual, ExchangeConfigurationUnitSchema.OrganizationStatus, OrganizationStatus.ReadyForRemoval),
				new ComparisonFilter(ComparisonOperator.NotEqual, ExchangeConfigurationUnitSchema.OrganizationStatus, OrganizationStatus.SoftDeleted),
				new ComparisonFilter(ComparisonOperator.NotEqual, ExchangeConfigurationUnitSchema.OrganizationStatus, OrganizationStatus.PendingRemoval)
			});
		}
	}
}
