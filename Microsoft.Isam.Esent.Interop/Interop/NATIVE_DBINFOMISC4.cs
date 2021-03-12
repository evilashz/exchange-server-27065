using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200027F RID: 639
	internal struct NATIVE_DBINFOMISC4
	{
		// Token: 0x0400050D RID: 1293
		public NATIVE_DBINFOMISC dbinfo;

		// Token: 0x0400050E RID: 1294
		public uint genMinRequired;

		// Token: 0x0400050F RID: 1295
		public uint genMaxRequired;

		// Token: 0x04000510 RID: 1296
		public JET_LOGTIME logtimeGenMaxCreate;

		// Token: 0x04000511 RID: 1297
		public uint ulRepairCount;

		// Token: 0x04000512 RID: 1298
		public JET_LOGTIME logtimeRepair;

		// Token: 0x04000513 RID: 1299
		public uint ulRepairCountOld;

		// Token: 0x04000514 RID: 1300
		public uint ulECCFixSuccess;

		// Token: 0x04000515 RID: 1301
		public JET_LOGTIME logtimeECCFixSuccess;

		// Token: 0x04000516 RID: 1302
		public uint ulECCFixSuccessOld;

		// Token: 0x04000517 RID: 1303
		public uint ulECCFixFail;

		// Token: 0x04000518 RID: 1304
		public JET_LOGTIME logtimeECCFixFail;

		// Token: 0x04000519 RID: 1305
		public uint ulECCFixFailOld;

		// Token: 0x0400051A RID: 1306
		public uint ulBadChecksum;

		// Token: 0x0400051B RID: 1307
		public JET_LOGTIME logtimeBadChecksum;

		// Token: 0x0400051C RID: 1308
		public uint ulBadChecksumOld;

		// Token: 0x0400051D RID: 1309
		public uint genCommitted;

		// Token: 0x0400051E RID: 1310
		public JET_BKINFO bkinfoCopyPrev;

		// Token: 0x0400051F RID: 1311
		public JET_BKINFO bkinfoDiffPrev;
	}
}
