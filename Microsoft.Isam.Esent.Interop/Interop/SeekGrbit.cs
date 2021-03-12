using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000249 RID: 585
	[Flags]
	public enum SeekGrbit
	{
		// Token: 0x040003D5 RID: 981
		SeekEQ = 1,
		// Token: 0x040003D6 RID: 982
		SeekLT = 2,
		// Token: 0x040003D7 RID: 983
		SeekLE = 4,
		// Token: 0x040003D8 RID: 984
		SeekGE = 8,
		// Token: 0x040003D9 RID: 985
		SeekGT = 16,
		// Token: 0x040003DA RID: 986
		SetIndexRange = 32
	}
}
