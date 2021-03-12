using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000242 RID: 578
	[Flags]
	public enum EnumerateColumnsGrbit
	{
		// Token: 0x040003B3 RID: 947
		None = 0,
		// Token: 0x040003B4 RID: 948
		EnumerateCompressOutput = 524288,
		// Token: 0x040003B5 RID: 949
		EnumerateCopy = 1,
		// Token: 0x040003B6 RID: 950
		EnumerateIgnoreDefault = 32,
		// Token: 0x040003B7 RID: 951
		EnumeratePresenceOnly = 131072,
		// Token: 0x040003B8 RID: 952
		EnumerateTaggedOnly = 262144
	}
}
