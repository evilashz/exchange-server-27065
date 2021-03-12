using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000094 RID: 148
	[Flags]
	internal enum SyncFlag : ushort
	{
		// Token: 0x04000221 RID: 545
		None = 0,
		// Token: 0x04000222 RID: 546
		Unicode = 1,
		// Token: 0x04000223 RID: 547
		NoDeletions = 2,
		// Token: 0x04000224 RID: 548
		NoSoftDeletions = 4,
		// Token: 0x04000225 RID: 549
		ReadState = 8,
		// Token: 0x04000226 RID: 550
		Associated = 16,
		// Token: 0x04000227 RID: 551
		Normal = 32,
		// Token: 0x04000228 RID: 552
		NoConflicts = 64,
		// Token: 0x04000229 RID: 553
		OnlySpecifiedProps = 128,
		// Token: 0x0400022A RID: 554
		NoForeignKeys = 256,
		// Token: 0x0400022B RID: 555
		LimitedIMessage = 512,
		// Token: 0x0400022C RID: 556
		CatchUp = 1024,
		// Token: 0x0400022D RID: 557
		Conversations = 2048,
		// Token: 0x0400022E RID: 558
		NewMessage = 2048,
		// Token: 0x0400022F RID: 559
		MessageSelective = 4096,
		// Token: 0x04000230 RID: 560
		BestBody = 8192,
		// Token: 0x04000231 RID: 561
		IgnoreSpecifiedOnAssociated = 16384,
		// Token: 0x04000232 RID: 562
		ProgressMode = 32768
	}
}
