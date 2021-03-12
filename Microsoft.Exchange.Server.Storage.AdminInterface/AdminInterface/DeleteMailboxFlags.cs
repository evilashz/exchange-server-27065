using System;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x0200005E RID: 94
	[Flags]
	internal enum DeleteMailboxFlags : uint
	{
		// Token: 0x040001D5 RID: 469
		None = 0U,
		// Token: 0x040001D6 RID: 470
		MailboxMoved = 1U,
		// Token: 0x040001D7 RID: 471
		HardDelete = 2U,
		// Token: 0x040001D8 RID: 472
		MoveFailed = 4U,
		// Token: 0x040001D9 RID: 473
		SoftDelete = 8U,
		// Token: 0x040001DA RID: 474
		RemoveSoftDeleted = 16U,
		// Token: 0x040001DB RID: 475
		AcceptableFlagsMask = 31U
	}
}
