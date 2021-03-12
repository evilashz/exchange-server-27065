using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000320 RID: 800
	[Flags]
	internal enum NotificationFlags : ushort
	{
		// Token: 0x04000A23 RID: 2595
		CriticalError = 1,
		// Token: 0x04000A24 RID: 2596
		NewMail = 2,
		// Token: 0x04000A25 RID: 2597
		ObjectCreated = 4,
		// Token: 0x04000A26 RID: 2598
		ObjectDeleted = 8,
		// Token: 0x04000A27 RID: 2599
		ObjectModified = 16,
		// Token: 0x04000A28 RID: 2600
		ObjectMoved = 32,
		// Token: 0x04000A29 RID: 2601
		ObjectCopied = 64,
		// Token: 0x04000A2A RID: 2602
		SearchComplete = 128,
		// Token: 0x04000A2B RID: 2603
		TableModified = 256,
		// Token: 0x04000A2C RID: 2604
		Ics = 512,
		// Token: 0x04000A2D RID: 2605
		Extended = 1024
	}
}
