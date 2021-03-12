using System;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x0200004E RID: 78
	public enum ReplicationStatus
	{
		// Token: 0x04000156 RID: 342
		Pending,
		// Token: 0x04000157 RID: 343
		Succeeded,
		// Token: 0x04000158 RID: 344
		Failed,
		// Token: 0x04000159 RID: 345
		FailedUnprovisionedUsers,
		// Token: 0x0400015A RID: 346
		FailedDistributionGroups,
		// Token: 0x0400015B RID: 347
		FailedPermanentError,
		// Token: 0x0400015C RID: 348
		FailedNoRecipientsResolved,
		// Token: 0x0400015D RID: 349
		FailedUnprovisionedUserAndDistributionLists
	}
}
