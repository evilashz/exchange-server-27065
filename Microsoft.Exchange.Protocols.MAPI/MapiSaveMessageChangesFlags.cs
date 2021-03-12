using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000069 RID: 105
	[Flags]
	public enum MapiSaveMessageChangesFlags
	{
		// Token: 0x040001E1 RID: 481
		None = 0,
		// Token: 0x040001E2 RID: 482
		IMAPIDChange = 1,
		// Token: 0x040001E3 RID: 483
		ForceSave = 2,
		// Token: 0x040001E4 RID: 484
		SkipMailboxQuotaCheck = 4,
		// Token: 0x040001E5 RID: 485
		SkipFolderQuotaCheck = 8,
		// Token: 0x040001E6 RID: 486
		SkipQuotaCheck = 12,
		// Token: 0x040001E7 RID: 487
		NonFatalDuplicateKey = 16,
		// Token: 0x040001E8 RID: 488
		ForceCreatedEventForCopy = 32,
		// Token: 0x040001E9 RID: 489
		Submit = 65536,
		// Token: 0x040001EA RID: 490
		SkipSizeCheck = 131072,
		// Token: 0x040001EB RID: 491
		Delivery = 262144
	}
}
