using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200023B RID: 571
	internal interface ITenantDagQuota
	{
		// Token: 0x0600190E RID: 6414
		int GetDagCountForTenant(Guid externalOrganizationId);

		// Token: 0x0600190F RID: 6415
		void IncrementMessagesDeliveredToTenant(Guid externalOrganizationId);

		// Token: 0x06001910 RID: 6416
		void RefreshDagCount(int dagsAvailable);
	}
}
