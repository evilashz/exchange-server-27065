using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002BC RID: 700
	internal struct NATIVE_RETRIEVECOLUMN
	{
		// Token: 0x04000814 RID: 2068
		public uint columnid;

		// Token: 0x04000815 RID: 2069
		public IntPtr pvData;

		// Token: 0x04000816 RID: 2070
		public uint cbData;

		// Token: 0x04000817 RID: 2071
		public uint cbActual;

		// Token: 0x04000818 RID: 2072
		public uint grbit;

		// Token: 0x04000819 RID: 2073
		public uint ibLongValue;

		// Token: 0x0400081A RID: 2074
		public uint itagSequence;

		// Token: 0x0400081B RID: 2075
		public uint columnidNextTagged;

		// Token: 0x0400081C RID: 2076
		public int err;
	}
}
