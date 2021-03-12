using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000056 RID: 86
	[Flags]
	public enum AddressInfoEntryFlags
	{
		// Token: 0x040002B6 RID: 694
		EntryId = 1,
		// Token: 0x040002B7 RID: 695
		SearchKey = 2,
		// Token: 0x040002B8 RID: 696
		AddressType = 4,
		// Token: 0x040002B9 RID: 697
		EmailAddress = 8,
		// Token: 0x040002BA RID: 698
		DisplayName = 16,
		// Token: 0x040002BB RID: 699
		SimpleDisplayName = 32,
		// Token: 0x040002BC RID: 700
		Flags = 64,
		// Token: 0x040002BD RID: 701
		OriginalAddressType = 128,
		// Token: 0x040002BE RID: 702
		OriginalEmailAddress = 256,
		// Token: 0x040002BF RID: 703
		Sid = 512,
		// Token: 0x040002C0 RID: 704
		Guid = 1024
	}
}
