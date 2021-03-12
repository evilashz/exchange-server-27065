using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002D0 RID: 720
	internal struct NATIVE_TABLECREATE3
	{
		// Token: 0x04000888 RID: 2184
		public uint cbStruct;

		// Token: 0x04000889 RID: 2185
		[MarshalAs(UnmanagedType.LPWStr)]
		public string szTableName;

		// Token: 0x0400088A RID: 2186
		[MarshalAs(UnmanagedType.LPWStr)]
		public string szTemplateTableName;

		// Token: 0x0400088B RID: 2187
		public uint ulPages;

		// Token: 0x0400088C RID: 2188
		public uint ulDensity;

		// Token: 0x0400088D RID: 2189
		public unsafe NATIVE_COLUMNCREATE* rgcolumncreate;

		// Token: 0x0400088E RID: 2190
		public uint cColumns;

		// Token: 0x0400088F RID: 2191
		public IntPtr rgindexcreate;

		// Token: 0x04000890 RID: 2192
		public uint cIndexes;

		// Token: 0x04000891 RID: 2193
		[MarshalAs(UnmanagedType.LPWStr)]
		public string szCallback;

		// Token: 0x04000892 RID: 2194
		public JET_cbtyp cbtyp;

		// Token: 0x04000893 RID: 2195
		public uint grbit;

		// Token: 0x04000894 RID: 2196
		public unsafe NATIVE_SPACEHINTS* pSeqSpacehints;

		// Token: 0x04000895 RID: 2197
		public unsafe NATIVE_SPACEHINTS* pLVSpacehints;

		// Token: 0x04000896 RID: 2198
		public uint cbSeparateLV;

		// Token: 0x04000897 RID: 2199
		public IntPtr tableid;

		// Token: 0x04000898 RID: 2200
		public uint cCreated;
	}
}
