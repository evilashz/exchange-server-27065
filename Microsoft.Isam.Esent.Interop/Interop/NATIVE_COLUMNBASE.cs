using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200026E RID: 622
	internal struct NATIVE_COLUMNBASE
	{
		// Token: 0x04000482 RID: 1154
		private const int NameSize = 256;

		// Token: 0x04000483 RID: 1155
		public uint cbStruct;

		// Token: 0x04000484 RID: 1156
		public uint columnid;

		// Token: 0x04000485 RID: 1157
		public uint coltyp;

		// Token: 0x04000486 RID: 1158
		[Obsolete("Reserved")]
		public ushort wCountry;

		// Token: 0x04000487 RID: 1159
		[Obsolete("Use cp")]
		public ushort langid;

		// Token: 0x04000488 RID: 1160
		public ushort cp;

		// Token: 0x04000489 RID: 1161
		[Obsolete("Reserved")]
		public ushort wFiller;

		// Token: 0x0400048A RID: 1162
		public uint cbMax;

		// Token: 0x0400048B RID: 1163
		public uint grbit;

		// Token: 0x0400048C RID: 1164
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string szBaseTableName;

		// Token: 0x0400048D RID: 1165
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string szBaseColumnName;
	}
}
