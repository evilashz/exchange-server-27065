using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200026C RID: 620
	[Flags]
	internal enum ReservationFlags
	{
		// Token: 0x04000C91 RID: 3217
		None = 0,
		// Token: 0x04000C92 RID: 3218
		Read = 1,
		// Token: 0x04000C93 RID: 3219
		Write = 2,
		// Token: 0x04000C94 RID: 3220
		HighPriority = 4,
		// Token: 0x04000C95 RID: 3221
		Move = 16,
		// Token: 0x04000C96 RID: 3222
		Merge = 32,
		// Token: 0x04000C97 RID: 3223
		Archive = 64,
		// Token: 0x04000C98 RID: 3224
		PST = 128,
		// Token: 0x04000C99 RID: 3225
		Interactive = 256,
		// Token: 0x04000C9A RID: 3226
		InternalMaintenance = 512
	}
}
