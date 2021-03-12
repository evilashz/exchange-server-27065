using System;

namespace Microsoft.Exchange.DxStore.Server
{
	// Token: 0x02000050 RID: 80
	[Flags]
	public enum StoreState
	{
		// Token: 0x04000178 RID: 376
		Unknown = 0,
		// Token: 0x04000179 RID: 377
		Initializing = 1,
		// Token: 0x0400017A RID: 378
		Current = 2,
		// Token: 0x0400017B RID: 379
		Stale = 4,
		// Token: 0x0400017C RID: 380
		Struck = 8,
		// Token: 0x0400017D RID: 381
		CatchingUp = 16,
		// Token: 0x0400017E RID: 382
		NoMajority = 32
	}
}
