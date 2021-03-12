using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000243 RID: 579
	[Flags]
	public enum GetRecordSizeGrbit
	{
		// Token: 0x040003BA RID: 954
		None = 0,
		// Token: 0x040003BB RID: 955
		InCopyBuffer = 1,
		// Token: 0x040003BC RID: 956
		RunningTotal = 2,
		// Token: 0x040003BD RID: 957
		Local = 4
	}
}
