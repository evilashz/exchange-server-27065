using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000275 RID: 629
	internal struct NATIVE_COLUMNLIST
	{
		// Token: 0x040004C2 RID: 1218
		public uint cbStruct;

		// Token: 0x040004C3 RID: 1219
		public IntPtr tableid;

		// Token: 0x040004C4 RID: 1220
		public uint cRecord;

		// Token: 0x040004C5 RID: 1221
		public uint columnidPresentationOrder;

		// Token: 0x040004C6 RID: 1222
		public uint columnidcolumnname;

		// Token: 0x040004C7 RID: 1223
		public uint columnidcolumnid;

		// Token: 0x040004C8 RID: 1224
		public uint columnidcoltyp;

		// Token: 0x040004C9 RID: 1225
		public uint columnidCountry;

		// Token: 0x040004CA RID: 1226
		public uint columnidLangid;

		// Token: 0x040004CB RID: 1227
		public uint columnidCp;

		// Token: 0x040004CC RID: 1228
		public uint columnidCollate;

		// Token: 0x040004CD RID: 1229
		public uint columnidcbMax;

		// Token: 0x040004CE RID: 1230
		public uint columnidgrbit;

		// Token: 0x040004CF RID: 1231
		public uint columnidDefault;

		// Token: 0x040004D0 RID: 1232
		public uint columnidBaseTableName;

		// Token: 0x040004D1 RID: 1233
		public uint columnidBaseColumnName;

		// Token: 0x040004D2 RID: 1234
		public uint columnidDefinitionName;
	}
}
