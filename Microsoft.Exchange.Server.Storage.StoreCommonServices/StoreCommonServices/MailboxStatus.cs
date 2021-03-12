using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000055 RID: 85
	public enum MailboxStatus : short
	{
		// Token: 0x040002AE RID: 686
		Invalid,
		// Token: 0x040002AF RID: 687
		New,
		// Token: 0x040002B0 RID: 688
		UserAccessible,
		// Token: 0x040002B1 RID: 689
		Disabled,
		// Token: 0x040002B2 RID: 690
		SoftDeleted,
		// Token: 0x040002B3 RID: 691
		HardDeleted,
		// Token: 0x040002B4 RID: 692
		Tombstone
	}
}
