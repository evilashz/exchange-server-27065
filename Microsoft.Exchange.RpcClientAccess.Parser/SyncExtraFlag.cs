using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000093 RID: 147
	[Flags]
	internal enum SyncExtraFlag : uint
	{
		// Token: 0x04000217 RID: 535
		None = 0U,
		// Token: 0x04000218 RID: 536
		Eid = 1U,
		// Token: 0x04000219 RID: 537
		MessageSize = 2U,
		// Token: 0x0400021A RID: 538
		Cn = 4U,
		// Token: 0x0400021B RID: 539
		OrderByDeliveryTime = 8U,
		// Token: 0x0400021C RID: 540
		NoChanges = 16U,
		// Token: 0x0400021D RID: 541
		ManifestMode = 32U,
		// Token: 0x0400021E RID: 542
		CatchUpFull = 64U,
		// Token: 0x0400021F RID: 543
		ReadCn = 128U
	}
}
