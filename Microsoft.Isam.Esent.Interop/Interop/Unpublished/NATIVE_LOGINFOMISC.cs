using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200003A RID: 58
	internal struct NATIVE_LOGINFOMISC
	{
		// Token: 0x04000124 RID: 292
		public uint ulGeneration;

		// Token: 0x04000125 RID: 293
		public NATIVE_SIGNATURE signLog;

		// Token: 0x04000126 RID: 294
		public JET_LOGTIME logtimeCreate;

		// Token: 0x04000127 RID: 295
		public JET_LOGTIME logtimePreviousGeneration;

		// Token: 0x04000128 RID: 296
		public JET_LogInfoFlag ulFlags;

		// Token: 0x04000129 RID: 297
		public uint ulVersionMajor;

		// Token: 0x0400012A RID: 298
		public uint ulVersionMinor;

		// Token: 0x0400012B RID: 299
		public uint ulVersionUpdate;

		// Token: 0x0400012C RID: 300
		public uint cbSectorSize;

		// Token: 0x0400012D RID: 301
		public uint cbHeader;

		// Token: 0x0400012E RID: 302
		public uint cbFile;

		// Token: 0x0400012F RID: 303
		public uint cbDatabasePageSize;
	}
}
