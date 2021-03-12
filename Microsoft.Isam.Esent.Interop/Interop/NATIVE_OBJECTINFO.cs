using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002A2 RID: 674
	internal struct NATIVE_OBJECTINFO
	{
		// Token: 0x04000767 RID: 1895
		public uint cbStruct;

		// Token: 0x04000768 RID: 1896
		public uint objtyp;

		// Token: 0x04000769 RID: 1897
		[Obsolete("Unused member")]
		public double ignored1;

		// Token: 0x0400076A RID: 1898
		[Obsolete("Unused member")]
		public double ignored2;

		// Token: 0x0400076B RID: 1899
		public uint grbit;

		// Token: 0x0400076C RID: 1900
		public uint flags;

		// Token: 0x0400076D RID: 1901
		public uint cRecord;

		// Token: 0x0400076E RID: 1902
		public uint cPage;
	}
}
