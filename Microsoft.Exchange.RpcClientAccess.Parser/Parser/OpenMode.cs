using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000076 RID: 118
	[Flags]
	internal enum OpenMode : byte
	{
		// Token: 0x0400017D RID: 381
		ReadOnly = 0,
		// Token: 0x0400017E RID: 382
		ReadWrite = 1,
		// Token: 0x0400017F RID: 383
		Create = 2,
		// Token: 0x04000180 RID: 384
		BestAccess = 3,
		// Token: 0x04000181 RID: 385
		OpenSoftDeleted = 4,
		// Token: 0x04000182 RID: 386
		Append = 4,
		// Token: 0x04000183 RID: 387
		NoBlock = 8
	}
}
