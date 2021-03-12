using System;

namespace Microsoft.Isam.Esent.Interop.Vista
{
	// Token: 0x020002A8 RID: 680
	internal struct NATIVE_OPENTEMPORARYTABLE
	{
		// Token: 0x0400078F RID: 1935
		public uint cbStruct;

		// Token: 0x04000790 RID: 1936
		public unsafe NATIVE_COLUMNDEF* prgcolumndef;

		// Token: 0x04000791 RID: 1937
		public uint ccolumn;

		// Token: 0x04000792 RID: 1938
		public unsafe NATIVE_UNICODEINDEX* pidxunicode;

		// Token: 0x04000793 RID: 1939
		public uint grbit;

		// Token: 0x04000794 RID: 1940
		public unsafe uint* rgcolumnid;

		// Token: 0x04000795 RID: 1941
		public uint cbKeyMost;

		// Token: 0x04000796 RID: 1942
		public uint cbVarSegMac;

		// Token: 0x04000797 RID: 1943
		public IntPtr tableid;
	}
}
