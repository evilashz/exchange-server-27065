using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000089 RID: 137
	[Flags]
	internal enum SaveChangesMode : byte
	{
		// Token: 0x040001DD RID: 477
		Close = 0,
		// Token: 0x040001DE RID: 478
		KeepOpenReadOnly = 1,
		// Token: 0x040001DF RID: 479
		KeepOpenReadWrite = 2,
		// Token: 0x040001E0 RID: 480
		ForceSave = 4,
		// Token: 0x040001E1 RID: 481
		DelayedCall = 8,
		// Token: 0x040001E2 RID: 482
		SkipQuotaCheck = 16,
		// Token: 0x040001E3 RID: 483
		TransportDelivery = 32,
		// Token: 0x040001E4 RID: 484
		IMAPChange = 64,
		// Token: 0x040001E5 RID: 485
		ForceNotificationPublish = 128
	}
}
