using System;

namespace Microsoft.Isam.Esent.Interop.Vista
{
	// Token: 0x020002B8 RID: 696
	internal struct NATIVE_RECSIZE
	{
		// Token: 0x040007F9 RID: 2041
		public ulong cbData;

		// Token: 0x040007FA RID: 2042
		public ulong cbLongValueData;

		// Token: 0x040007FB RID: 2043
		public ulong cbOverhead;

		// Token: 0x040007FC RID: 2044
		public ulong cbLongValueOverhead;

		// Token: 0x040007FD RID: 2045
		public ulong cNonTaggedColumns;

		// Token: 0x040007FE RID: 2046
		public ulong cTaggedColumns;

		// Token: 0x040007FF RID: 2047
		public ulong cLongValues;

		// Token: 0x04000800 RID: 2048
		public ulong cMultiValues;
	}
}
