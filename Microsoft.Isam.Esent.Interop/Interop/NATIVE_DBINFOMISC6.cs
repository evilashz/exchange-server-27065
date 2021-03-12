using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200002C RID: 44
	internal struct NATIVE_DBINFOMISC6
	{
		// Token: 0x04000085 RID: 133
		public NATIVE_DBINFOMISC4 dbinfo4;

		// Token: 0x04000086 RID: 134
		public uint ulIncrementalReseedCount;

		// Token: 0x04000087 RID: 135
		public JET_LOGTIME logtimeIncrementalReseed;

		// Token: 0x04000088 RID: 136
		public uint ulIncrementalReseedCountOld;

		// Token: 0x04000089 RID: 137
		public uint ulPagePatchCount;

		// Token: 0x0400008A RID: 138
		public JET_LOGTIME logtimePagePatch;

		// Token: 0x0400008B RID: 139
		public uint ulPagePatchCountOld;

		// Token: 0x0400008C RID: 140
		public JET_LOGTIME logtimeChecksumPrev;

		// Token: 0x0400008D RID: 141
		public JET_LOGTIME logtimeChecksumStart;

		// Token: 0x0400008E RID: 142
		public uint cpgDatabaseChecked;
	}
}
