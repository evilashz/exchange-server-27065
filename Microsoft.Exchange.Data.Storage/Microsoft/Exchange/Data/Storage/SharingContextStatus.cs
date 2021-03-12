using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DCB RID: 3531
	internal enum SharingContextStatus
	{
		// Token: 0x040053DB RID: 21467
		Uninitialized,
		// Token: 0x040053DC RID: 21468
		TentativeConfig,
		// Token: 0x040053DD RID: 21469
		Configured,
		// Token: 0x040053DE RID: 21470
		Expired,
		// Token: 0x040053DF RID: 21471
		Revoked,
		// Token: 0x040053E0 RID: 21472
		TentativeDelete,
		// Token: 0x040053E1 RID: 21473
		Tombstone,
		// Token: 0x040053E2 RID: 21474
		Modified,
		// Token: 0x040053E3 RID: 21475
		TentativeRebind,
		// Token: 0x040053E4 RID: 21476
		NotFound
	}
}
