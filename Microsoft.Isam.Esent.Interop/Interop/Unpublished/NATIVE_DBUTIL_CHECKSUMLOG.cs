using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000030 RID: 48
	internal struct NATIVE_DBUTIL_CHECKSUMLOG
	{
		// Token: 0x040000D9 RID: 217
		public uint cbStruct;

		// Token: 0x040000DA RID: 218
		public IntPtr sesid;

		// Token: 0x040000DB RID: 219
		public uint dbid;

		// Token: 0x040000DC RID: 220
		public IntPtr tableid;

		// Token: 0x040000DD RID: 221
		public DBUTIL_OP op;

		// Token: 0x040000DE RID: 222
		public EDBDUMP_OP edbdump;

		// Token: 0x040000DF RID: 223
		public DbutilGrbit grbitOptions;

		// Token: 0x040000E0 RID: 224
		[MarshalAs(UnmanagedType.LPTStr)]
		public string szLog;

		// Token: 0x040000E1 RID: 225
		[MarshalAs(UnmanagedType.LPTStr)]
		public string szBase;

		// Token: 0x040000E2 RID: 226
		public IntPtr pvBuffer;

		// Token: 0x040000E3 RID: 227
		public int cbBuffer;

		// Token: 0x040000E4 RID: 228
		public IntPtr PadPointer1;

		// Token: 0x040000E5 RID: 229
		public IntPtr PadPointer2;

		// Token: 0x040000E6 RID: 230
		public int PadInt1;

		// Token: 0x040000E7 RID: 231
		public int PadInt2;

		// Token: 0x040000E8 RID: 232
		public int PadInt3;

		// Token: 0x040000E9 RID: 233
		public int PadInt4;

		// Token: 0x040000EA RID: 234
		public int PadInt5;

		// Token: 0x040000EB RID: 235
		public int PadInt6;

		// Token: 0x040000EC RID: 236
		public IntPtr PadPointer3;

		// Token: 0x040000ED RID: 237
		public IntPtr PadPointer4;
	}
}
