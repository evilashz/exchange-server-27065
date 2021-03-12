using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000271 RID: 625
	internal struct NATIVE_COLUMNCREATE
	{
		// Token: 0x040004A1 RID: 1185
		public uint cbStruct;

		// Token: 0x040004A2 RID: 1186
		public IntPtr szColumnName;

		// Token: 0x040004A3 RID: 1187
		public uint coltyp;

		// Token: 0x040004A4 RID: 1188
		public uint cbMax;

		// Token: 0x040004A5 RID: 1189
		public uint grbit;

		// Token: 0x040004A6 RID: 1190
		public IntPtr pvDefault;

		// Token: 0x040004A7 RID: 1191
		public uint cbDefault;

		// Token: 0x040004A8 RID: 1192
		public uint cp;

		// Token: 0x040004A9 RID: 1193
		public uint columnid;

		// Token: 0x040004AA RID: 1194
		public int err;
	}
}
