using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000295 RID: 661
	internal struct NATIVE_INDEXCREATE
	{
		// Token: 0x04000705 RID: 1797
		public uint cbStruct;

		// Token: 0x04000706 RID: 1798
		public IntPtr szIndexName;

		// Token: 0x04000707 RID: 1799
		public IntPtr szKey;

		// Token: 0x04000708 RID: 1800
		public uint cbKey;

		// Token: 0x04000709 RID: 1801
		public uint grbit;

		// Token: 0x0400070A RID: 1802
		public uint ulDensity;

		// Token: 0x0400070B RID: 1803
		public unsafe NATIVE_UNICODEINDEX* pidxUnicode;

		// Token: 0x0400070C RID: 1804
		public IntPtr cbVarSegMac;

		// Token: 0x0400070D RID: 1805
		public IntPtr rgconditionalcolumn;

		// Token: 0x0400070E RID: 1806
		public uint cConditionalColumn;

		// Token: 0x0400070F RID: 1807
		public int err;
	}
}
