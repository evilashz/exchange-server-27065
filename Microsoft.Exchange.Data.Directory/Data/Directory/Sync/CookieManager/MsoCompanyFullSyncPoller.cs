using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007D5 RID: 2005
	internal sealed class MsoCompanyFullSyncPoller : MsoFullSyncPoller
	{
		// Token: 0x0600637D RID: 25469 RVA: 0x00159148 File Offset: 0x00157348
		public MsoCompanyFullSyncPoller(string serviceInstanceName) : base(serviceInstanceName)
		{
		}

		// Token: 0x0600637E RID: 25470 RVA: 0x00159154 File Offset: 0x00157354
		protected override QueryFilter RetrieveFullSyncTenantsSearchFilter()
		{
			return new AndFilter(new QueryFilter[]
			{
				new ExistsFilter(MsoTenantCookieContainerSchema.MsoForwardSyncNonRecipientCookie),
				base.RetrieveFullSyncTenantsSearchFilter()
			});
		}
	}
}
