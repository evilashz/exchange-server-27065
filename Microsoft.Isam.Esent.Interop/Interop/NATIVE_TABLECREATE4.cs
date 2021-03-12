using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002D2 RID: 722
	internal struct NATIVE_TABLECREATE4
	{
		// Token: 0x040008A9 RID: 2217
		public uint cbStruct;

		// Token: 0x040008AA RID: 2218
		[MarshalAs(UnmanagedType.LPWStr)]
		public string szTableName;

		// Token: 0x040008AB RID: 2219
		[MarshalAs(UnmanagedType.LPWStr)]
		public string szTemplateTableName;

		// Token: 0x040008AC RID: 2220
		public uint ulPages;

		// Token: 0x040008AD RID: 2221
		public uint ulDensity;

		// Token: 0x040008AE RID: 2222
		public unsafe NATIVE_COLUMNCREATE* rgcolumncreate;

		// Token: 0x040008AF RID: 2223
		public uint cColumns;

		// Token: 0x040008B0 RID: 2224
		public IntPtr rgindexcreate;

		// Token: 0x040008B1 RID: 2225
		public uint cIndexes;

		// Token: 0x040008B2 RID: 2226
		[MarshalAs(UnmanagedType.LPWStr)]
		public string szCallback;

		// Token: 0x040008B3 RID: 2227
		public JET_cbtyp cbtyp;

		// Token: 0x040008B4 RID: 2228
		public uint grbit;

		// Token: 0x040008B5 RID: 2229
		public unsafe NATIVE_SPACEHINTS* pSeqSpacehints;

		// Token: 0x040008B6 RID: 2230
		public unsafe NATIVE_SPACEHINTS* pLVSpacehints;

		// Token: 0x040008B7 RID: 2231
		public uint cbSeparateLV;

		// Token: 0x040008B8 RID: 2232
		public IntPtr tableid;

		// Token: 0x040008B9 RID: 2233
		public uint cCreated;
	}
}
