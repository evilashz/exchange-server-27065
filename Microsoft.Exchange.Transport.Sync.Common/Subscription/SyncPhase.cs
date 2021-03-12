using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000CE RID: 206
	public enum SyncPhase
	{
		// Token: 0x04000346 RID: 838
		Initial,
		// Token: 0x04000347 RID: 839
		Incremental,
		// Token: 0x04000348 RID: 840
		Finalization,
		// Token: 0x04000349 RID: 841
		Completed,
		// Token: 0x0400034A RID: 842
		Delete
	}
}
