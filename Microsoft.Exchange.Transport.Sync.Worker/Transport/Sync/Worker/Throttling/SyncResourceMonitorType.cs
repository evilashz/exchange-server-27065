using System;

namespace Microsoft.Exchange.Transport.Sync.Worker.Throttling
{
	// Token: 0x02000038 RID: 56
	internal enum SyncResourceMonitorType
	{
		// Token: 0x0400017E RID: 382
		DatabaseRPCLatency,
		// Token: 0x0400017F RID: 383
		DatabaseReplicationLog,
		// Token: 0x04000180 RID: 384
		MailboxCPU,
		// Token: 0x04000181 RID: 385
		ServerTransportQueue,
		// Token: 0x04000182 RID: 386
		UserTransportQueue,
		// Token: 0x04000183 RID: 387
		Unknown
	}
}
