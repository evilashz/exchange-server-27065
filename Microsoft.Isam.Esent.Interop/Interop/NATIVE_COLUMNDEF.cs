using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000273 RID: 627
	internal struct NATIVE_COLUMNDEF
	{
		// Token: 0x040004B4 RID: 1204
		public uint cbStruct;

		// Token: 0x040004B5 RID: 1205
		public uint columnid;

		// Token: 0x040004B6 RID: 1206
		public uint coltyp;

		// Token: 0x040004B7 RID: 1207
		[Obsolete("Reserved")]
		public ushort wCountry;

		// Token: 0x040004B8 RID: 1208
		[Obsolete("Use cp")]
		public ushort langid;

		// Token: 0x040004B9 RID: 1209
		public ushort cp;

		// Token: 0x040004BA RID: 1210
		[Obsolete("Reserved")]
		public ushort wCollate;

		// Token: 0x040004BB RID: 1211
		public uint cbMax;

		// Token: 0x040004BC RID: 1212
		public uint grbit;
	}
}
