using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002CF RID: 719
	internal struct NATIVE_TABLECREATE2
	{
		// Token: 0x0400087A RID: 2170
		public uint cbStruct;

		// Token: 0x0400087B RID: 2171
		[MarshalAs(UnmanagedType.LPTStr)]
		public string szTableName;

		// Token: 0x0400087C RID: 2172
		[MarshalAs(UnmanagedType.LPTStr)]
		public string szTemplateTableName;

		// Token: 0x0400087D RID: 2173
		public uint ulPages;

		// Token: 0x0400087E RID: 2174
		public uint ulDensity;

		// Token: 0x0400087F RID: 2175
		public unsafe NATIVE_COLUMNCREATE* rgcolumncreate;

		// Token: 0x04000880 RID: 2176
		public uint cColumns;

		// Token: 0x04000881 RID: 2177
		public IntPtr rgindexcreate;

		// Token: 0x04000882 RID: 2178
		public uint cIndexes;

		// Token: 0x04000883 RID: 2179
		[MarshalAs(UnmanagedType.LPTStr)]
		public string szCallback;

		// Token: 0x04000884 RID: 2180
		public JET_cbtyp cbtyp;

		// Token: 0x04000885 RID: 2181
		public uint grbit;

		// Token: 0x04000886 RID: 2182
		public IntPtr tableid;

		// Token: 0x04000887 RID: 2183
		public uint cCreated;
	}
}
