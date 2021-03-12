using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000012 RID: 18
	[Flags]
	internal enum SeekReference
	{
		// Token: 0x04000023 RID: 35
		OriginBeginning = 0,
		// Token: 0x04000024 RID: 36
		OriginCurrent = 1,
		// Token: 0x04000025 RID: 37
		OriginEnd = 2,
		// Token: 0x04000026 RID: 38
		SeekBackward = 4,
		// Token: 0x04000027 RID: 39
		ValidFlags = 7,
		// Token: 0x04000028 RID: 40
		ForwardFromBeginning = 0,
		// Token: 0x04000029 RID: 41
		ForwardFromCurrent = 1,
		// Token: 0x0400002A RID: 42
		BackwardFromCurrent = 5,
		// Token: 0x0400002B RID: 43
		BackwardFromEnd = 6
	}
}
