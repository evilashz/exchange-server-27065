using System;

namespace Microsoft.Isam.Esent.Interop.Vista
{
	// Token: 0x020002B9 RID: 697
	internal struct NATIVE_RECSIZE2
	{
		// Token: 0x04000801 RID: 2049
		public ulong cbData;

		// Token: 0x04000802 RID: 2050
		public ulong cbLongValueData;

		// Token: 0x04000803 RID: 2051
		public ulong cbOverhead;

		// Token: 0x04000804 RID: 2052
		public ulong cbLongValueOverhead;

		// Token: 0x04000805 RID: 2053
		public ulong cNonTaggedColumns;

		// Token: 0x04000806 RID: 2054
		public ulong cTaggedColumns;

		// Token: 0x04000807 RID: 2055
		public ulong cLongValues;

		// Token: 0x04000808 RID: 2056
		public ulong cMultiValues;

		// Token: 0x04000809 RID: 2057
		public ulong cCompressedColumns;

		// Token: 0x0400080A RID: 2058
		public ulong cbDataCompressed;

		// Token: 0x0400080B RID: 2059
		public ulong cbLongValueDataCompressed;
	}
}
