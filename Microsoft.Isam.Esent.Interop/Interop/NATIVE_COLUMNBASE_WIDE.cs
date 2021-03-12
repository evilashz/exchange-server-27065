using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200026F RID: 623
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct NATIVE_COLUMNBASE_WIDE
	{
		// Token: 0x0400048E RID: 1166
		private const int NameSize = 256;

		// Token: 0x0400048F RID: 1167
		public uint cbStruct;

		// Token: 0x04000490 RID: 1168
		public uint columnid;

		// Token: 0x04000491 RID: 1169
		public uint coltyp;

		// Token: 0x04000492 RID: 1170
		[Obsolete("Reserved")]
		public ushort wCountry;

		// Token: 0x04000493 RID: 1171
		[Obsolete("Use cp")]
		public ushort langid;

		// Token: 0x04000494 RID: 1172
		public ushort cp;

		// Token: 0x04000495 RID: 1173
		[Obsolete("Reserved")]
		public ushort wFiller;

		// Token: 0x04000496 RID: 1174
		public uint cbMax;

		// Token: 0x04000497 RID: 1175
		public uint grbit;

		// Token: 0x04000498 RID: 1176
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string szBaseTableName;

		// Token: 0x04000499 RID: 1177
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string szBaseColumnName;
	}
}
