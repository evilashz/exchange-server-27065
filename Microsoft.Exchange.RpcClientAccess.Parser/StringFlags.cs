using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000092 RID: 146
	[Flags]
	internal enum StringFlags : uint
	{
		// Token: 0x0400020E RID: 526
		None = 0U,
		// Token: 0x0400020F RID: 527
		IncludeNull = 1U,
		// Token: 0x04000210 RID: 528
		Sized = 2U,
		// Token: 0x04000211 RID: 529
		Sized16 = 4U,
		// Token: 0x04000212 RID: 530
		SevenBitAscii = 8U,
		// Token: 0x04000213 RID: 531
		Sized32 = 16U,
		// Token: 0x04000214 RID: 532
		FailOnError = 32U,
		// Token: 0x04000215 RID: 533
		SevenBitAsciiOrFail = 40U
	}
}
