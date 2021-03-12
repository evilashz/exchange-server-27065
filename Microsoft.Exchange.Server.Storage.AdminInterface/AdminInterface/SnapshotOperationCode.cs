using System;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000060 RID: 96
	internal enum SnapshotOperationCode : uint
	{
		// Token: 0x040001E6 RID: 486
		None,
		// Token: 0x040001E7 RID: 487
		Prepare,
		// Token: 0x040001E8 RID: 488
		Freeze,
		// Token: 0x040001E9 RID: 489
		Thaw,
		// Token: 0x040001EA RID: 490
		Truncate,
		// Token: 0x040001EB RID: 491
		Stop,
		// Token: 0x040001EC RID: 492
		Last
	}
}
