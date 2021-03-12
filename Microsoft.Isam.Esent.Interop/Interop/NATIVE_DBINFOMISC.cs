using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200027E RID: 638
	internal struct NATIVE_DBINFOMISC
	{
		// Token: 0x040004F8 RID: 1272
		public uint ulVersion;

		// Token: 0x040004F9 RID: 1273
		public uint ulUpdate;

		// Token: 0x040004FA RID: 1274
		public NATIVE_SIGNATURE signDb;

		// Token: 0x040004FB RID: 1275
		public uint dbstate;

		// Token: 0x040004FC RID: 1276
		public JET_LGPOS lgposConsistent;

		// Token: 0x040004FD RID: 1277
		public JET_LOGTIME logtimeConsistent;

		// Token: 0x040004FE RID: 1278
		public JET_LOGTIME logtimeAttach;

		// Token: 0x040004FF RID: 1279
		public JET_LGPOS lgposAttach;

		// Token: 0x04000500 RID: 1280
		public JET_LOGTIME logtimeDetach;

		// Token: 0x04000501 RID: 1281
		public JET_LGPOS lgposDetach;

		// Token: 0x04000502 RID: 1282
		public NATIVE_SIGNATURE signLog;

		// Token: 0x04000503 RID: 1283
		public JET_BKINFO bkinfoFullPrev;

		// Token: 0x04000504 RID: 1284
		public JET_BKINFO bkinfoIncPrev;

		// Token: 0x04000505 RID: 1285
		public JET_BKINFO bkinfoFullCur;

		// Token: 0x04000506 RID: 1286
		public uint fShadowingDisabled;

		// Token: 0x04000507 RID: 1287
		public uint fUpgradeDb;

		// Token: 0x04000508 RID: 1288
		public uint dwMajorVersion;

		// Token: 0x04000509 RID: 1289
		public uint dwMinorVersion;

		// Token: 0x0400050A RID: 1290
		public uint dwBuildNumber;

		// Token: 0x0400050B RID: 1291
		public uint lSPNumber;

		// Token: 0x0400050C RID: 1292
		public uint cbPageSize;
	}
}
