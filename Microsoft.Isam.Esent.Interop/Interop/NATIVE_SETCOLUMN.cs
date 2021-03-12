using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002C3 RID: 707
	internal struct NATIVE_SETCOLUMN
	{
		// Token: 0x0400083B RID: 2107
		public uint columnid;

		// Token: 0x0400083C RID: 2108
		public IntPtr pvData;

		// Token: 0x0400083D RID: 2109
		public uint cbData;

		// Token: 0x0400083E RID: 2110
		public uint grbit;

		// Token: 0x0400083F RID: 2111
		public uint ibLongValue;

		// Token: 0x04000840 RID: 2112
		public uint itagSequence;

		// Token: 0x04000841 RID: 2113
		public uint err;
	}
}
