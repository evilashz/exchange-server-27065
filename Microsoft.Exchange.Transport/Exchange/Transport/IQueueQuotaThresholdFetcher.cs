using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000347 RID: 839
	internal interface IQueueQuotaThresholdFetcher
	{
		// Token: 0x06002423 RID: 9251
		int GetOrganizationQuotaHighMark(Guid organizationId, QueueQuotaResources resource);

		// Token: 0x06002424 RID: 9252
		int GetOrganizationWarningMark(Guid organizationId, QueueQuotaResources resource);

		// Token: 0x06002425 RID: 9253
		int GetSenderQuotaHighMark(Guid organizationId, string sender, QueueQuotaResources resource);

		// Token: 0x06002426 RID: 9254
		int GetSenderWarningMark(Guid organizationId, string sender, QueueQuotaResources resource);

		// Token: 0x06002427 RID: 9255
		int GetAccountForestQuotaHighMark(QueueQuotaResources resource);

		// Token: 0x06002428 RID: 9256
		int GetAccountForestWarningMark(QueueQuotaResources resource);

		// Token: 0x06002429 RID: 9257
		int GetAvailableResourceSize(QueueQuotaResources resource);
	}
}
