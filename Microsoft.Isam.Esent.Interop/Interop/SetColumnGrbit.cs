using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000240 RID: 576
	[Flags]
	public enum SetColumnGrbit
	{
		// Token: 0x040003A0 RID: 928
		None = 0,
		// Token: 0x040003A1 RID: 929
		AppendLV = 1,
		// Token: 0x040003A2 RID: 930
		OverwriteLV = 4,
		// Token: 0x040003A3 RID: 931
		RevertToDefaultValue = 512,
		// Token: 0x040003A4 RID: 932
		SeparateLV = 64,
		// Token: 0x040003A5 RID: 933
		SizeLV = 8,
		// Token: 0x040003A6 RID: 934
		UniqueMultiValues = 128,
		// Token: 0x040003A7 RID: 935
		UniqueNormalizedMultiValues = 256,
		// Token: 0x040003A8 RID: 936
		ZeroLength = 32,
		// Token: 0x040003A9 RID: 937
		IntrinsicLV = 1024
	}
}
