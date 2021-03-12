using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200002F RID: 47
	internal struct NATIVE_DBUTIL_LEGACY
	{
		// Token: 0x040000C4 RID: 196
		public uint cbStruct;

		// Token: 0x040000C5 RID: 197
		public IntPtr sesid;

		// Token: 0x040000C6 RID: 198
		public uint dbid;

		// Token: 0x040000C7 RID: 199
		public IntPtr tableid;

		// Token: 0x040000C8 RID: 200
		public DBUTIL_OP op;

		// Token: 0x040000C9 RID: 201
		public EDBDUMP_OP edbdump;

		// Token: 0x040000CA RID: 202
		public DbutilGrbit grbitOptions;

		// Token: 0x040000CB RID: 203
		[MarshalAs(UnmanagedType.LPTStr)]
		public string szDatabase;

		// Token: 0x040000CC RID: 204
		[MarshalAs(UnmanagedType.LPTStr)]
		public string szSLV;

		// Token: 0x040000CD RID: 205
		[MarshalAs(UnmanagedType.LPTStr)]
		public string szBackup;

		// Token: 0x040000CE RID: 206
		[MarshalAs(UnmanagedType.LPTStr)]
		public string szTable;

		// Token: 0x040000CF RID: 207
		[MarshalAs(UnmanagedType.LPTStr)]
		public string szIndex;

		// Token: 0x040000D0 RID: 208
		[MarshalAs(UnmanagedType.LPTStr)]
		public string szIntegPrefix;

		// Token: 0x040000D1 RID: 209
		public int pgno;

		// Token: 0x040000D2 RID: 210
		public int iline;

		// Token: 0x040000D3 RID: 211
		public int lGeneration;

		// Token: 0x040000D4 RID: 212
		public int isec;

		// Token: 0x040000D5 RID: 213
		public int ib;

		// Token: 0x040000D6 RID: 214
		public int cRetry;

		// Token: 0x040000D7 RID: 215
		public IntPtr pfnCallback;

		// Token: 0x040000D8 RID: 216
		public IntPtr pvCallback;
	}
}
