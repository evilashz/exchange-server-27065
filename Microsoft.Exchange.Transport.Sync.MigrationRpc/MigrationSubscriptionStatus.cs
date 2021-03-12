using System;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000003 RID: 3
	public enum MigrationSubscriptionStatus
	{
		// Token: 0x04000002 RID: 2
		None = 1,
		// Token: 0x04000003 RID: 3
		InvalidPathPrefix,
		// Token: 0x04000004 RID: 4
		MailboxNotFound,
		// Token: 0x04000005 RID: 5
		RpcThresholdExceeded,
		// Token: 0x04000006 RID: 6
		SubscriptionNotFound
	}
}
