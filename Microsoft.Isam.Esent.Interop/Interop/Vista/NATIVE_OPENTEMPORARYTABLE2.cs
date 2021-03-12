using System;

namespace Microsoft.Isam.Esent.Interop.Vista
{
	// Token: 0x020002AA RID: 682
	internal struct NATIVE_OPENTEMPORARYTABLE2
	{
		// Token: 0x040007A0 RID: 1952
		public uint cbStruct;

		// Token: 0x040007A1 RID: 1953
		public unsafe NATIVE_COLUMNDEF* prgcolumndef;

		// Token: 0x040007A2 RID: 1954
		public uint ccolumn;

		// Token: 0x040007A3 RID: 1955
		public unsafe NATIVE_UNICODEINDEX2* pidxunicode;

		// Token: 0x040007A4 RID: 1956
		public uint grbit;

		// Token: 0x040007A5 RID: 1957
		public unsafe uint* rgcolumnid;

		// Token: 0x040007A6 RID: 1958
		public uint cbKeyMost;

		// Token: 0x040007A7 RID: 1959
		public uint cbVarSegMac;

		// Token: 0x040007A8 RID: 1960
		public IntPtr tableid;
	}
}
