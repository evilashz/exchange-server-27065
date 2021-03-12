using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000061 RID: 97
	[Flags]
	internal enum MailboxType
	{
		// Token: 0x040001C6 RID: 454
		User = 1,
		// Token: 0x040001C7 RID: 455
		System = 2,
		// Token: 0x040001C8 RID: 456
		Archive = 4,
		// Token: 0x040001C9 RID: 457
		Arbitration = 8,
		// Token: 0x040001CA RID: 458
		PublicFolder = 16,
		// Token: 0x040001CB RID: 459
		InactiveMailbox = 32,
		// Token: 0x040001CC RID: 460
		All = 31
	}
}
