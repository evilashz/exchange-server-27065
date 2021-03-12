using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000299 RID: 665
	internal struct NATIVE_INDEXCREATE3
	{
		// Token: 0x04000720 RID: 1824
		public uint cbStruct;

		// Token: 0x04000721 RID: 1825
		public IntPtr szIndexName;

		// Token: 0x04000722 RID: 1826
		public IntPtr szKey;

		// Token: 0x04000723 RID: 1827
		public uint cbKey;

		// Token: 0x04000724 RID: 1828
		public uint grbit;

		// Token: 0x04000725 RID: 1829
		public uint ulDensity;

		// Token: 0x04000726 RID: 1830
		public unsafe NATIVE_UNICODEINDEX2* pidxUnicode;

		// Token: 0x04000727 RID: 1831
		public IntPtr cbVarSegMac;

		// Token: 0x04000728 RID: 1832
		public IntPtr rgconditionalcolumn;

		// Token: 0x04000729 RID: 1833
		public uint cConditionalColumn;

		// Token: 0x0400072A RID: 1834
		public int err;

		// Token: 0x0400072B RID: 1835
		public uint cbKeyMost;

		// Token: 0x0400072C RID: 1836
		public IntPtr pSpaceHints;
	}
}
